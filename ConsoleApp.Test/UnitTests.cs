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
        #region ' Arguments '

        [TestMethod]
        public void Arguments_WithValidArguments()
        {
            //arrange
            string[] clArguments = new string[] { Extensions.CirclesArgument, string.Empty, Extensions.ParallelogramsArgument, string.Empty, Extensions.SquaresArgument, string.Empty, Extensions.TrianglesArgument, string.Empty };

            //assert
            Assert.IsTrue(clArguments.HasValidArguments());
        }

        [TestMethod]
        public void Arguments_WithInvalidArguments()
        {
            //arrange
            string[] clArguments = new string[] { "invalid1", string.Empty, "invalid2", string.Empty };

            //assert
            Assert.IsFalse(clArguments.HasValidArguments());
        }

        [TestMethod]
        public void Arguments_WithInvalidFileNames()
        {
            //arrange
            string[] clArguments = new string[] { Extensions.CirclesArgument, "BadFile.txt", Extensions.ParallelogramsArgument, "BadFile.txt" };

            //assert
            Assert.ThrowsException<FileNotFoundException>(() => clArguments.GetFilePathFromArgument(Extensions.CirclesArgument));
            Assert.ThrowsException<FileNotFoundException>(() => clArguments.GetFilePathFromArgument(Extensions.ParallelogramsArgument));
        }

        [TestMethod]
        public void Arguments_WithMissingArguments()
        {
            //arrange
            string[] clArguments = Array.Empty<string>();

            //assert
            Assert.IsFalse(clArguments.HasValidArguments());
        } 

        #endregion

        #region ' Circles '

        [TestMethod]
        public void Circles_FileWithValidJSONFormat()
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
        public void Circles_FileWithInvalidJSONFormat()
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
                File.Delete(file.Name);
            }
        }

        [TestMethod]
        public void Circles_FileWithInvalidJSONPropertyNames()
        {
            //arrange
            AllShapes<int> allShapes = new AllShapes<int>();
            var file = new FileInfo(CreateTempFile(new string[] { "[{ 'XX': 1, 'ZZ': 19.756},{ 'XX': 2, 'ZZ': 52}]" }));

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
        public void Circles_FileWithDataSortedProperly()
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
                    IJson ij = (IJson)tr;

                    Assert.AreEqual(tr.Name, expectedResults[counter, 0]);
                    Assert.AreEqual(tr.Id.ToString(), expectedResults[counter, 1]);
                    Assert.AreEqual(ij.CalculateArea().ToString(), expectedResults[counter, 2]);

                    counter++;
                }
            }
            finally
            {
                File.Delete(file.Name);
            }
        }

        #endregion

        #region ' Parallelograms '

        [TestMethod]
        public void Parallelograms_FileWithValidJSONFormat()
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
        public void Parallelograms_FileWithInvalidJSONFormat()
        {
            //arrange
            AllShapes<int> allShapes = new AllShapes<int>();
            var file = new FileInfo(CreateTempFile(new string[] { "['id': 203, 'b': 367, 'h': 134]" }));

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
        public void Parallelograms_FileWithInvalidJSONPropertyNames()
        {
            //arrange
            AllShapes<int> allShapes = new AllShapes<int>();
            var file = new FileInfo(CreateTempFile(new string[] { "[{'idd': 203, 'bb': 367, 'hh': 134}]" }));

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
        public void Parallelograms_FileWithDataSortedProperly()
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

        #endregion

        #region ' Squares '

        [TestMethod]
        public void Squares_FileWithValidJSONFormat()
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
        public void Squares_FileWithInvalidJSONFormat()
        {
            //arrange
            AllShapes<int> allShapes = new AllShapes<int>();
            var file = new FileInfo(CreateTempFile(new string[] { "['id': 24, 'l': 24, 'w': 2]" }));

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
        public void Squares_FileWithInvalidJSONPropertyNames()
        {
            //arrange
            AllShapes<int> allShapes = new AllShapes<int>();
            var file = new FileInfo(CreateTempFile(new string[] { "[{'idd': 24, 'll': 24, 'ww': 2}]" }));

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
        public void Squares_FileWithDataSortedProperly()
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

        #endregion

        #region ' Triangles '

        [TestMethod]
        public void Triangles_FileWithValidJSONFormat()
        {
            //arrange
            AllShapes<int> allShapes = new AllShapes<int>();
            var file = new FileInfo(CreateTempFile(new string[] { "[{'id': 1, 'b': 34, 'h': 34.5}]" }));

            try
            {
                //act
                allShapes.Triangles.LoadShapes(file);
                List<IShape<int>> igc = allShapes.GetAllShapes<int>();

                //assert
                Assert.IsTrue(igc.Any(i => i.Id == 1 && igc.Cast<IJson>().Any(j => Math.Round(j.CalculateArea()) == 1173)));

            }
            finally
            {
                File.Delete(file.FullName);
            }
        }

        [TestMethod]
        public void Triangles_FileWithInvalidJSONFormat()
        {
            //arrange
            AllShapes<int> allShapes = new AllShapes<int>();
            var file = new FileInfo(CreateTempFile(new string[] { "[ 'id': 1, 'b': 34, 'h': 34.5 ]" }));

            try
            {
                //assert
                Assert.ThrowsException<JsonSerializationException>(() => allShapes.Triangles.LoadShapes(file));
            }
            finally
            {
                File.Delete(file.FullName);
            }
        }

        [TestMethod]
        public void Triangles_FileWithInvalidJSONPropertyNames()
        {
            //arrange
            AllShapes<int> allShapes = new AllShapes<int>();
            var file = new FileInfo(CreateTempFile(new string[] { "[{'idd': 1, 'bb': 34, 'hh': 34.5}]" }));

            try
            {
                //assert
                Assert.ThrowsException<ArgumentException>(() => allShapes.Triangles.LoadShapes(file));
            }
            finally
            {
                File.Delete(file.FullName);
            }
        }

        [TestMethod]
        public void Triangles_FileWithDataSortedProperly()
        {
            //arrange
            AllShapes<int> allShapes = new AllShapes<int>();
            var file = new FileInfo(CreateTempFile(new string[] { "[{'id': 1, 'b': 34, 'h': 34.5},{'id': 5, 'b': 22, 'h': 8},{'id': 1, 'b': 20, 'h': 21}]" }));

            string[,] expectedResults = new string[,]
            {
                {"Triangle", "1", "1173" },
                {"Triangle", "1", "420" },
                {"Triangle", "5", "176" },
            };

            try
            {
                //act
                allShapes.Triangles.LoadShapes(file);
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

        #endregion

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
