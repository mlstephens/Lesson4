﻿using ConsoleApp.Interface;
using Newtonsoft.Json.Linq;
using System;

namespace Shape.Triangle
{
    public class Triangle<T> : IShape<T>, IShapeParse
    {
        private T _id;
        private double _area;
        private double _base;
        private double _height;

        #region ' IShape Interface '

        T IShape<T>.Id => _id;

        string IShape<T>.Name => "Triangle";

        string IShape<T>.Formula => "base x height";

        int IShape<T>.Sides => 3;

        int IShape<T>.Angles => 3;

        double IShape<T>.Area => _area;

        #endregion

        #region ' IShapeParse Interface '

        void IShapeParse.CalculateArea()
        {
            _area = _base * _height;
        }

        void IShapeParse.LoadFromJson(JObject jobject)
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
