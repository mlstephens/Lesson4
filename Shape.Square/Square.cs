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
        const string _name = "Square";
        const string _formula = "Width * Height";
        const int _sides = 4;
        const int _angles = 4;

        private T _id;
        private int _length;
        private int _width;

        #region  ' IGenericClass Interface '

        T IGenericClass<T>.Id => _id;

        string IGenericClass<T>.Name => _name;

        string IGenericClass<T>.Formula => _formula;

        int IGenericClass<T>.Sides => _sides;

        int IGenericClass<T>.Angles => _angles;

        #endregion

        #region ' IUtility Interface '

        double IUtility.CalculateArea()
        {
            return _length * _width;
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
