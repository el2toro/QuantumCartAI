using CustomerChat.Application.Common.Interfaces;
using CustomerChat.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace CustomerChat.Infrastructure.Services.AI;

/// <summary>
/// Real AI bot powered by Semantic Kernel + Azure OpenAI.
/// Replaces StubBotResponseService — zero changes needed in Application layer
/// because both implement IBotResponseService.
///
/// Features:
/// - Multi-turn conversation memory per session
/// - Function calling (AI autonomously uses ecommerce tools)
/// - Graceful handoff detection
/// - Token usage logging for cost tracking
/// </summary>
public sealed class SemanticKernelBotService(
    Kernel kernel,
    ConversationHistoryStore historyStore,
    IUnitOfWork unitOfWork,
    ILogger<SemanticKernelBotService> logger,
    IOptions<AzureOpenAIOptions> options) : IBotResponseService
{
    // The system prompt defines the AI's personality, scope, and constraints.
    // This is the most important thing to tune for your ecommerce brand.
    private const string SystemPrompt = """
        You are a friendly and professional customer support assistant for an ecommerce store.
        
        Your responsibilities:
        - Help customers with orders, returns, refunds, shipping, and product questions
        - Always be polite, empathetic, and concise
        - Use the available tools to look up real order/product information
        - If a customer is frustrated or the issue is complex, offer to connect them with a human agent
        - Never make up order statuses or pricing — always use tools to get real data
        - Keep responses under 3 sentences when possible
        
        When a customer asks to speak to a human or agent, immediately use the request_human_agent tool.
        
        You do NOT help with: account passwords, payment disputes (escalate those), or anything outside ecommerce support.
        """;

    public async Task<string?> GenerateResponseAsync(
        Guid conversationId,
        string userMessage,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Generating AI response for conversation {ConversationId}, message length: {Length}",
            conversationId, userMessage.Length);

        try
        {
            // Get or create the conversation history (multi-turn memory)
            var history = historyStore.GetOrCreate(conversationId, SystemPrompt);
            historyStore.AddUserMessage(conversationId, userMessage);

            var chatService = kernel.GetRequiredService<IChatCompletionService>();

            // Enable automatic function calling — AI decides which tools to use
            var executionSettings = new AzureOpenAIPromptExecutionSettings
            {
                ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
                MaxTokens = 500,        // Keep responses concise
                Temperature = 0.3,      // Low = more consistent, factual responses
                TopP = 0.95
            };

            var response = await chatService.GetChatMessageContentAsync(
                history,
                executionSettings,
                kernel,
                cancellationToken);

            var responseText = response.Content ?? string.Empty;

            logger.LogInformation(
                "AI response generated for {ConversationId}. Tokens used: {Tokens}",
                conversationId,
                response.Metadata?.GetValueOrDefault("Usage")?.ToString() ?? "unknown");

            // Check if AI decided to trigger a human handoff via the plugin
            if (responseText.Contains("__HANDOFF__"))
            {
                logger.LogInformation(
                    "AI requested agent handoff for conversation {ConversationId}", conversationId);

                // Clean up history — conversation will be taken over by a human
                historyStore.Remove(conversationId);

                // Return null → Application layer interprets this as "trigger handoff"
                return null;
            }

            // Store AI response in history for next turn
            historyStore.AddAssistantMessage(conversationId, responseText);

            return responseText;
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "AI response generation failed for conversation {ConversationId}", conversationId);

            // Graceful degradation — fall back to a safe canned response
            // rather than crashing the entire conversation
            return "I'm having trouble processing your request right now. " +
                   "Would you like me to connect you with a support agent?";
        }
    }
}
