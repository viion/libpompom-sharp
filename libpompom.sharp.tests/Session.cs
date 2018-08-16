using System;
using Xunit;
using Pompom;
using System.Threading.Tasks;
using System.Diagnostics;
using Pompom.Models.Response;

namespace libpompom.sharp.tests
{
    public class Session
    {
        [Fact]
        public static async Task GenerateToken()
        {
            var companion = new Companion();

            var token = await companion.GetToken();

            Debug.WriteLine($"token={token.Token}");
            Debug.WriteLine($"salt={token.Salt}");
            Debug.WriteLine($"region={token.Region}");

            Assert.NotNull(token.Token);
            Assert.NotNull(token.Salt);
        }
    }
}
