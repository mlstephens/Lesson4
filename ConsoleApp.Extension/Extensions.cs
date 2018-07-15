using ConsoleApp.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shape.AllShapes;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp.Extension
{
    public static class Extensions
    {
        public static readonly string CirclesArgument = "-circles";
        public static readonly string SquaresArgument = "-squares";
        public static readonly string TrianglesArgument = "-triangles";
        public static readonly string ParallelogramsArgument = "-parallelograms";

        /// <summary>
        /// LoadShapes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">collection of shapes ie: circles</param>
        /// <param name="file">file containing the shape data</param>
        /// <returns>a shape collection</returns>
        public static List<T> LoadShapes<T>(this List<T> items, FileInfo file) where T : class, IJson, new()
        {
            if (file != null)
                {
                foreach (var jobj in file.GetParsedJsonData())
                {
                    var tValue = new T();

                    tValue.LoadFromJson(jobj);
                    
                    items.Add(tValue);
                }
            }

            return items;
        }

        /// <summary>
        /// GetAllShapes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="allShapes">AllShapes class that holds collections of each shape</param>
        /// <returns>collection of all shapes sorted by name, id and area descending</returns>
        public static List<IShape<T>> GetAllShapes<T>(this AllShapes<T> shapes)
        {
            return shapes.Circles.Cast<IJson>()
                .Concat(shapes.Parallelograms.Cast<IJson>())
                .Concat(shapes.Squares.Cast<IJson>())
                .Concat(shapes.Triangles.Cast<IJson>())
                .OrderByDescending(s => s.CalculateArea())
                .Cast<IShape<T>>()
                .OrderBy(s => s.Name)
                .ThenBy(s => s.Id)
                .ToList();
        }

        /// <summary>
        /// HaveValidArguments
        /// </summary>
        /// <param name="clArguments">command line arguments</param>
        /// <returns></returns>
        public static bool HasValidArguments(this string[] clArguments)
        {
            return clArguments.Any(a => string.Equals(a, CirclesArgument)
                || string.Equals(a, ParallelogramsArgument)
                || string.Equals(a, SquaresArgument)
                || string.Equals(a, TrianglesArgument));
        }

        /// <summary>
        /// GetFilePathFromArgument
        /// </summary>
        /// <param name="clArguments">command line arguments</param>
        /// <param name="argumentNameValue">the command line argument type being searched for. Example: -circle</param>
        /// <returns>file path or null if can't find that argumentnamevalue</returns>
        public static FileInfo GetFilePathFromArgument(this string[] clArguments, string argumentNameValue)
        {
            FileInfo file = null;

            string filePath = clArguments
                .SkipWhile(a => string.Compare(a, argumentNameValue, true) != 0)
                .Skip(1)
                .FirstOrDefault();

            if (!string.IsNullOrEmpty(filePath))
            {
                file = new FileInfo(filePath);

                if (!file.Exists)
                {
                    throw new FileNotFoundException($"Invalid file name - {file.FullName}.");
                }
            }

            return file;
        }

        /// <summary>
        /// GetParsedJsonData
        /// </summary>
        /// <param name="file">file containing json data</param>
        /// <returns>collection of jobjects</returns>
        private static List<JObject> GetParsedJsonData(this FileInfo file)
        {
            List<JObject> parsedData = null;

            try
            {
                parsedData = JsonConvert.DeserializeObject<JObject[]>(File.ReadAllText(file.FullName)).ToList();
            }
            catch
            {
                throw new JsonSerializationException("Error with JSON file.");
            }

            return parsedData;
        }
    }
}
