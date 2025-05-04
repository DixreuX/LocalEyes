using LocalEyes.Shared.Helpers;
using LocalEyes.Shared.Models;
using System.Text.Json;

namespace LocalEyes.Services
{
    public class MunicipalityService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        private EncryptionHelper _encryptionHelper;
        private string _apiKeyRaw = "";
        private string _apiKeyEncrypted = "";

        public MunicipalityService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            _apiKeyRaw = _configuration["API:Key"];
            _encryptionHelper = new EncryptionHelper(_apiKeyRaw);
            _apiKeyEncrypted = _encryptionHelper.EncryptKey();

            _httpClient.BaseAddress = new Uri(_configuration["API:BaseUrl"]);
            _httpClient.DefaultRequestHeaders.Add("APIKey", _apiKeyEncrypted);
        }

        public async Task<List<Municipality>> GetMunicipalitiesAsync()
        {
            var response = await _httpClient.GetAsync("Municipality/Municipalities");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Municipality>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            throw new Exception($"Failed to fetch municipalities: {response.ReasonPhrase}");
        }

        public async Task CreateMunicipality(Municipality municipality)
        {
            var response = await _httpClient.PostAsJsonAsync("Municipality/Create", municipality);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to create municipality: {response.ReasonPhrase}");
            }
        }

        public async Task UpdateMunicipality(Municipality municipality)
        {
            var response = await _httpClient.PutAsJsonAsync($"Municipality/Update/{municipality.Id}", municipality);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to update municipality: {response.ReasonPhrase}");
            }
        }

        public async Task DeleteMunicipality(Guid municipalityId)
        {
            var response = await _httpClient.DeleteAsync($"Municipality/Delete/{municipalityId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to delete municipality: {response.ReasonPhrase}");
            }
        }
    }
}
