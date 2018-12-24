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
            string IdentityAddress = "https://79.137.221.35:21150";
            string ApiAddress = "https://83.217.24.133:21150";
            try
            {
                var response = await RequestTokenAsync(IdentityAddress);
                response.Show();
                Console.ReadLine();
                await GetVersion(response.AccessToken, ApiAddress);
                await GetConnectionData(response.AccessToken, ApiAddress);
                Console.ReadLine();
            }
            catch (Exception e)
            {
                e.Message.ConsoleRed();
                Console.ReadLine();
            }
        }

        static HttpClient GetHttpClient(string address, HttpClientHandler handler)
        {
            var client = new HttpClient(handler);
            client.BaseAddress = new Uri( address);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        static async Task GetConnectionData(string token, string address)
        {
            using (var handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                var client = GetHttpClient(address, handler);
                client.SetBearerToken(token);
                var responce = await client.GetStringAsync("connection");
                "\n\nResponce:".ConsoleGreen();
                Console.WriteLine(JObject.Parse(responce));
            }
        }

        static async Task GetVersion(string token, string address)
        {
            using (var handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                var client = GetHttpClient(address, handler);
                client.SetBearerToken(token);
                var responce = await client.GetStringAsync("version/check?version=0.9");
                "\n\nResponce:".ConsoleGreen();
                Console.WriteLine(JObject.Parse(responce));
            }
        }

        static async Task<TokenResponse> RequestTokenAsync(string address)
        {
            using (var handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                var client = GetHttpClient(address, handler);
                var disco = await client.GetDiscoveryDocumentAsync(address);
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
        }
    }
}
