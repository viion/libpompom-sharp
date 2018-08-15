using System;
using RestSharp.Deserializers;

namespace Pompom.Models.Response
{
    public class GenerateTokenResponse
    {
        [DeserializeAs(Name = "token")]
        public string Token { get; set; }

        [DeserializeAs(Name = "salt")]
        public string Salt { get; set; }

        [DeserializeAs(Name = "region")]
        public string Region { get; set; }

    }
}
