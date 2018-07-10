using ConsoleApp.Interface;
using Newtonsoft.Json.Linq;
using System;

namespace Shape.Circle
{
    public class Circle<T> : IGenericClass<T>, IUtility
    {
        private T _id;
        private double _area;
        private int _radius;        

        #region  ' IGenericClass Interface '

        T IGenericClass<T>.Id => _id;

        string IGenericClass<T>.Name => "Circle";

        string IGenericClass<T>.Formula => "PI * Radius2";

        int IGenericClass<T>.Sides => 0;

        int IGenericClass<T>.Angles => 0;

        double IGenericClass<T>.Area => _area;

        #endregion

        #region ' IUtility Interface '

        double IUtility.CalculateArea()
        {
            _area = Math.PI * _radius * _radius;
            return _area;
        }

        void IUtility.LoadFromJson(JObject jobject)
        {
            _id = jobject.Property("id").ToObject<T>();
            _radius = jobject.Property("r").ToObject<int>();
        }

        #endregion
    }
}
