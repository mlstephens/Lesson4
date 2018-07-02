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
        const string _name = "Triangle";
        const string _formula = "Base * Height";
        const int _sides = 3;
        const int _angles = 3;

        private T _id;
        private int _base;
        private int _height;

        #region ' IGenericClass Interface '

        T IGenericClass<T>.Id => _id;

        string IGenericClass<T>.Name => _name;

        string IGenericClass<T>.Formula => _formula;

        int IGenericClass<T>.Sides => _sides;

        int IGenericClass<T>.Angles => _angles;

        #endregion

        #region ' IUtility Interface '

        double IUtility.CalculateArea()
        {
            return _base * _height;
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
