using RestSharp.Deserializers;

namespace Pompom.Models.Response
{
    public class PurchaseItemResponse
    {
        [DeserializeAs(Name = "gil")]
        public int Gil { get; set; }
    }
}
