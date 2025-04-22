using LocalEyes.Components.Pages;
using LocalEyesAPI.Helpers;
using System.Text.Json;
using static LocalEyes.Components.Pages.Reports;

namespace LocalEyes.Services
{
    public class ReportService
    {

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ReportService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            // Set the base address from appsettings.json
            _httpClient.BaseAddress = new Uri(_configuration["API:BaseUrl"]);
        }

        public async Task<List<Report>> GetReportsAsync()
        {
            var encryptionHelper = new EncryptionHelper(_configuration);

            var apiKey = encryptionHelper.EncryptKey();

            _httpClient.DefaultRequestHeaders.Add("APIKey", apiKey);

            var response = await _httpClient.GetAsync("Report/Reports");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<List<Report>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            throw new Exception($"Failed to fetch reports: {response.ReasonPhrase}");
        }
    }
}
