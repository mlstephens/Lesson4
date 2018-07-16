using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConsoleApp;
using ConsoleApp.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Shape.Parallelogram.Test
{
    [TestClass]
    public class ParallelogramTest
    {
        [TestMethod]
        public void File_WithValidJSONFormat()
        {
            //arrange
            AllShapes<int> allShapes = new AllShapes<int>();
            var file = new FileInfo(CreateTempFile(new string[] { "[{'id': 203, 'b': 367, 'h': 134}]" }));

            try
            {
                //act
                allShapes.Parallelograms.LoadShapes(file);
                List<IShape<int>> igc = allShapes.GetAllShapes<int>();

                //assert
                Assert.IsTrue(igc.Any(i => i.Id == 203 && igc.Cast<IJson>().Any(j => Math.Round(j.CalculateArea()) == 49178)));

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
            var file = new FileInfo(CreateTempFile(new string[] { "[ 'id': 203, 'b': 367, 'h': 134] " }));

            try
            {
                //assert
                Assert.ThrowsException<JsonSerializationException>(() => allShapes.Parallelograms.LoadShapes(file));
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
            var file = new FileInfo(CreateTempFile(new string[] { "[{'idd': 203, 'bbb': 367, 'hhh': 134}]" }));

            try
            {
                //assert
                Assert.ThrowsException<ArgumentException>(() => allShapes.Parallelograms.LoadShapes(file));
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
            var file = new FileInfo(CreateTempFile(new string[] { "[{'id': 203, 'b': 367, 'h': 134},{'id': 55, 'b': 98, 'h': 28.5}, {'id': 55, 'b': 70, 'h': 25.96}]" }));

            string[,] expectedResults = new string[,]
            {
                {"Parallelogram", "55", "2793" },
                {"Parallelogram", "55", "1817.2" },
                {"Parallelogram", "203", "49178" },
            };

            try
            {
                //act
                allShapes.Parallelograms.LoadShapes(file);
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
