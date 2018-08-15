using System;
using Xunit;
using Pompom;
using RestSharp;
using System.Threading.Tasks;

namespace libpompom.sharp.tests
{
    public class Session
    {
        [Fact]
        public async Task GenerateToken()
        {
            var companion = new Companion();

            var token = await companion.GenerateToken(Guid.NewGuid().ToString());
            Assert.NotNull(token.Token);
            Assert.NotNull(token.Salt);
        }
    }
}
