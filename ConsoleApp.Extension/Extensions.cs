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
        public static readonly string Circles = "-circles";
        public static readonly string Square = "-squares";
        public static readonly string Triangles = "-triangles";
        public static readonly string Parallelograms = "-parallelograms";

        /// <summary>
        /// LoadShapes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">collection of shapes ie: circles</param>
        /// <param name="filePath">file containing the shape data</param>
        /// <returns>a shape collection</returns>
        public static List<T> LoadShapes<T>(this List<T> items, string filePath) where T : class, new()
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                foreach (var jobj in GetParsedJsonData(filePath))
                {
                    var tValue = new T();

                    IJson iu = (IJson)tValue;
                    iu.LoadFromJson(jobj);

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
            return shapes.Circles
                .Cast<IJson>()
                .OrderByDescending(s => s.CalculateArea())

                .Concat(shapes.Parallelograms
                        .Cast<IJson>()
                        .OrderByDescending(s => s.CalculateArea()))

                .Concat(shapes.Squares
                        .Cast<IJson>()
                        .OrderByDescending(s => s.CalculateArea()))

                .Concat(shapes.Triangles
                        .Cast<IJson>()
                        .OrderByDescending(s => s.CalculateArea()))

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
        public static bool HaveValidArguments(this string[] clArguments)
        {
            return clArguments.Any(a => string.Equals(a, Circles)
                || string.Equals(a, Parallelograms)
                || string.Equals(a, Square)
                || string.Equals(a, Triangles));
        }

        /// <summary>
        /// GetFilePathFromArgument
        /// </summary>
        /// <param name="clArguments">command line arguments</param>
        /// <param name="argumentNameValue">the command line argument type being searched for. Example: -circle</param>
        /// <returns>file path or empty string if can't find that argumentnamevalue</returns>
        public static string GetFilePathFromArgument(this string[] clArguments, string argumentNameValue)
        {
            string filePath = clArguments.SkipWhile(a => string.Compare(a, argumentNameValue, true) != 0)
                .Skip(1)
                .FirstOrDefault();

            if (!string.IsNullOrEmpty(filePath) && !File.Exists(filePath))
            {
                throw new FileNotFoundException($"Invalid file name - {filePath}.");
            }

            return filePath;
        }

        /// <summary>
        /// GetParsedJsonData
        /// </summary>
        /// <param name="filePath">file containing json data</param>
        /// <returns>collection of jobjects</returns>
        private static List<JObject> GetParsedJsonData(string filePath)
        {
            List<JObject> parsedData = null;

            try
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    parsedData = JsonConvert.DeserializeObject<JObject[]>(File.ReadAllText(filePath)).ToList();
                }
            }
            catch
            {
                throw new JsonSerializationException("Error with JSON file.");
            }

            return parsedData;
        }
    }
}
