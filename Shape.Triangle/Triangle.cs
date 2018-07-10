using ConsoleApp.Interface;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shape.Triangle
{
    public class Triangle<T> : IGenericClass<T>, IUtility
    {
        private T _id;
        private double _area;
        private int _base;
        private int _height;

        #region ' IGenericClass Interface '

        T IGenericClass<T>.Id => _id;

        string IGenericClass<T>.Name => "Triangle";

        string IGenericClass<T>.Formula => "Base * Height";

        int IGenericClass<T>.Sides => 3;

        int IGenericClass<T>.Angles => 3;

        double IGenericClass<T>.Area => _area;

        #endregion

        #region ' IUtility Interface '

        double IUtility.CalculateArea()
        {
            _area = _base * _height; ;
            return _area;
        }

        void IUtility.LoadFromJson(JObject jobject)
        {
            _id = jobject.Property("id").ToObject<T>();
            _base = jobject.Property("b").ToObject<int>();
            _height = jobject.Property("h").ToObject<int>();
        }

        #endregion
    }
}
