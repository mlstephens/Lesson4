using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Interface
{
    public interface IUtility
    {
        // circle PI * r2
        // square Side * Side
        // triangle Base * Height / 2
        // parallelogram Base * Height
        void CalculateArea();

        void LoadFromJson(JObject jobject);
    }
}
