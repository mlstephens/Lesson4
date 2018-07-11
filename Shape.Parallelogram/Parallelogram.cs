using ConsoleApp.Interface;
using Newtonsoft.Json.Linq;

namespace Shape.Parallelogram
{
    public class Parallelogram<T> : IGenericClass<T>, IUtility
    {
        private T _id;
        private double _area;
        private double _base;
        private double _height;        

        #region ' IGenericClass Interface '

        T IGenericClass<T>.Id => _id;

        string IGenericClass<T>.Name => "Parallelogram";

        string IGenericClass<T>.Formula => "base x height";

        int IGenericClass<T>.Sides => 4;

        int IGenericClass<T>.Angles => 4;

        double IGenericClass<T>.Area => _area;

        #endregion

        #region ' IUtility Interface '

        void IUtility.CalculateArea()
        {
            _area = _base * _height;
        }

        void IUtility.LoadFromJson(JObject jobject)
        {
            _id = jobject.Property("id").ToObject<T>();
            _base = jobject.Property("b").ToObject<double>();
            _height = jobject.Property("h").ToObject<double>();
        }

        #endregion
    }
}
