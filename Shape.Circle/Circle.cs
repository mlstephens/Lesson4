using ConsoleApp.Interface;
using Newtonsoft.Json.Linq;
using System;

namespace Shape.Circle
{
    public class Circle<T> : IGenericClass<T>, IUtility
    {
        const string _name = "Circle";
        const string _formula = "PI * Radius2";
        const int _sides = 0;
        const int _angles = 0;

        private T _id;
        private int _radius;

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
            return Math.PI * _radius * _radius;
        }

        void IUtility.LoadFromJson(JObject jobject)
        {
            _id = jobject.Property("id").ToObject<T>();
            _radius = jobject.Property("r").ToObject<int>();
        }

        #endregion


    }
}
