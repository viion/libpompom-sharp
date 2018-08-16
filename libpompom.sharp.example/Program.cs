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

            // Get OAuth login uri
            var uri = token.GetOAuthUri(userId);
            Console.WriteLine($"Login request: {uri}");
            Process.Start(new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = uri,
            });

            // Wait for the enter key
            Console.ReadLine();

            // Now we have a token. Have fun with the API.
            api.Token = token.Token;

            // Account info
            var accountInfo = await api.GetCharacters();
            foreach(var character in accountInfo.Account[0].Characters)
            {
                Console.WriteLine($"{character.Name}: {character.CharacterId}");
                Console.WriteLine($"Face image: {character.FaceUri}");
                Console.WriteLine($"Body image: {character.BodyUri}");
            }

            var retainers = await api.GetRetainers();

        }
    }
}
