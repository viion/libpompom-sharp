using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Flurl.Util;
using Polly;

namespace Pompom
{
    public partial class Companion
    {
        public static string NewRequestId()
        {
            return Guid.NewGuid().ToString();
        }

        private const HttpStatusCode HTTP_STATUS_PENDING = HttpStatusCode.Accepted;

        private const string BASE_API_URI = "https://companion.finalfantasyxiv.com/sight-v060/sight/";
        private const string API_USER_AGENT = "ffxivcomapp-j/1.0.0.5 CFNetwork/902.2 Darwin/17.7.0";
        private const string OUATH_IOS_USER_AGENT = "XIV-Companion for iPhone";
        private const string OAUTH_ANDROID_USER_AGENT = "XIV-Companion for Android";

        public string UserAgent { get; set; } = API_USER_AGENT;

        /// <summary>
        /// A timeout.
        /// </summary>
        /// <remarks>
        /// TODO: 3.5 seconds are expected. stuff, etc..
        /// TODO: API related in-game stuff takes a whole minute to complete if you're logged in the game lol.
        /// </remarks>
        public TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(3);

        /// <summary>
        /// This can be null for initial handshake and if so it won't be sent to the server.
        /// </summary>
        public string Token { get; set; }

        public IFlurlRequest PrepareRequest(string resource, string requestId)
        {
            var request = BASE_API_URI
                .AppendPathSegment(resource)
                .WithTimeout(Timeout)
                .WithHeader("Accept", "*/*")
                .WithHeader("Content-Type", "application /json;charset=utf-8")
                .WithHeader("request-id", requestId);

            // Appends a token
            if (Token != null)
            {
                request = request.WithHeader("token", Token);
            }

            return request;
        }

        public async Task<T> Request<T>(CompanionRequest<T> request)
        {
            // Build a request
            var httpRequest = PrepareRequest(request.Resource, request.Id);
            httpRequest = request.Setup(httpRequest);

            // Post a request
            // TODO: replace these shitty code with polly
            var elapsed = TimeSpan.Zero;
            while (true)
            {
                if (elapsed > Timeout)
                {
                    throw new TimeoutException("Server is not responding.");
                }

                var responseTask = request.Send(httpRequest);
                var response = await responseTask;

                if (response.StatusCode != HTTP_STATUS_PENDING)
                {
                    // Map a response
                    return await request.Map(responseTask);
                }

                // API call is not processed yet. Try again after 1 second.
                await Task.Delay(1000);
                elapsed += TimeSpan.FromMilliseconds(1000);
            }
        }
    }

    public class CompanionRequest<T>
    {
        public CompanionRequest()
        {
            Setup = (x) => x;
            Send = (_) => throw new InvalidOperationException("Method is not specified");
            Map = (_) => Task.FromResult<T>(default);
        }

        public string Resource { get; set; } = "";
        public string Id { get; set; } = Companion.NewRequestId();

        public Func<IFlurlRequest, IFlurlRequest> Setup { get; set; }
        public Func<IFlurlRequest, Task<HttpResponseMessage>> Send { get; set; }
        public Func<Task<HttpResponseMessage>, Task<T>> Map { get; set; }
    }
}
