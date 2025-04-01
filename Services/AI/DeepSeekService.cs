// Services/AI/DeepSeekService.cs
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace VanRental.Web.Services.AI
{
    public class DeepSeekService : IDeepSeekService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "sk-2137f1ec44a547e2839e9627a5e24b85"; // Sostituisci con la tua API Key DeepSeek

        public DeepSeekService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.deepseek.com/v1/");
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", ApiKey);
        }

        public async Task<string> GetResponse(string prompt)
        {
            try
            {
                var request = new
                {
                    model = "deepseek-chat",
                    messages = new[] { new { role = "user", content = prompt } }
                };

                var response = await _httpClient.PostAsJsonAsync("chat/completions", request);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<DeepSeekResponse>();
                return result?.Choices?.FirstOrDefault()?.Message?.Content ?? "Risposta non disponibile";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore DeepSeek: {ex.Message}");
                return "Si è verificato un errore. Riprova più tardi.";
            }
        }

        // Classi per deserializzare la risposta JSON
        private class DeepSeekResponse
        {
            public required List<Choice> Choices { get; set; }
        }

        private class Choice
        {
            public required Message Message { get; set; }
        }

        private class Message
        {
            public required string Content { get; set; }
        }
    }
}