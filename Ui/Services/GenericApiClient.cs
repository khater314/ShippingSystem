using System.Net.Http.Json;
using System.Text.Json;

namespace Ui.Services
{
    using System.Net.Http.Json;
    using System.Text.Json;

    public class GenericApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public GenericApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ShippingApiClient");

            _options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        }

        // GET
        public async Task<T?> GetAsync<T>(string endpoint)
        {
            return await _httpClient.GetFromJsonAsync<T>(endpoint, _options);
        }

        // POST
        public async Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, data, _options);
            return await HandleResponse<TResponse>(response);
        }

        // PUT: Whole Update
        public async Task<TResponse?> PutAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            var response = await _httpClient.PutAsJsonAsync(endpoint, data, _options);
            return await HandleResponse<TResponse>(response);
        }

        // 4. PATCH: Partial Update
        public async Task<TResponse?> PatchAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            // بنستخدم PatchAsJsonAsync المتاحة في الدوت نت الحديث
            var response = await _httpClient.PatchAsJsonAsync(endpoint, data, _options);
            return await HandleResponse<TResponse>(response);
        }

        // DELETE
        public async Task<bool> DeleteAsync(string endpoint)
        {
            var response = await _httpClient.DeleteAsync(endpoint);
            return response.IsSuccessStatusCode;
        }

        private async Task<T?> HandleResponse<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>(_options);
            }
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"API Error: {response.StatusCode} - {error}");
        }
    
    }
}
