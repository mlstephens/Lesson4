using ConsoleApp.Extension;
using ConsoleApp.Interface;
using Shape.AllShapes;
using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        [STAThread]
        static void Main(string[] clArguments)
        {
            if (clArguments.HaveValidArguments())
            {
                AllShapes<int> allShapes = new AllShapes<int>();

                //circles
                allShapes.Circles.LoadShapes(clArguments.GetFilePathFromArgument(ConsoleAppExtensions._circle));

                //parallelograms
                allShapes.Parallelograms.LoadShapes(clArguments.GetFilePathFromArgument(ConsoleAppExtensions._parallelogram));

                //squares
                allShapes.Squares.LoadShapes(clArguments.GetFilePathFromArgument(ConsoleAppExtensions._square));

                //triangles
                allShapes.Triangles.LoadShapes(clArguments.GetFilePathFromArgument(ConsoleAppExtensions._triangle));

                List<IGenericClass<int>> igc = allShapes.GetAllShapes<int>();
                igc.ForEach(i => Console.WriteLine($"{ i.Name } ({ i.Formula }, {i.Sides} sides, {i.Angles} angles): Id: {i.Id}, Area {i.Area}"));
            }
        }
    }
}
