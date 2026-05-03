using System.Text;
using System.Text.Json;
using PlatformsService.Dtos;

namespace PlatformsService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        // Method to send platform data to the CommandService via an HTTP POST request
        public async Task SendPlatformToCommand(PlatformReadDto platform)
        {
            // Serialize the platform data to JSON and prepare the HTTP content for the POST request
            // Create a StringContent object with the serialized platform data, specifying UTF-8 encoding and application/json media type
            var httpContent = new StringContent(
                JsonSerializer.Serialize(platform),
                Encoding.UTF8,
                "application/json");

            // Send a POST request to the CommandService with the platform data and check the response status
            var response = await _httpClient.PostAsync($"{_configuration["CommandService"]}", httpContent);

            // Log the result of the POST request to the console, indicating whether it was successful or not
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to CommandService was OK!");
            }
            else
            {
                Console.WriteLine("--> Sync POST to CommandService failed.");
            }
        }
    }
}