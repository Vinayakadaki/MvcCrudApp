using System.Text;
using System.Text.Json;

namespace MvcCrudApp.Services
{
    public class LogicAppService
    {
        private readonly HttpClient _httpClient;

        public LogicAppService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SendLoginDetails(string username)
        {
            var payload = new
            {
                username = username,
                loginTime = DateTime.Now
            };

            var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json");

            await _httpClient.PostAsync(
                "YOUR_LOGIC_APP_URL",
                content);
        }
    }
}