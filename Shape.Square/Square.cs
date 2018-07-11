using ConsoleApp.Interface;
using Newtonsoft.Json.Linq;

namespace Shape.Square
{
    public class Square<T> : IGenericClass<T>, IUtility
    {
        private T _id;
        private double _area;
        private double _length;
        private double _width;

        #region  ' IGenericClass Interface '

        T IGenericClass<T>.Id => _id;

        string IGenericClass<T>.Name => "Square";

        string IGenericClass<T>.Formula => "length x width";

        int IGenericClass<T>.Sides => 4;

        int IGenericClass<T>.Angles => 4;

        double IGenericClass<T>.Area => _area;

        #endregion

        #region ' IUtility Interface '

        void IUtility.CalculateArea()
        {
            _area = _length * _width;
        }

        void IUtility.LoadFromJson(JObject jobject)
        {
            _id = jobject.Property("id").ToObject<T>();
            _length = jobject.Property("l").ToObject<double>();
            _width = jobject.Property("w").ToObject<double>();
        }

        #endregion
    }
}
