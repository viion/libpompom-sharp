using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Pompom;

namespace Pompom.Examples
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var userId = Guid.NewGuid().ToString();
            var api = new Companion();

            var token = await api.GetToken(userId);
            Console.WriteLine($"Token={token.Token} Salt={token.Salt}");

            var uri = token.GetOAuthUri(userId);
            Console.WriteLine($"OAuth request: {uri}");
            Process.Start(uri);

            Console.WriteLine($"Press enter to continue..");
            Console.ReadLine();
        }
    }
}
