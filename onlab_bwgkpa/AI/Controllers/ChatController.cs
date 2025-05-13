using AI.Models;
using AI.Services;
using LLama.Common;
using Microsoft.AspNetCore.Mvc;

namespace AI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;
        private readonly ILlamaService _service;

        public ChatController(ILogger<ChatController> logger, ILlamaService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(HistoryOutput), StatusCodes.Status200OK, "application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<HistoryOutput>> Post([FromBody] HistoryInput input)
        {
            var history = ConvertHistory(input);
            return await _service.SendAsync(history);
        }

        [HttpPost("Stream")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK, "text/event-stream")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task PostStream([FromBody] HistoryInput input, CancellationToken cancellationToken)
        {
            var history = ConvertHistory(input);

            Response.ContentType = "text/event-stream";
            await foreach (var r in _service.SendStreamAsync(history))
            {
                await Response.WriteAsync(r, cancellationToken);
                await Response.Body.FlushAsync(cancellationToken);
            }
            await Response.CompleteAsync();
        }

        private static ChatHistory ConvertHistory(HistoryInput input)
        {
            var history = new ChatHistory();
            var messages = input.Messages.Select(m => new ChatHistory.Message(Enum.Parse<AuthorRole>(m.Role.ToString()), m.Content));
            history.Messages.AddRange(messages);
            return history;
        }
    }
}