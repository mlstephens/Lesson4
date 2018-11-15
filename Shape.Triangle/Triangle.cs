using ConsoleApp.Interface;
using Newtonsoft.Json.Linq;
using System;

namespace Shape.Triangle
{
    public class Triangle<T> : IShape<T>, IJson
    {
        private T _id;
        private double _base;
        private double _height;

        #region ' IShape Interface '

        T IShape<T>.Id => _id;

        string IShape<T>.Name => "Triangle";

        string IShape<T>.Formula => "base x height";

        int IShape<T>.Sides => 3;

        int IShape<T>.Angles => 3;

        #endregion

        #region ' IJson Interface '

        double IJson.CalculateArea()
        {
            return _base * _height;
        }

        void IJson.LoadFromJson(JObject jobject)
        {
            try
            {
                _id = jobject.Property("id").ToObject<T>();
                _base = jobject.Property("b").ToObject<double>();
                _height = jobject.Property("h").ToObject<double>();
            }
            catch
            {
                throw new ArgumentException("Invalid file parameters.");
            }
        }

        #endregion
    }
}
