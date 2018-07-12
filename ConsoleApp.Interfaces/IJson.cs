using Newtonsoft.Json.Linq;

namespace ConsoleApp.Interface
{
    public interface IJson
    {
        double CalculateArea();

        void LoadFromJson(JObject jobject);
    }
}
