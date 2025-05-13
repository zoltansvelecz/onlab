using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HRController : ControllerBase
    {
        private readonly ILogger<HRController> _logger;
        private readonly IChatService _chatService;

        public HRController(ILogger<HRController> logger, IChatService chatService)
        {
            _logger = logger;
            _chatService = chatService;
        }

        [HttpPost("competences", Name = "AskForKeyCompetences")]
        [ProducesResponseType(typeof(HRResponse), StatusCodes.Status200OK, "application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<HRResponse> AskForKeyCompetences([FromBody] HRRequest request)
        {
            var prompt = $"List the key competencies from the following CV:\n\n{request.Cv}";

            var result = await _chatService.SendPromptAsync(new ChatRequest
            {
                Messages = new List<ChatMessage>
                {
                    new ChatMessage
                    {
                        Role = "user",
                        Content = prompt
                    }
                }
            });

            return new HRResponse { Text = result.Text };
        }

        [HttpPost("positions", Name = "AskForSuggestedPositions")]
        [ProducesResponseType(typeof(HRResponse), StatusCodes.Status200OK, "application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<HRResponse> AskForSuggestedPositions([FromBody] HRRequest request)
        {
            var prompt = $"Based on the following CV, suggest suitable job positions:\n\n{request.Cv}";

            var result = await _chatService.SendPromptAsync(new ChatRequest
            {
                Messages = new List<ChatMessage>
                {
                    new ChatMessage { Role = "user", Content = prompt }
                }
            });

            return new HRResponse { Text = result.Text };
        }

        [HttpPost("data", Name = "AskForDataTable")]
        [ProducesResponseType(typeof(HRResponse), StatusCodes.Status200OK, "application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<HRResponse> AskForDataTable([FromBody] HRRequest request)
        {
            var prompt = $"Create a table with the key details from the following CV:\n\n{request.Cv}";

            var result = await _chatService.SendPromptAsync(new ChatRequest
            {
                Messages = new List<ChatMessage>
                {
                    new ChatMessage { Role = "user", Content = prompt }
                }
            });

            return new HRResponse { Text = result.Text };
        }

        [HttpPost("questions", Name = "AskForInterviewQuestions")]
        [ProducesResponseType(typeof(HRResponse), StatusCodes.Status200OK, "application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<HRResponse> AskForInterviewQuestions([FromBody] HRRequest request)
        {
            var prompt = $"Generate possible interview questions based on the following CV:\n\n{request.Cv}";

            var result = await _chatService.SendPromptAsync(new ChatRequest
            {
                Messages = new List<ChatMessage>
                {
                    new ChatMessage { Role = "user", Content = prompt }
                }
            });

            return new HRResponse { Text = result.Text };
        }

        [HttpPost("invitation", Name = "AskForInvitationLetter")]
        [ProducesResponseType(typeof(HRResponse), StatusCodes.Status200OK, "application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<HRResponse> AskForInvitationLetter([FromBody] HRRequest request)
        {
            var prompt = $"Create an invitation letter for the owner of the following CV:\n\n{request.Cv}";

            var result = await _chatService.SendPromptAsync(new ChatRequest
            {
                Messages = new List<ChatMessage>
                {
                    new ChatMessage { Role = "user", Content = prompt }
                }
            });

            return new HRResponse { Text = result.Text };
        }
    }
}


