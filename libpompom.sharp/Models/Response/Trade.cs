using Newtonsoft.Json;
using RestSharp.Deserializers;

namespace Pompom.Models.Response
{
    public class PurchaseItemResponse
    {
        [JsonProperty("gil")]
        public int Gil { get; set; }
    }
}
