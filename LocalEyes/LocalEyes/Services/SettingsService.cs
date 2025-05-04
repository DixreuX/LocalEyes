using Microsoft.Extensions.Configuration;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace LocalEyes.Services
{
    public class SettingsService
    {

        private readonly IConfiguration _configuration;
        private readonly string _appSettingsPath;

        public string BaseURL { get; private set; }
        public string Key { get; private set; }


        public SettingsService(IConfiguration configuration)
        {
            _configuration = configuration;

            BaseURL = _configuration["API:BaseURL"];
            Key = _configuration["API:Key"];

            _appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
        }

        public void UpdateSettings(string baseURL, string key)
        {
            BaseURL = baseURL;
            Key = key;

            var json = File.ReadAllText(_appSettingsPath);
            var jsonObject = JsonNode.Parse(json);

            if (jsonObject != null)
            {
                jsonObject["API"]["BaseURL"] = baseURL;
                jsonObject["API"]["Key"] = key;

                File.WriteAllText(_appSettingsPath, jsonObject.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));
            }
        }
    }
}
