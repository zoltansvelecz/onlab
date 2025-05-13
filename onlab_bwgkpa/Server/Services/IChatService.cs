using System.Threading.Tasks;
using Server.Models;

namespace Server.Services
{
    public interface IChatService
    {
        Task<HRResponse> SendPromptAsync(ChatRequest request);
    }
}

