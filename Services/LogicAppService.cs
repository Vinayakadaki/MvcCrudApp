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
                "https://prod-14.centralindia.logic.azure.com:443/workflows/40164cbd2edf4adba36793746875e5be/triggers/When_an_HTTP_request_is_received/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2FWhen_an_HTTP_request_is_received%2Frun&sv=1.0&sig=CcrpZYf_J_XbpxMNoEFV3s_BMHc25FLXv2qAeSSQzVs",
                content);
        }
    }
}