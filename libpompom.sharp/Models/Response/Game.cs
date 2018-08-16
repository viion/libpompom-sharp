using Newtonsoft.Json;

namespace Pompom.Models.Response
{
    public class LoginStatusResponse
    {
        [JsonProperty("getBagFlag")]
        public bool GetBagFlag { get; set; }
    }
}
