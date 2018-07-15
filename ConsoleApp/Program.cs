using ConsoleApp.Interface;
using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        [STAThread]
        static void Main(string[] clArguments)
        {
            if (clArguments.HasValidArguments())
            {
                AllShapes<int> allShapes = new AllShapes<int>();

                //circles
                allShapes.Circles.LoadShapes(clArguments.GetFilePathFromArgument(Extensions.CirclesArgument));

                //parallelograms
                allShapes.Parallelograms.LoadShapes(clArguments.GetFilePathFromArgument(Extensions.ParallelogramsArgument));

                //squares
                allShapes.Squares.LoadShapes(clArguments.GetFilePathFromArgument(Extensions.SquaresArgument));

                ////triangles
                allShapes.Triangles.LoadShapes(clArguments.GetFilePathFromArgument(Extensions.TrianglesArgument));

                //combine all shapes
                List<IShape<int>> iShapes = allShapes.GetAllShapes<int>();

                //display shape data
                foreach (var shape in iShapes)
                {
                    IJson ij = (IJson)shape;
                    Console.WriteLine($"{ shape.Name } ({ shape.Formula }, { shape.Sides } sides, { shape.Angles } angles): Id: { shape.Id }, Area: {ij.CalculateArea()}");
                }

                //testing...
                //iShapes.ForEach(s => Console.WriteLine($"{ s.Name } ({ s.Formula }, { s.Sides } sides, { s.Angles } angles): Id: { s.Id }, Area: {s.CalculateArea()}"));
            }
        }
    }
}
