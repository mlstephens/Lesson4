using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConsoleApp;
using ConsoleApp.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Shape.Square.Test
{
    [TestClass]
    public class SquareTest
    {
        [TestMethod]
        public void File_WithValidJSONFormat()
        {
            //arrange
            AllShapes<int> allShapes = new AllShapes<int>();
            var file = new FileInfo(CreateTempFile(new string[] { "[{'id': 24, 'l': 24, 'w': 2}]" }));

            try
            {
                //act
                allShapes.Squares.LoadShapes(file);
                List<IShape<int>> igc = allShapes.GetAllShapes<int>();

                //assert
                Assert.IsTrue(igc.Any(i => i.Id == 24 && igc.Cast<IJson>().Any(j => Math.Round(j.CalculateArea()) == 48)));

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
            var file = new FileInfo(CreateTempFile(new string[] { "[ 'id': 24, 'l': 24, 'w': 2 ]" }));

            try
            {
                //assert
                Assert.ThrowsException<JsonSerializationException>(() => allShapes.Squares.LoadShapes(file));
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
            var file = new FileInfo(CreateTempFile(new string[] { "[{'idd': 24, 'lll': 24, 'www': 2}]" }));

            try
            {
                //assert
                Assert.ThrowsException<ArgumentException>(() => allShapes.Squares.LoadShapes(file));
            }
            finally
            {
                File.Delete(file.FullName);
            }
        }

        [TestMethod]
        public void File_WithDataSortedProperly()
        {
            //arrange
            AllShapes<int> allShapes = new AllShapes<int>();
            var file = new FileInfo(CreateTempFile(new string[] { "[{'id': 24, 'l': 24, 'w': 2},{'id': 05, 'l': 12.824, 'w': 15}, {'id': 5, 'l': 9.352, 'w': 10}]" }));

            string[,] expectedResults = new string[,]
            {
                {"Square", "5", "192.36" },
                {"Square", "5", "93.52" },
                {"Square", "24", "48" },
            };

            try
            {
                //act
                allShapes.Squares.LoadShapes(file);
                List<IShape<int>> testResults = allShapes.GetAllShapes<int>();

                //assert - validate that the sorted test results match the sorted expected results
                int counter = 0;
                foreach (var tr in testResults)
                {
                    IJson ij = (IJson)tr;

                    Assert.AreEqual(tr.Name, expectedResults[counter, 0]);
                    Assert.AreEqual(tr.Id.ToString(), expectedResults[counter, 1]);
                    Assert.AreEqual(ij.CalculateArea().ToString(), expectedResults[counter, 2]);

                    counter++;
                }
            }
            finally
            {
                File.Delete(file.FullName);
            }
        }

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
    }
}
