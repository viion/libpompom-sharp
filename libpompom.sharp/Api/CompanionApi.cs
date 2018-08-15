using System;
using System.Net;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Deserializers;

namespace Pompom
{
    public partial class Companion
    {
        private const HttpStatusCode HTTP_STATUS_PENDING = HttpStatusCode.Accepted; // lol SE

#if HTTPBIN
        private const string BASE_API_URI = "http://httpbin.org/get";
#else
        private const string BASE_API_URI = "https://companion.finalfantasyxiv.com/sight-v060/sight/";
#endif

        private const string IOS_USER_AGENT = "ffxivcomapp-j/1.0.0.5 CFNetwork/902.2 Darwin/17.7.0";
        private const string ANDROID_USER_AGENT = "Mozilla/5.0 (Linux; Android 7.0; Moto G (4) Build/NPJ25.93-14; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/56.0.2924.87 Mobile Safari/537.36 XIV-Companion for Android";
        
        public string UserAgent { get; set; } = IOS_USER_AGENT;

        public int MaxTries { get; set; } = 5;
        public int PollingInterval { get; set; } = 1500;

        /// <summary>
        /// This can be null for initial handshake and if so it won't be sent to the server.
        /// </summary>
        public string Token { get; set; }
        
        protected RestClient NewRestClient()
        {
            var client = new RestClient(BASE_API_URI);
            client.UserAgent = UserAgent;

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
            if (Token != null)
            {
                request.AddHeader("token", Token);
            }
            
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

        public Task<T> Execute<T>(IRestRequest request) where T : new()
        {
            var requestId = NewRequestId();
            var response = Execute<T>(request, requestId); // ¯\_(ツ)_/¯
            return response;
        }

        public async Task<T> Execute<T>(IRestRequest request, string requestId) where T : new()
        {
            var response = await Request<T>(request, requestId);

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Error retrieving response. Check inner details for more info.", response.ErrorException);
            }

            if (!response.IsSuccessful)
            {
                throw new ApplicationException("Server returned an error.");
            }

            return response.Data;
        }

        public Task<IRestResponse<T>> Request<T>(IRestRequest request) where T : new()
        {
            var requestId = NewRequestId();
            var response = Request<T>(request, requestId); // ¯\_(ツ)_/¯
            return response;
        }

        public async Task<IRestResponse<T>> Request<T>(IRestRequest request, string requestId) where T : new()
        {
            // https://github.com/restsharp/RestSharp/wiki/Recommended-Usage
            var client = NewRestClient();
            PrepareRequest(request, requestId);

            // Send a request
            for (var i = 0; i < MaxTries; i++)
            {
                var response = await client.ExecuteTaskAsync<T>(request);
                if (response.StatusCode != HTTP_STATUS_PENDING)
                {
                    return response;
                }

                await Task.Delay(PollingInterval);
            }

            throw new TimeoutException("Server is busy");
        }
    }
}
