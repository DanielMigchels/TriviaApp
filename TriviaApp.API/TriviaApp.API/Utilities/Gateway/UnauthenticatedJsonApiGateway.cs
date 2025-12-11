using System;
using System.Text.Json;

namespace TriviaApp.API.Utilities.Gateway
{
    public abstract class UnauthenticatedJsonApiGateway(HttpClient client)
    {
        protected abstract string BaseUrl { get; }

        public async Task<T> Get<T>(string action = "")
        {
            var url = $"{BaseUrl.TrimEnd('/')}/{action.TrimStart('/')}".TrimEnd('/');
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseString) ?? throw new InvalidOperationException("Deserialization returned null.");
        }

        public async Task<T> Post<T, T2>(T2 body, string action = "")
        {
            var url = $"{BaseUrl.TrimEnd('/')}/{action.TrimStart('/')}".TrimEnd('/');

            var content = new StringContent(JsonSerializer.Serialize(body), System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseString) ?? throw new InvalidOperationException("Deserialization returned null.");
        }
    }
}
