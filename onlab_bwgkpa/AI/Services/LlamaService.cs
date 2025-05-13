using AI.Models;
using LLama;
using LLama.Common;
using LLama.Sampling;
using System.Text;
using static LLama.LLamaTransforms;

namespace AI.Services
{
    public class LlamaService : ILlamaService, IDisposable
    {
        private readonly ChatSession _session;
        private readonly LLamaContext _context;
        private readonly ILogger<LlamaService> _logger;

        public LlamaService(IConfiguration configuration, ILogger<LlamaService> logger)
        {
            _logger = logger;

            var @params = new ModelParams(configuration["ModelPath"]!)
            {
                ContextSize = 512,
            };

            using var weights = LLamaWeights.LoadFromFile(@params);

            _context = new LLamaContext(weights, @params, logger);

            _session = new ChatSession(new InteractiveExecutor(_context))
                .WithOutputTransform(new KeywordTextOutputStreamTransform(["User:", "Assistant:"], redundancyLength: 8))
                .WithHistoryTransform(new HistoryTransform());
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<HistoryOutput> SendAsync(ChatHistory input)
        {
            _logger.LogInformation($"Sending chat history: {input.ToJson()}");

            var result = _session.ChatAsync(input, new InferenceParams()
            {
                SamplingPipeline = new DefaultSamplingPipeline() { RepeatPenalty = 1.0f },
                AntiPrompts = ["User:"],
                MaxTokens = 250,
            });

            var sb = new StringBuilder();

            await foreach (var r in result)
            {
                _logger.LogDebug(r);
                sb.Append(r);
            }

            var text = sb.ToString().Trim((char)65533).Trim();
            _logger.LogInformation(text);

            return new HistoryOutput { Text = text };
        }

        public async IAsyncEnumerable<string> SendStreamAsync(ChatHistory input)
        {
            _logger.LogInformation($"Sending chat history: {input.ToJson()}");

            var result = _session.ChatAsync(input, new InferenceParams()
            {
                SamplingPipeline = new DefaultSamplingPipeline() { RepeatPenalty = 1.0f },
                AntiPrompts = ["User:"],
                MaxTokens = 250,
            });

            var sb = new StringBuilder();

            await foreach (var r in result)
            {
                var text = r.Trim((char)65533);
                _logger.LogDebug(text);
                sb.Append(text);
                yield return text;
            }

            _logger.LogInformation(sb.ToString());
        }

        public class HistoryTransform : DefaultHistoryTransform
        {
            public override string HistoryToText(ChatHistory history)
            {
                return base.HistoryToText(history) + "\n Assistant:";
            }

        }
    }
}
