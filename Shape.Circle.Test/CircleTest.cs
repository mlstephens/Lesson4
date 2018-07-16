using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConsoleApp;
using ConsoleApp.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Shape.Circle.Test
{
    [TestClass]
    public class CircleTest
    {
        [TestMethod]
        public void File_WithValidJSONFormat()
        {
            //arrange
            AllShapes<int> allShapes = new AllShapes<int>();
            var file = new FileInfo(CreateTempFile(new string[] { "[{ 'id': 1, 'r': 4}]" }));

            try
            {
                //act
                allShapes.Circles.LoadShapes(file);
                List<IShape<int>> igc = allShapes.GetAllShapes<int>();

                //assert
                Assert.IsTrue(igc.Any(i => i.Id == 1 && igc.Cast<IJson>().Any(j => Math.Round(j.CalculateArea()) == 50)));

            }
            finally
            {
                File.Delete(file.FullName);
            }
        }

        [TestMethod]
        public void File_WithInvalidJSONFormat()
        {
            //arrange
            AllShapes<int> allShapes = new AllShapes<int>();
            var file = new FileInfo(CreateTempFile(new string[] { "[ 'id': 1 'r': 19.756, 'id': 2 'r': 52 ]" }));

            try
            {
                //assert
                Assert.ThrowsException<JsonSerializationException>(() => allShapes.Circles.LoadShapes(file));
            }
            finally
            {
                File.Delete(file.FullName);
            }
        }

        [TestMethod]
        public void File_WithInvalidJSONPropertyNames()
        {
            //arrange
            AllShapes<int> allShapes = new AllShapes<int>();
            var file = new FileInfo(CreateTempFile(new string[] { "[{ 'idd': 1, 'rrr': 19.756},{ 'idd': 2, 'rrr': 52}]" }));

            try
            {
                //assert
                Assert.ThrowsException<ArgumentException>(() => allShapes.Circles.LoadShapes(file));
            }
            finally
            {
                File.Delete(file.FullName);
            }
        }

        [TestMethod]
        public void File_WithJSONSortedProperly()
        {
            //arrange
            AllShapes<int> allShapes = new AllShapes<int>();
            var file = new FileInfo(CreateTempFile(new string[] { "[{ 'id': 1, 'r': 19.756},{ 'id': 2, 'r': 52}, {'id': 2, 'r': 22}]" }));

            string[,] expectedResults = new string[,]
            {
                {"Circle", "1", "1226.16215499711" },
                {"Circle", "2", "8494.8665353068" },
                {"Circle", "2", "1520.53084433746" },
            };

            try
            {
                //act
                allShapes.Circles.LoadShapes(file);
                List<IShape<int>> testResults = allShapes.GetAllShapes<int>();

                //assert - validate that the sorted test results match the sorted expected results
                int counter = 0;
                foreach (var tr in testResults)
                {
                    Assert.AreEqual(tr.Name, expectedResults[counter, 0]);
                    Assert.AreEqual(tr.Id.ToString(), expectedResults[counter, 1]);
                    Assert.AreEqual(((IJson)tr).CalculateArea().ToString(), expectedResults[counter, 2]);

                    counter++;
                }
            }
            finally
            {
                File.Delete(file.FullName);
            }
        }

        //creates temporary testing file
        string CreateTempFile(string[] fileData)
        {
            string file = Path.GetTempFileName();
            File.WriteAllLines(file, fileData);

            return file;
        }
    }
}
