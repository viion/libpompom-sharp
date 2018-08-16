using System;
using System.Threading.Tasks;
using System.Text;
using Pompom.Models;
using Pompom.Models.Request;
using Pompom.Models.Response;
using Org.BouncyCastle.OpenSsl;
using System.IO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Encodings;
using Flurl.Http;

namespace Pompom
{
    public partial class Companion
    {
        public Task<LoginStatusResponse> GetLoginStatus()
        {
            // Note: StatusCode == 500 is expected but doesn't handle anything yet.
            return Request(new CompanionRequest<LoginStatusResponse>
            {
                Resource = "character/login-status",
                Send = (x) => x.GetAsync(),
                Map = (x) => x.ReceiveJson<LoginStatusResponse>(),
            });
        }

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
