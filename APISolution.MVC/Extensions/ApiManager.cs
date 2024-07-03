using APISolution.MVC.Models;
using APISolution.MVC.URI;
using Newtonsoft.Json;

namespace APISolution.MVC.Extensions
{
    public class ApiManager
    {
        private readonly HttpClient _httpClient;
        private readonly SettingURI _uriManager;

        public ApiManager(HttpClient httpClient, SettingURI uriManager)
        {
            _httpClient = httpClient;
            _uriManager = uriManager;
        }

        public async Task<ModelAPi> GetApiDataAsync(string endpoint)
        {
            var fullUri = _uriManager.GetFullUri(endpoint);
            var response = await _httpClient.GetAsync(fullUri);
            response.EnsureSuccessStatusCode();

            var jsonData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ModelAPi>(jsonData);
        }
    }
}
