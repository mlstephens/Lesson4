using Newtonsoft.Json.Linq;

namespace ConsoleApp.Interface
{
    public interface IUtility
    {
        void CalculateArea();

        void LoadFromJson(JObject jobject);
    }
}
