using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace ConsoleApp.Test
{
    [TestClass]
    public class ConsoleAppTest
    {
        [TestMethod]
        public void Arguments_WithValidArguments()
        {
            //arrange
            string[] clArguments = new string[] { Extensions.CirclesArgument, string.Empty, Extensions.ParallelogramsArgument, string.Empty, Extensions.SquaresArgument, string.Empty, Extensions.TrianglesArgument, string.Empty };

            //assert
            Assert.IsTrue(clArguments.ValidArguments());
        }

        [TestMethod]
        public void Arguments_WithInvalidArguments()
        {
            //arrange
            string[] clArguments = new string[] { "invalid1", string.Empty, "invalid2", string.Empty };

            //assert
            Assert.IsFalse(clArguments.ValidArguments());
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
            Assert.IsFalse(clArguments.ValidArguments());
        } 
    }
}
