using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var response = await RequestTokenAsync();
            response.Show();

            Console.ReadLine();
            await CallServiceAsync(response.AccessToken);
            //await GetVersion(response.AccessToken);
            Console.ReadLine();
        }

        static async Task GetVersion(string token)
        {
            var baseAddress = "http://localhost:5001";
            var client = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };

            client.SetBearerToken(token);
            var responce = await client.GetStringAsync("version/check?version=0.9");
            "\n\nResponce:".ConsoleGreen();
            Console.WriteLine(JObject.Parse(responce));
        }

        static async Task<TokenResponse> RequestTokenAsync()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                throw new Exception(disco.Error);
            }
            var responce = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "ro.client_with_identity",
                ClientSecret = "another_secret",
                Scope = "ThinClientApi",
                UserName = "admin@admin.com",
                Password = "123_Secret"
            });

            if (responce.IsError)
            {
                throw new Exception(responce.Error);
            }
            return responce;
        }

        static async Task CallServiceAsync(string token)
        {
            var baseAddress = "http://localhost:5001";
            var client = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };

            client.SetBearerToken(token);
            var responce = await client.GetStringAsync("identity");
            "\n\nService claims:".ConsoleGreen();
            Console.WriteLine(JArray.Parse(responce));
        }
    }
}
