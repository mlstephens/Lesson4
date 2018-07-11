using ConsoleApp.Interface;
using Newtonsoft.Json.Linq;
using System;

namespace Shape.Circle
{
    public class Circle<T> : IShape<T>, IUtility
    {
        private T _id;
        private double _area;
        private double _radius;        

        #region  ' IShape Interface '

        T IShape<T>.Id => _id;

        string IShape<T>.Name => "Circle";

        string IShape<T>.Formula => "pi x radius2";

        int IShape<T>.Sides => 0;

        int IShape<T>.Angles => 0;

        double IShape<T>.Area => _area;

        #endregion

        #region ' IUtility Interface '

        void IUtility.CalculateArea()
        {
            _area = Math.PI * (_radius * _radius);
        }

        void IUtility.LoadFromJson(JObject jobject)
        {
            try
            {
                _id = jobject.Property("id").ToObject<T>();
                _radius = jobject.Property("r").ToObject<double>();
            }
            catch
            {
                throw new ArgumentException("Invalid file parameters.");
            }
        }

        #endregion
    }
}
