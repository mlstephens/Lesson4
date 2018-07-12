using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConsoleApp.Extension;
using ConsoleApp.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Shape.AllShapes;

namespace ConsoleApp.Test
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void Arguments_WithValidArguments()
        {
            //arrange
            string[] clArguments = new string[] { ConsoleAppExtensions._circle, string.Empty, ConsoleAppExtensions._parallelogram, string.Empty, ConsoleAppExtensions._square, string.Empty, ConsoleAppExtensions._triangle, string.Empty};

            //assert
            Assert.IsTrue(clArguments.HaveValidArguments());
        }

        [TestMethod]
        public void Arguments_WithInvalidArguments()
        {
            //arrange
            string[] clArguments = new string[] { "invalid1", string.Empty, "invalid2", string.Empty };

            //assert
            Assert.IsFalse(clArguments.HaveValidArguments());
        }

        [TestMethod]
        public void Arguments_WithInvalidFileNames()
        {
            //arrange
            string[] clArguments = new string[] { ConsoleAppExtensions._circle, "BadFile.txt", ConsoleAppExtensions._parallelogram, "BadFile.txt" };

            //assert
            Assert.ThrowsException<FileNotFoundException>(() => clArguments.GetFilePathFromArgument(ConsoleAppExtensions._circle));
            Assert.ThrowsException<FileNotFoundException>(() => clArguments.GetFilePathFromArgument(ConsoleAppExtensions._parallelogram));
        }

        [TestMethod]
        public void Arguments_WithMissingArguments()
        {
            //arrange
            string[] clArguments = Array.Empty<string>();

            //assert
            Assert.IsFalse(clArguments.HaveValidArguments());
        }        

        [TestMethod]
        public void File_WithValidJSONFormat()
        {
            //arrange
            AllShapes<int> allShapes = new AllShapes<int>();
            var file = CreateTempFile(new string[] { "[{ 'id': 1, 'r': 19.756},{ 'id': 2, 'r': 52}]" });

            try
            {
                //act
                allShapes.Circles.LoadShapes(file);
                List<IShape<int>> igc = allShapes.GetAllShapes<int>();

                //assert
                //Assert.IsTrue(igc.Any(i => i.Id == 1 && i.Area == 1226.1621549971051));
                //Assert.IsTrue(igc.Any(i => i.Id == 2 && i.Area == 8494.8665353068));
            }
            finally
            {
                File.Delete(file);
            }
        }

        [TestMethod]
        public void File_WithInvalidJSONFormat()
        {
            //arrange
            AllShapes<int> allShapes = new AllShapes<int>();
            var file = CreateTempFile(new string[] { "[ 'id': 1 'r': 19.756, 'id': 2 'r': 52 ]" });

            try
            {
                //assert
                Assert.ThrowsException<JsonSerializationException>(() => allShapes.Circles.LoadShapes(file));
            }
            finally
            {
                File.Delete(file);
            }
        }

        [TestMethod]
        public void File_WithInvalidJSONData()
        {
            //arrange
            AllShapes<int> allShapes = new AllShapes<int>();
            var file = CreateTempFile(new string[] { "[{ 'XX': 1, 'r': 19.756},{ 'XX': 2, 'r': 52}]" });

            try
            {
                //assert
                Assert.ThrowsException<ArgumentException>(() => allShapes.Circles.LoadShapes(file));
            }
            finally
            {
                File.Delete(file);
            }
        }

        [TestMethod]
        public void File_WithDataSortedProperly()
        {
            //arrange
            AllShapes<int> allShapes = new AllShapes<int>();
            var file1 = CreateTempFile(new string[] { "[{ 'id': 1, 'r': 19.756},{ 'id': 2, 'r': 52}]" });
            var file2 = CreateTempFile(new string[] { "[{ 'id': 203, 'b': 367, 'h': 134},{'id': 55, 'b': 98, 'h': 28.5}]" });

            string[,] expectedResults = new string[,]
            {
                {"Circle", "1", "1226.16215499711" },
                {"Circle", "2", "8494.8665353068" },
                {"Parallelogram", "55", "2793" },
                {"Parallelogram", "203", "49178" }
            };

            //act
            allShapes.Circles.LoadShapes(file1);
            allShapes.Parallelograms.LoadShapes(file2);
            List<IShape<int>> testResults = allShapes.GetAllShapes<int>();            

            //assert - validate that the sorted test results match the sorted expected results
            for (int i = 0; i < expectedResults.Length / 3; i++)
            {
                //name-formula-sides-angles
                Assert.AreEqual(testResults[i].Name, expectedResults[i,0]);
                Assert.AreEqual(testResults[i].Id.ToString(), expectedResults[i,1]);
                //Assert.AreEqual(testResults[i].Area.ToString(), expectedResults[i,2]);
            }
        }

        #region  " Non Test Methods "

        /// <summary>
        /// CreateTempFile
        /// </summary>
        /// <param name="fileData">array containing data you want to write to the file. pass an empty array if no data needed</param>
        /// <returns>filepath of new temp file</returns>
        string CreateTempFile(string[] fileData)
       {
           string file = Path.GetTempFileName();

           if (fileData.Length > 0)
           {
               File.WriteAllLines(file, fileData);
           }

           return file;
       }

       #endregion
    }
}
