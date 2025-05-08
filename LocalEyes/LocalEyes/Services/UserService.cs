using LocalEyes.Components.Pages;
using LocalEyes.Data;
using LocalEyes.Shared.Helpers;
using LocalEyes.Shared.Models;

namespace LocalEyes.Services
{
    public class UserService
    {

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        private EncryptionHelper _encryptionHelper;
        private string _apiKeyRaw = "";
        private string _apiKeyEncrypted = "";

        public UserService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            _apiKeyRaw = _configuration["API:Key"];
            _encryptionHelper = new EncryptionHelper(_apiKeyRaw);
            _apiKeyEncrypted = _encryptionHelper.EncryptKey();

            _httpClient.BaseAddress = new Uri(_configuration["API:BaseUrl"]);
            _httpClient.DefaultRequestHeaders.Add("APIKey", _apiKeyEncrypted);
        }

        public async Task<List<ApplicationUser>> GetUsersAsync()
        {
            var response = await _httpClient.GetAsync("User/Users");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch users: {response.ReasonPhrase}");
            }

            return await response.Content.ReadFromJsonAsync<List<ApplicationUser>>();
        }

        public async Task CreateUserAsync(NewUser newUser)
        {
            var response = await _httpClient.PostAsJsonAsync("User/CreateUser", newUser);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to create user: {response.ReasonPhrase}");
            }
        }

        public async Task CreateAPIUserAsync(NewAPIUser newAPIUser)
        {
            var response = await _httpClient.PostAsJsonAsync("User/CreateAPIUser", newAPIUser);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to create user: {response.ReasonPhrase}");
            }
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            var response = await _httpClient.DeleteAsync($"User/Delete/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to delete user: {response.ReasonPhrase}");
            }
        }
    }
}
