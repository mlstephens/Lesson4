using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ConsoleApp.Interface;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Shape.Square
{
    public class Square<T> : IGenericClass<T>, IUtility
    {
        private T _id;
        private double _area;
        private int _length;
        private int _width;

        #region  ' IGenericClass Interface '

        T IGenericClass<T>.Id => _id;

        string IGenericClass<T>.Name => "Square";

        string IGenericClass<T>.Formula => "Width * Height";

        int IGenericClass<T>.Sides => 4;

        int IGenericClass<T>.Angles => 4;

        double IGenericClass<T>.Area => _area;

        #endregion

        #region ' IUtility Interface '

        double IUtility.CalculateArea()
        {
            _area = _length * _width;
            return _area;
        }

        void IUtility.LoadFromJson(JObject jobject)
        {
            _id = jobject.Property("id").ToObject<T>();
            _length = jobject.Property("l").ToObject<int>();
            _width = jobject.Property("w").ToObject<int>();
        }

        #endregion
    }
}
