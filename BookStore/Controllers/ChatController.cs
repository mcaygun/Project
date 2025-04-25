using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ChatController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public class ChatRequest
        {
            public string Message { get; set; }
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] ChatRequest request)
        {
            var openAiApiKey = "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";  // Buraya kendi API anahtarınızı koyun.
            var url = "https://api.openai.com/v1/chat/completions";

            // Kullanıcıdan gelen mesaj
            var userMessage = request.Message ?? string.Empty;

            // OpenAI API isteğinin gövdesi
            var requestBody = new
            {
                model = "gpt-3.5-turbo", // veya "gpt-4" gibi model
                messages = new[]
                {
                    new { role = "system", content = "You are a helpful assistant." },
                    new { role = "user", content = userMessage }
                }
            };

            // HttpClient ile istek
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {openAiApiKey}");

            var jsonRequest = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            // ChatGPT'den gelen ham JSON döndürülüyor
            // Front-end bu JSON içinden cevap cümlesini alacak.
            return Content(responseBody, "application/json");
        }
    }
}
