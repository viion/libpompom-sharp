using System;
using Xunit;
using Pompom;
using Pompom.Testing.Httpbin;
using RestSharp;
using System.Threading.Tasks;

namespace libpompom.sharp.tests
{
    public class Login
    {
        [Fact]
        public async Task Test1()
        {
            // TODO
            var client = new Companion("Hello, world!");

            var request = new RestRequest("", Method.GET);

            var response = await client.Execute<Anything>(request);

        }
    }
}
