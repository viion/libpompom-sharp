using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Pompom.Models.Request;
using Pompom.Models.Response;

namespace Pompom
{
    public partial class Companion
    {
        public Task<GenerateTokenResponse> GenerateToken(DeviceInfo device)
        {
            var request = new RestRequest("login/token", Method.POST);
            request.JsonSerializer = new RestSharp.Serializers.Newtonsoft.Json.NewtonsoftJsonSerializer();
            request.AddJsonBody(device);

            return Execute<GenerateTokenResponse>(request);
        }

        public Task Login(string uid)
        {
            var request = new RestRequest("login/auth", Method.POST);

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
            var request = new RestRequest("login/region", Method.GET);

            // ..
            throw new NotImplementedException("response");
        }

        public Task GetCharacter()
        {
            var request = new RestRequest("login/character", Method.GET);

            // ..
            throw new NotImplementedException("response");
        }

        public Task PostCharacter(ulong characterId)
        {
            // TODO: Need LocaleInfo too
            var request = new RestRequest("login/characters/{char_id}", Method.GET);
            request.AddUrlSegment("char_id", characterId.ToString());

            throw new NotImplementedException("response");
            // TODO
        }

        public Task GetCharacters()
        {
            var request = new RestRequest("login/characters", Method.GET);

            // ..
            throw new NotImplementedException("response");
        }
    }
}
