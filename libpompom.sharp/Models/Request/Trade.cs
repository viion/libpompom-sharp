using System;
using RestSharp.Deserializers;

namespace Pompom.Models.Request
{
    public class ChangePrice
    {
        [DeserializeAs(Name = "transactionId")]
        public string TransactionId { get; set; }

        [DeserializeAs(Name = "sellPrice")]
        public string SellPrice { get; set; }
    }

    public class PurchaseItem
    {
        [DeserializeAs(Name = "transactionId")]
        public string TransactionId { get; set; }

        [DeserializeAs(Name = "price")]
        public int Price { get; set; }

        [DeserializeAs(Name = "itemId")]
        public string ItemId { get; set; }

        [DeserializeAs(Name = "catalogId")]
        public string CatalogueId { get; set; }
    }
}
