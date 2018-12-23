using System;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback +=
                delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
                var response = await RequestTokenAsync();
                response.Show();

                Console.ReadLine();
                //await CallServiceAsync(response.AccessToken);
                await GetVersion(response.AccessToken);
                await GetConnectionData(response.AccessToken);
                Console.ReadLine();
            }
            catch (Exception e)
            {
                e.Message.ConsoleRed();
                Console.ReadLine();
            }
        }

        static HttpClient GetHttpClient(string address)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri( address);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        static async Task GetConnectionData(string token)
        {
            var baseAddress = "http://83.217.24.133:21150";
            var client = GetHttpClient(baseAddress);
            client.SetBearerToken(token);
            var responce = await client.GetStringAsync("connection");
            "\n\nResponce:".ConsoleGreen();
            Console.WriteLine(JArray.Parse(responce));
        }

        static async Task GetVersion(string token)
        {
            var baseAddress = "http://83.217.24.133:21150";
            var client = GetHttpClient(baseAddress);
            client.SetBearerToken(token);
            var responce = await client.GetStringAsync("version/check?version=0.9");
            "\n\nResponce:".ConsoleGreen();
            Console.WriteLine(JObject.Parse(responce));
        }

        static async Task<TokenResponse> RequestTokenAsync()
        {
            var client = GetHttpClient("http://79.137.221.35:21150");
            var disco = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = "http://79.137.221.35:21150",
                Policy =
                {
                    RequireHttps = false
                }
            });
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
            var baseAddress = "https://83.217.24.133:5001";
            var client = GetHttpClient(baseAddress);
            client.SetBearerToken(token);
            var responce = await client.GetStringAsync("identity");
            "\n\nService claims:".ConsoleGreen();
            Console.WriteLine(JArray.Parse(responce));
        }
    }
}
