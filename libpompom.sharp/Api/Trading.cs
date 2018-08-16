using System;
using System.Threading.Tasks;
using Pompom.Models.Request;
using Pompom.Models.Response;

namespace Pompom
{
    public partial class Companion
    {
        /*
        public Task<PurchaseItemResponse> PurchaseItem(int num, PurchaseItem info)
        {
            var request = new JsonRequest("market/item", Method.POST);
            request.AddQueryParameter("pointType", num.ToString());
            request.AddJsonBody(info);

            return Execute<PurchaseItemResponse>(request);
        }*/
    }
}
