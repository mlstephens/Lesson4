using ConsoleApp.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        /// <typeparam name="T">collection type. ie: circle</typeparam>
        /// <param name="items">collection of shapes ie: circles</param>
        /// <param name="filePath">file containing the shape data</param>
        /// <returns>a shape collection</returns>
        public static List<T> LoadShapes<T>(this List<T> items, string filePath)
        {
            //parse the file data returning jobject list
            //loop thru the jobject list, 
            //      create a new shape object
            //      call LoadFromJson(jobject) to parse data
            //add the item to the shape collection

            var jobjects = GetFileData(filePath);
            dynamic tValue;

            foreach(var jobj in jobjects)
            {
                tValue = Activator.CreateInstance(typeof(T));

                IUtility iu = tValue;
                iu.LoadFromJson(jobj);

                items.Add(tValue);
            }

            return items;
        }

        /// <summary>
        /// GetAllShapes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">AllShapes class that holds collections of each shape</param>
        /// <returns>collection of all shapes sorted</returns>
        public static List<T> GetAllShapes<T>(this object item)
        {
            //List<T> shapes = item.Circles
            //    .Concat(item.Parallelograms)
            //    .Concat(item.Squares)
            //    .Concat(item.Triangles)
            //    .ToList();

            return null;
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
                parsedData = GetParsedData(File.ReadAllText(filePath));
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
        /// GetParsedData
        /// </summary>
        /// <param name="fileData"> </param>
        /// <returns></returns>
        private static List<JObject> GetParsedData(string fileData)
        {
            List<JObject> parsedData = new List<JObject>();

            try
            {
                if (!string.IsNullOrEmpty(fileData))
                {
                    //create a list of json values
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
