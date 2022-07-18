using Newtonsoft.Json;
using Patika.Shared.DTO.Identity;
using Patika.Shared.Entities;
using Patika.Shared.Exceptions.AccountDomain;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Patika.Shared.Services
{
	public class HttpClientService
    {
        private string BaseAPIUrl { get; }
        HttpClient HttpClient { get; }
        ClientAuthenticationParams AuthenticationParams { get; } = null;
        public AuthenticationHeaderValue Authorization { get => HttpClient.DefaultRequestHeaders.Authorization; set => HttpClient.DefaultRequestHeaders.Authorization = value; }
        public HttpRequestHeaders RequestHeaders => HttpClient.DefaultRequestHeaders;
        public HttpClientService(string baseAPIUrl, ClientAuthenticationParams authenticationParams = null)
        {
            if (!baseAPIUrl.EndsWith("/"))
                baseAPIUrl += "/";
            AuthenticationParams = authenticationParams ?? new ClientAuthenticationParams();
            BaseAPIUrl = baseAPIUrl;
            HttpClient = GetHttpClient();
        }

        public Task SetTokenAsync(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    throw new UserIsUnauthorizedException();
                }
                RequestHeaders.Remove("Authorization");
                RequestHeaders.Add("Authorization", $"Bearer {token.Replace("Bearer", "").Trim()}");
                return Task.CompletedTask;
            }
            catch
            {
                throw;
            }
        }


        public async Task AcquireTokenAsync(ClientAuthenticationParams authenticationParams = null)
        {
            authenticationParams ??= AuthenticationParams;
            if (!authenticationParams.AuthServer.EndsWith("/"))
                authenticationParams.AuthServer += "/";
            try
            {
                var response = await HttpClient.PostAsJsonAsync(GetUrl("identity/connect/token"), new BasicLoginInputDto
                {
                    Password = authenticationParams.ClientSecret,
                    PhoneNumber = authenticationParams.ClientId
                });

                var payload = await response.Content.ReadFromJsonAsync<TokenResultDto>();

                if (string.IsNullOrEmpty(payload.Token))
                {
                    throw new InvalidOperationException("An error occurred while retrieving an access token.");
                }
                RequestHeaders.Remove("Authorization");
                RequestHeaders.Add("Authorization", $"Bearer {payload.Token}");
            }
            catch (Exception)
			{
                throw;
            }
        }

        public async Task<string> HttpGetAsJson(string apiPath)
        {
            var response = await HttpClient.GetAsync(GetUrl(apiPath));
            return await response.Content.ReadAsStringAsync();
        }


        public async Task<T> HttpGetAs<T>(string apiPath)
        {
            var response = await HttpClient.GetFromJsonAsync<T>(GetUrl(apiPath));
            return response;
        }
        public async Task<T> HttpPostJson<T>(string apiPath, object value)
        {
            var response = await HttpClient.PostAsJsonAsync(GetUrl(apiPath), value);
            if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
            return await ReadAsAsync<T>(response.Content);
        }
        public async Task<HttpResponseMessage> HttpDeleteAsync(string apiPath) => await HttpClient.DeleteAsync(GetUrl(apiPath));
        public async Task<HttpResponseMessage> HttpPostJson(string apiPath, object value)
        {
            return await HttpClient.PostAsJsonAsync(GetUrl(apiPath), value);
        }
        public async Task<T> HttpPostFormContentAsync<T>(string apiPath, HttpContent value)
        {
            var response = await HttpClient.PostAsync(GetUrl(apiPath), value);
            return await ReadAsAsync<T>(response.Content);
        }

        private static async Task<T> ReadAsAsync<T>(HttpContent content)
        {
            var json = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }
        private HttpClient GetHttpClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(BaseAPIUrl)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
                );
            return client;
        }

        private string GetUrl(string apiPath) => $"{BaseAPIUrl}{(apiPath.StartsWith("/") ? apiPath[1..] : apiPath)}";

    }
}