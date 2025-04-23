using LocalEyes.Components.Pages;
using LocalEyes.Shared.Helpers;
using LocalEyes.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Text.Json;
using static LocalEyes.Components.Pages.Reports;

namespace LocalEyes.Services
{
    public class ReportService
    {

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        private EncryptionHelper _encryptionHelper;
        private string _apiKeyRaw = "";
        private string _apiKeyEncrypted = "";

        public ReportService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            _apiKeyRaw = _configuration["API:Key"];
            _encryptionHelper = new EncryptionHelper(_apiKeyRaw);
            _apiKeyEncrypted = _encryptionHelper.EncryptKey();

            // Set the base address from appsettings.json
            _httpClient.BaseAddress = new Uri(_configuration["API:BaseUrl"]);
        }

        public async Task<List<Report>> GetReportsAsync()
        {
            _httpClient.DefaultRequestHeaders.Add("APIKey", _apiKeyEncrypted);

            var response = await _httpClient.GetAsync("Report/Reports");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<List<Report>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            throw new Exception($"Failed to fetch reports: {response.ReasonPhrase}");
        }

        public async Task<List<Shared.Models.Type>> GetTypesAsync()
        {
            _httpClient.DefaultRequestHeaders.Add("APIKey", _apiKeyEncrypted);

            var response = await _httpClient.GetAsync("Report/Types");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<List<Shared.Models.Type>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            throw new Exception($"Failed to fetch types: {response.ReasonPhrase}");
        }

        public async Task CreateReportAsync(Report report)
        {
            var response = await _httpClient.PostAsJsonAsync("Report/Create", report);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to create report: {response.ReasonPhrase}");
            }
        }

        public async Task UpdateReportAsync(Report report)
        {
            var response = await _httpClient.PutAsJsonAsync($"Report/Update/{report.Id}", report);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to update report: {response.ReasonPhrase}");
            }
        }

        public async Task<Report> GetReportByIdAsync(Guid reportId)
        {
            var response = await _httpClient.GetAsync($"Report/{reportId}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<Report>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            throw new Exception($"Failed to fetch report: {response.ReasonPhrase}");
        }


    }
}
