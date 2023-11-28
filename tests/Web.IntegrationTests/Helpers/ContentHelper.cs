using System.Text;
using Newtonsoft.Json;

namespace Web.IntegrationTests.Helpers;
internal class ContentHelper
{
    public static StringContent GetStringContent(object obj)
       => new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");
}
