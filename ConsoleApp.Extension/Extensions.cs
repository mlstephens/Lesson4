using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Extension
{ 
    public static class Extensions
    {
        const string _circle = "-circles";
        const string _square = "-squares";
        const string _triangle = "-triangles";
        const string _parallelogram = "-parallelograms";

        public static List<object> LoadShapes(this List<object> items, string[] clArguments)
        {
            return items;
        }

        /// <summary>
        /// GetCircleData
        /// </summary>
        /// <param name="clArguments">command line arguments</param>
        /// <returns></returns>
        public static List<JObject> GetCircleData(this string[] clArguments)
        {
            return clArguments.GetFilePathFromArgument(_circle).GetFileData();
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
        private static string GetFilePathFromArgument(this string[] clArguments, string argumentNameValue)
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
