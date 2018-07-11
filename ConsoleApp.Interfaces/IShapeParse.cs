using Newtonsoft.Json.Linq;

namespace ConsoleApp.Interface
{
    public interface IShapeParse
    {
        void CalculateArea();

        void LoadFromJson(JObject jobject);
    }
}
