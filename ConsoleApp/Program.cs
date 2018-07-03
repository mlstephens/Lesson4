using ConsoleApp.Extension;
using ConsoleApp.Interface;
using Newtonsoft.Json.Linq;
using Shape.AllShapes;
using Shape.Circle;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                if (clArguments.Any(cl => cl.Equals(ConsoleAppExtensions._circle)))
                {
                    var filePath = clArguments.GetFilePathFromArgument(ConsoleAppExtensions._circle);                    
                    allShapes.Circles.LoadShapes(filePath);
                }

                //parallelograms
                if (clArguments.Any(cl => cl.Equals(ConsoleAppExtensions._parallelogram)))
                {
                    var filePath = clArguments.GetFilePathFromArgument(ConsoleAppExtensions._parallelogram);
                    allShapes.Parallelograms.LoadShapes(filePath);
                }

                //squares
                if (clArguments.Any(cl => cl.Equals(ConsoleAppExtensions._square)))
                {
                    var filePath = clArguments.GetFilePathFromArgument(ConsoleAppExtensions._square);
                    allShapes.Squares.LoadShapes(filePath);
                }

                //triangles
                if (clArguments.Any(cl => cl.Equals(ConsoleAppExtensions._triangle)))
                {
                    var filePath = clArguments.GetFilePathFromArgument(ConsoleAppExtensions._triangle);
                    allShapes.Triangles.LoadShapes(filePath);
                }

                //Circle<int> circle = new Circle<int>();
                //IGenericClass<int> igc = circle;
                //IUtility iu = circle;

                //List<JObject> parsedData = iu.LoadFromJson(clArguments).ToList();
                //parsedData.ForEach(pd => Console.WriteLine($"Circle (PI * Radius2, {igc.Sides} sides, {igc.Angles} angles): Id: {igc.Id}, Area {iu.CalculateArea()}"));

            }
        }
    }
}
