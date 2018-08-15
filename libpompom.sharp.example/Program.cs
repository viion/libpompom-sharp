using System;
using System.Threading.Tasks;
using Pompom;
using Pompom.Models.Request;
using Pompom.Models.Response;

namespace libpompom.sharp.example
{
    class Program
    {
        static void Main(string[] args)
        {
            var companion = new Companion();

            var token = companion.GenerateToken(new Pompom.Models.Request.DeviceInfo
            {
                Platform = Pompom.Models.PlatformType.Apple,
                Uuid = Guid.NewGuid().ToString(),
            }).Result;

            Console.WriteLine($"Generated token={token.Token} salt={token.Salt}");
        }
    }
}
