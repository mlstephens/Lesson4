using Newtonsoft.Json.Linq;

namespace ConsoleApp.Interface
{
    public interface IJson
    {
        void CalculateArea();

        void LoadFromJson(JObject jobject);
    }
}
