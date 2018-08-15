using System;
using Newtonsoft.Json;
using RestSharp.Deserializers;

namespace Pompom.Models.Response
{
    public class GenerateTokenResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("salt")]
        public string Salt { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

    }
}
