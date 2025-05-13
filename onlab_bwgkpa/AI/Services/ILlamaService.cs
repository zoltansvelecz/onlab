using AI.Models;
using LLama.Common;

namespace AI.Services
{
    public interface ILlamaService
    {
        Task<HistoryOutput> SendAsync(ChatHistory input);
        IAsyncEnumerable<string> SendStreamAsync(ChatHistory input);
    }
}
