using Newtonsoft.Json;

namespace Pompom.Models.Response
{
    public class PurchaseItemResponse
    {
        [JsonProperty("gil")]
        public int Gil { get; set; }
    }
}
