using System;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Deserializers;

namespace Pompom
{
    public partial class Companion
    {
#if HTTPBIN
        private const string BASE_API_URI = "http://httpbin.org/get";
#else
        private const string BASE_API_URI = "https://companion.finalfantasyxiv.com/sight-v060/sight/";
#endif

        private const string DEFAULT_WEBVIEW_USERAGENT = @"Mozilla/5.0 (Linux; Android 7.0; Moto G (4) Build/NPJ25.93-14; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/56.0.2924.87 Mobile Safari/537.36";
        private const string USER_AGENT_POSTFIX = "XIV-Companion for Android";

        private string userAgent;
        private string token;


        public Companion(string token, string userAgent = DEFAULT_WEBVIEW_USERAGENT)
        {
            // iOS or Android user agent + postfix
            this.userAgent = $"{userAgent} {USER_AGENT_POSTFIX}";
            this.token = token;
        }

        protected RestClient NewRestClient()
        {
            var client = new RestClient(BASE_API_URI);
            client.UserAgent = this.userAgent;

            return client;
        }

        /// <summary>
        /// Set common headers.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="requestId">A request id.</param>
        public void PrepareRequest(IRestRequest request, string requestId)
        {
            // Set http headers
            request.AddHeader("token", this.token);
            request.AddHeader("request-id", requestId);
        }

        /// <summary>
        /// Creates a request id which can be used to make an api call.
        /// </summary>
        /// <returns>Request id.</returns>
        private string NewRequestId()
        {
            var requestId = Guid.NewGuid(); // chosen by fair dice roll.
                                            // guaranteed to be random.

            return requestId.ToString();
        }

        public async Task<IRestResponse<T>> Execute<T>(IRestRequest request) where T : new()
        {
            var requestId = NewRequestId();
            var response = await Execute<T>(request, requestId); // ¯\_(ツ)_/¯
            return response;
        }

        public async Task<IRestResponse<T>> Execute<T>(IRestRequest request, string requestId) where T : new()
        {
            // https://github.com/restsharp/RestSharp/wiki/Recommended-Usage
            var client = NewRestClient();
            PrepareRequest(request, requestId);            

            // Send a request
            var response = await client.ExecuteTaskAsync<T>(request);
            return response;
        }
    }
}
