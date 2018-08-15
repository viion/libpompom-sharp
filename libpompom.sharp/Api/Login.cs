using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Pompom
{
    public partial class Companion
    {
        /// <summary>
        /// Setup common headers used in login.
        /// </summary>
        /// <param name=""></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private void PrepareLoginRequest(IRestRequest request)
        {
            request.AddHeader("domain-type", "global");
        }

        public async Task Login(string uid)
        {
            var request = new RestRequest("login/auth", Method.POST);
            PrepareLoginRequest(request);

            var requestId = NewRequestId();

            request.AddQueryParameter("token", this.token);
            request.AddQueryParameter("uid", uid);
            request.AddQueryParameter("request_id", requestId);

            // TODO
            //Execute<object>();
        }

        public async Task GetRegions()
        {
            var request = new RestRequest("login/region", Method.GET);
            PrepareLoginRequest(request);

            // ..
            throw new NotImplementedException("response");
        }

        public async Task GetCharacter()
        {
            var request = new RestRequest("login/character", Method.GET);
            PrepareLoginRequest(request);

            // ..
            throw new NotImplementedException("response");
        }

        public async Task PostCharacter(ulong characterId)
        {
            // TODO: Need LocaleInfo too
            var request = new RestRequest("login/characters/{char_id}", Method.GET);
            PrepareLoginRequest(request);
            request.AddUrlSegment("char_id", characterId.ToString());

            throw new NotImplementedException("response");
            // TODO
        }

        public async Task GetCharacters()
        {
            var request = new RestRequest("login/characters", Method.GET);
            PrepareLoginRequest(request);

            // ..
            throw new NotImplementedException("response");
        }
    }
}
