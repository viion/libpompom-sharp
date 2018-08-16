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
using System.Threading.Tasks;

namespace Pompom
{
    public partial class Companion
    {
        public Task<RetainerListResponse> GetRetainers()
        {
            return Request(new CompanionRequest<RetainerListResponse>
            {
                Resource = "retainers",
                Send = (x) => x.GetAsync(),
                Map = (x) => x.ReceiveJson<RetainerListResponse>(),
            });
        }
    }
}
