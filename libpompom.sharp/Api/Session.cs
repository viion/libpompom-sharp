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
        public Task<TokenResponse> GetToken(string uid, PlatformType platform = PlatformType.Android)
        {
            var signedUid = EncryptUserId(uid);
            return GetToken(new DeviceInfo
            {
                Uid = signedUid,
                Platform = platform
            });
        }

        public Task<TokenResponse> GetToken(DeviceInfo info)
        {
            return Request(new CompanionRequest<TokenResponse>
            {
                Resource = "login/token",
                Send = (x) => x.PostJsonAsync(info),
                Map = (x) => x.ReceiveJson<TokenResponse>()
            });
        }

        public async Task DeleteToken()
        {
            await Request(new CompanionRequest<object>
            {
                Resource = "login/auth",
                Send = (x) => x.DeleteAsync(),
            });
        }

        private string EncryptUserId(string uid)
        {
            using (var certText = new StringReader(Properties.Resources.Token))
            {
                // Ughh... looks like X509Certificates class can't handle it.

                // Reads a key.
                var pr = new PemReader(certText);
                var keys = (RsaKeyParameters)pr.ReadObject();

                // Setup RSA w/ ECB mode.
                var engine = new Pkcs1Encoding(new RsaEngine());
                engine.Init(true, keys);

                // Encrypt a key
                using (var buffer = new MemoryStream())
                {
                    var uidBytes = Encoding.UTF8.GetBytes(uid);

                    var length = uidBytes.Length;
                    var blockSize = engine.GetInputBlockSize();
                    for (var chunkPos = 0; chunkPos < length; chunkPos += blockSize)
                    {
                        var chunkSize = Math.Min(blockSize, length - chunkPos);

                        var block = engine.ProcessBlock(uidBytes, chunkPos, chunkSize);
                        buffer.Write(block, 0, block.Length);
                    }

                    return Convert.ToBase64String(buffer.ToArray());
                }
            }
        }

        public Task<CharacterAccountResponse> GetCharacters()
        {
            return Request(new CompanionRequest<CharacterAccountResponse>
            {
                Resource = "login/characters",
                Send = (x) => x.GetAsync(),
                Map = (x) => x.ReceiveJson<CharacterAccountResponse>()
            });
        }
        /*
        public Task Login(string uid)
        {
            var request = new JsonRequest("login/auth", Method.POST);

            var requestId = NewRequestId();

            request.AddQueryParameter("token", Token);
            request.AddQueryParameter("uid", uid);
            request.AddQueryParameter("request_id", requestId);

            // TODO
            //Execute<object>();
            throw new NotImplementedException();
        }

        public Task GetRegions()
        {
            var request = new JsonRequest("login/region", Method.GET);

            // ..
            throw new NotImplementedException("response");
        }

        public Task GetCharacter()
        {
            var request = new JsonRequest("login/character", Method.GET);

            // ..
            throw new NotImplementedException("response");
        }

        public Task PostCharacter(ulong characterId)
        {
            // TODO: Need LocaleInfo too
            var request = new JsonRequest("login/characters/{char_id}", Method.GET);
            request.AddUrlSegment("char_id", characterId.ToString());

            throw new NotImplementedException("response");
            // TODO
        }

        public Task GetCharacters()
        {
            var request = new JsonRequest("login/characters", Method.GET);

            // ..
            throw new NotImplementedException("response");
        }*/
    }
}
