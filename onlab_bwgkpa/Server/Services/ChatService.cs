using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Server.Models;


namespace Server.Services
{
    public class ChatService : IChatService
    {
        private readonly HttpClient _httpClient;

        public ChatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://ai:5555"); // AI WebAPI címe
        }

        public async Task<HRResponse> SendPromptAsync(ChatRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Chat", request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<HRResponse>();
            return result!;
        }
    }

    //public class ChatService : IChatService
    //{
    //    private readonly HttpClient _httpClient;

    //    public ChatService(HttpClient httpClient)
    //    {
    //        _httpClient = httpClient;
    //        _httpClient.BaseAddress = new Uri("http://localhost:5555");
    //    }

    //    public async Task<string> SendPromptAsync(ChatRequest request)
    //    {

    //        var response = await _httpClient.PostAsJsonAsync("api/Chat", request);
    //        if (response.IsSuccessStatusCode)
    //        {
    //            var result = await response.Content.ReadFromJsonAsync<HRResponse>();
    //            return result?.Text ?? string.Empty;
    //        }
    //        else
    //        {
    //            var error = await response.Content.ReadAsStringAsync();
    //            throw new Exception($"Error calling AI service: {error}");
    //        }
    //    }
    //}
    //public class ChatService : IChatService
    //{
    //    private readonly HttpClient _httpClient;

    //    public ChatService(HttpClient httpClient)
    //    {
    //        _httpClient = httpClient;
    //    }

    //    public async Task<string> SendPromptAsync(ChatRequest request)
    //    {
    //        var jsonContent = JsonSerializer.Serialize(request);
    //        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

    //        var response = await _httpClient.PostAsync("http://localhost:5555/api/Chat", content);

    //        response.EnsureSuccessStatusCode();

    //        var responseContent = await response.Content.ReadAsStringAsync();
    //        var chatResponse = JsonSerializer.Deserialize<string>(responseContent);

    //        return chatResponse ?? "Hiba történt az AI válasz feldolgozásakor.";
    //    }
    //}
}
