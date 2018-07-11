using ConsoleApp.Interface;
using Newtonsoft.Json.Linq;
using System;

namespace Shape.Square
{
    public class Square<T> : IShape<T>, IShapeParse
    {
        private T _id;
        private double _area;
        private double _length;
        private double _width;

        #region  ' IShape Interface '

        T IShape<T>.Id => _id;

        string IShape<T>.Name => "Square";

        string IShape<T>.Formula => "length x width";

        int IShape<T>.Sides => 4;

        int IShape<T>.Angles => 4;

        double IShape<T>.Area => _area;

        #endregion

        #region ' IShapeParse Interface '

        void IShapeParse.CalculateArea()
        {
            _area = _length * _width;
        }

        void IShapeParse.LoadFromJson(JObject jobject)
        {
            try
            {
                _id = jobject.Property("id").ToObject<T>();
                _length = jobject.Property("l").ToObject<double>();
                _width = jobject.Property("w").ToObject<double>();
            }
            catch
            {
                throw new ArgumentException("Invalid file parameters.");
            }
        }

        #endregion
    }
}
