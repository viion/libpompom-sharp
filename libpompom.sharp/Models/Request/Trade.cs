using System;
using Newtonsoft.Json;

namespace Pompom.Models.Request
{
    public class ChangePrice
    {
        [JsonProperty("transactionId")]
        public string TransactionId { get; set; }

        [JsonProperty("sellPrice")]
        public string SellPrice { get; set; }
    }

    public class PurchaseItem
    {
        [JsonProperty("transactionId")]
        public string TransactionId { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("itemId")]
        public string ItemId { get; set; }

        [JsonProperty("catalogId")]
        public string CatalogueId { get; set; }
    }
}
