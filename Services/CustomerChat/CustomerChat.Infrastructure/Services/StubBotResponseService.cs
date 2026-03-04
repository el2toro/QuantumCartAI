using CustomerChat.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace CustomerChat.Infrastructure.Services;

/// <summary>
/// Stub bot service. Replace with an actual LLM call (OpenAI, Azure OpenAI, etc.)
/// by implementing IBotResponseService without touching any other layer.
/// </summary>
public sealed class StubBotResponseService(
    ILogger<StubBotResponseService> logger) : IBotResponseService
{
    private static readonly string[] _greetings =
    [
        "Hi! I'm your ecommerce assistant. How can I help you today?",
        "Hello! How can I assist you with your order or account?",
        "Welcome! What can I help you with?"
    ];

    public Task<string?> GenerateResponseAsync(
        Guid conversationId,
        string userMessage,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Bot generating response for conversation {ConversationId}", conversationId);

        var response = userMessage.ToLower() switch
        {
            var m when m.Contains("order") =>
                "I'd be happy to help with your order! Could you please provide your order number?",
            var m when m.Contains("return") || m.Contains("refund") =>
                "For returns and refunds, please provide your order number and reason. We process refunds within 5-7 business days.",
            var m when m.Contains("track") || m.Contains("shipping") =>
                "To track your shipment, please share your order number and I'll look that up for you.",
            var m when m.Contains("hello") || m.Contains("hi") || m.Contains("hey") =>
                _greetings[Random.Shared.Next(_greetings.Length)],
            var m when m.Contains("agent") || m.Contains("human") || m.Contains("person") =>
                null, // Return null to trigger agent handoff request
            _ =>
                "I understand your concern. Let me connect you with the right information. Could you share more details?"
        };

        return Task.FromResult(response);
    }
}
