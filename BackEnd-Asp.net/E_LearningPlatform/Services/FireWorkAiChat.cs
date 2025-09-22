using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace E_LearningPlatform.Services
{
    public class FireWorkAiChat
    {
       
            private readonly HttpClient _httpClient;
            private readonly string _apiKey;
            private readonly string _endpoint;
            private readonly string _modelName;

            public FireWorkAiChat(string apiKey, string endpoint, string modelName)
            {
                _httpClient = new HttpClient();
                _apiKey = apiKey;
                _endpoint = endpoint;
                _modelName = modelName;
            }

            public async Task<string> AskAiAsync(string prompt)
            {
                // Add Authorization header
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _apiKey);

                // Prepare request body
                var requestBody = new
                {
                    model = _modelName,
                    messages = new[]
                    {
                new { role = "user", content = prompt }
            }
                };

                // Serialize to JSON
                var jsonContent = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Send POST request
                var response = await _httpClient.PostAsync(_endpoint, content);

            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("🔥 Response Content: " + responseContent); // Add this

            var chatResponse = JsonSerializer.Deserialize<ChatResponse>(responseContent);

            return chatResponse?.ToString() ?? "[No response from AI]";
         
        }
        }
    public class ChatResponse
    {
        [JsonPropertyName("choices")]
        public List<Choice> Choices { get; set; }

        public override string ToString()
        {
            return Choices?.FirstOrDefault()?.Message?.Content ?? "[Empty]";
        }
    }

    public class Choice
    {
        [JsonPropertyName("message")]
        public Message Message { get; set; }
    }

    public class Message
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }
    }


}

