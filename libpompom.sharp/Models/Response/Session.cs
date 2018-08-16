using System.Collections.Generic;
using Newtonsoft.Json;
using Flurl;
using System.Security.Cryptography;
using System.Text;

namespace Pompom.Models.Response
{
    public class CharacterAccountResponse
    {
        [JsonProperty("updatedAt")]
        public long UpdatedAt { get; set; }

        [JsonProperty("apiParameters")]
        public ApiParametersInfo ApiParameters { get; set; }

        [JsonProperty("accounts")]
        public List<AccountInfo> Account { get; set; }

        public class AccountInfo
        {
            [JsonProperty("accName")]
            public string Name { get; set; }

            [JsonProperty("role")]
            public int Role { get; set; }

            [JsonProperty("contractEndTime")]
            public long ContractEndTime { get; set; }

            [JsonProperty("xDay")]
            public int XDay { get; set; }

            [JsonProperty("characters")]
            public List<CharacterInfo> Characters { get; set; }
        }

        public class CharacterInfo
        {
            [JsonProperty("cid")]
            public string CharacterId { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("world")]
            public string World { get; set; }

            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("faceUrl")]
            public string FaceUri { get; set; }

            [JsonProperty("bodyUrl")]
            public string BodyUri { get; set; }

            [JsonProperty("lodestonecid")]
            public string LodestoneCharacterId { get; set; }

            [JsonProperty("isRenamed")]
            public bool Renamed { get; set; }
        }

        public class ApiParametersInfo
        {
            [JsonProperty("lodestoneUrlTemplate")]
            public string LodestoneUriTemplate { get; set; }
        }
    }

    public class TokenResponse
    {
        private const string SQEX_AUTH_URI = "https://secure.square-enix.com/oauth/oa/oauthauth";
        private const string SQEX_LOGIN_URI = "https://secure.square-enix.com/oauth/oa/oauthlogin";
        
        private const string OAUTH_APP_ID = "ffxiv_comapp";
        private const string OAUTH_CALLBACK = "https://companion.finalfantasyxiv.com/api/0/auth/callback";

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("salt")]
        public string Salt { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }
        
        public string GetOAuthUri(string userId)
        {
            var redirectUri = BuildOAuthRedirectUri(userId);

            var uri = SQEX_AUTH_URI.SetQueryParams(new
            {
                client_id = OAUTH_APP_ID,
                lang = "en-us",
                response_type = "code",
                redirect_uri = redirectUri,
            });

            return uri.ToString();
        }

        private string BuildOAuthRedirectUri(string userId)
        {
            var encryptedUserId = EncryptUserId(userId);
            var uri = OAUTH_CALLBACK.SetQueryParams(new
            {
                token = Token,
                uid = encryptedUserId,
                request_id = Companion.NewRequestId(),
            });

            return uri.ToString();
        }

        private string EncryptUserId(string uid)
        {
            const int DIGEST_LENGTH = 1024;

            var saltData = Encoding.UTF8.GetBytes(Salt);

            // uid passed to oauth is encrypted with PBKDF2
            // where PRF is SHA1, salt is `app_salt`, the number of iterations is 1000 and and digest length is 1024
            using (var pbkdf2 = new Rfc2898DeriveBytes(uid, saltData, 1000))
            {
                var keyData = pbkdf2.GetBytes(DIGEST_LENGTH / 8);

                // Converts a key to the hex string
                var buffer = new StringBuilder(keyData.Length * 2);
                for (var i = 0; i < keyData.Length; i++)
                {
                    buffer.AppendFormat("{0:x2}", keyData[i]);
                }

                return buffer.ToString();
            }
        }
    }
}
