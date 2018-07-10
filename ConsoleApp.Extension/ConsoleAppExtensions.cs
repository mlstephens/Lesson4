using ConsoleApp.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shape.AllShapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp.Extension
{
    public static class ConsoleAppExtensions
    {
        public static readonly string _circle = "-circles";
        public static readonly string _square = "-squares";
        public static readonly string _triangle = "-triangles";
        public static readonly string _parallelogram = "-parallelograms";

        /// <summary>
        /// LoadShapes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">collection of shapes ie: circles</param>
        /// <param name="filePath">file containing the shape data</param>
        /// <returns>a shape collection</returns>
        public static List<T> LoadShapes<T>(this List<T> items, string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                var jobjects = GetFileData(filePath);
                dynamic tValue;

                foreach (var jobj in jobjects)
                {
                    tValue = Activator.CreateInstance(typeof(T));

                    IUtility iu = tValue;
                    iu.LoadFromJson(jobj);
                    iu.CalculateArea();

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
        public static List<IGenericClass<T>> GetAllShapes<T>(this AllShapes<T> shapes)
        {
            return shapes.Circles.Cast<IGenericClass<T>>()
                .Concat(shapes.Parallelograms.Cast<IGenericClass<T>>())
                .Concat(shapes.Squares.Cast<IGenericClass<T>>())
                .Concat(shapes.Triangles.Cast<IGenericClass<T>>())
                .OrderBy(s => s.Name)
                .ThenBy(s => s.Id)
                .ThenByDescending(s => s.Area)
                .ToList();
        }

        /// <summary>
        /// GetFileData
        /// </summary>
        /// <param name="filePath">file location for data to be parsed</param>
        /// <returns>returns properly parsed file data</returns>
        private static List<JObject> GetFileData(this string filePath)
        {
            List<JObject> parsedData = null;

            if (!string.IsNullOrWhiteSpace(filePath))
            {
                parsedData = GetParsedJsonData(File.ReadAllText(filePath));
            }

            return parsedData;
        }

        /// <summary>
        /// GetFilePathFromArgument
        /// </summary>
        /// <param name="clArguments">command line arguments</param>
        /// <param name="argumentNameValue">the command line argument type being searched for. Example: -json</param>
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
        /// <param name="fileData">file containing json data</param>
        /// <returns>collection of jobjects</returns>
        private static List<JObject> GetParsedJsonData(string fileData)
        {
            List<JObject> parsedData = new List<JObject>();

            try
            {
                if (!string.IsNullOrEmpty(fileData))
                {
                    parsedData = JsonConvert.DeserializeObject<JObject[]>(fileData).ToList();
                }
            }
            catch
            {
                throw new JsonSerializationException("Error with JSON file.");
            }

            return parsedData;
        }

        /// <summary>
        /// HaveValidArgument
        /// </summary>
        /// <param name="clArguments">command line arguments</param>
        /// <returns></returns>
        public static bool HaveValidArguments(this string[] clArguments)
        {
            return clArguments.Any(a => string.Equals(a, _circle) 
                || string.Equals(a, _square) 
                || string.Equals(a, _triangle) 
                || string.Equals(a, _parallelogram));
        }

    }
}
