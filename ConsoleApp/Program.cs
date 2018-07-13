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

                //combine all shapes
                List<IShape<int>> iShapes = allShapes.GetAllShapes<int>();

                //display shape data
                foreach(var shape in iShapes)
                {
                    IJson ij = (IJson)shape;
                    Console.WriteLine($"{ shape.Name } ({ shape.Formula }, { shape.Sides } sides, { shape.Angles } angles): Id: { shape.Id }, Area: {ij.CalculateArea()}");
                }
            }
        }
    }
}
