using ConsoleApp.Extension;
using ConsoleApp.Interface;
using Shape.AllShapes;
using System;
using System.Collections.Generic;
using System.Linq;

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

                //allShapes.Circles.ForEach(pd => Console.WriteLine($"Circle (PI * Radius2, {pd.Sides} sides, {pd.Angles} angles): Id: {pd.Id}, Area {pd.}"));

                List<IGenericClass<int>> igc = allShapes.GetAllShapes<int>();

                foreach (var gc in igc)
                {
                    IUtility iu = (IUtility)gc;
                    Console.WriteLine($"{ gc.GetType().Name } ({ gc.Formula }, {gc.Sides} sides, {gc.Angles} angles): Id: {gc.Id}, Area {iu.CalculateArea()}");
                }
            }
        }
    }
}
