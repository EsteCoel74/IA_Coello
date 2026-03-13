using System.Net.Http;
using System.Text.Json;

namespace IA_Coello.Services
{
    public class ChatGptService
    {
        private readonly HttpClient _httpClient;
        private readonly List<Message> _conversationHistory;
        private const string ApiUrl = "https://models.inference.ai.azure.com/chat/completions";

        public ChatGptService(string apiKey)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            _conversationHistory = new List<Message>();
        }

        public async Task<string> SendMessageAsync(string userMessage)
        {
            _conversationHistory.Add(new Message { Role = "user", Content = userMessage });

            var messages = _conversationHistory.Select(m => new
            {
                role = m.Role,
                content = m.Content
            }).ToList();

            var request = new
            {
                model = "gpt-4o-mini",
                messages = messages,
                temperature = 0.7
            };

            try
            {
                var jsonContent = JsonSerializer.Serialize(request);
                var httpContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(ApiUrl, httpContent);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Erreur API: {response.StatusCode} - {responseContent}");
                }

                var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseContent);
                var assistantMessage = jsonResponse.GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString() ?? string.Empty;

                _conversationHistory.Add(new Message { Role = "assistant", Content = assistantMessage });

                return assistantMessage;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la communication avec l'API: {ex.Message}", ex);
            }
        }

        public void ClearHistory()
        {
            _conversationHistory.Clear();
        }

        public List<Message> GetHistory()
        {
            return new List<Message>(_conversationHistory);
        }
    }

    public class Message
    {
        public string Role { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}