using CustomerChat.Domain.Repositories;
using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace CustomerChat.Infrastructure.Services.AI;

/// <summary>
/// Semantic Kernel "Plugin" — these are tools the AI can autonomously decide
/// to call during a conversation. The AI sees the descriptions and picks the
/// right tool based on what the customer asks.
///
/// This is "function calling" / "tool use" in action.
/// </summary>
public sealed class EcommercePlugin(IUnitOfWork unitOfWork)
{
    /// <summary>
    /// The AI calls this when a customer asks about their order status.
    /// </summary>
    [KernelFunction("get_order_status")]
    [Description("Gets the status of a customer order. Call this when the customer asks about their order, delivery, or shipment.")]
    public async Task<string> GetOrderStatusAsync(
        [Description("The order number provided by the customer")] string orderNumber)
    {
        // TODO: Replace with real order service call
        // e.g. var order = await orderService.GetByNumberAsync(orderNumber);
        await Task.Delay(10); // Simulate async call

        // Stub response — wire to your real Orders microservice
        return orderNumber.StartsWith("ORD")
            ? $"Order {orderNumber} is currently in transit and expected to arrive within 2-3 business days."
            : $"Order {orderNumber} was not found. Please double-check the order number from your confirmation email.";
    }

    /// <summary>
    /// The AI calls this to initiate a return request.
    /// </summary>
    [KernelFunction("initiate_return")]
    [Description("Initiates a return or refund request for an order. Call this when the customer wants to return a product or get a refund.")]
    public async Task<string> InitiateReturnAsync(
        [Description("The order number to return")] string orderNumber,
        [Description("The reason for the return")] string reason)
    {
        await Task.Delay(10);

        // Stub — wire to your real Returns microservice
        var returnId = $"RET-{Guid.NewGuid().ToString()[..8].ToUpper()}";
        return $"Return request {returnId} has been created for order {orderNumber}. " +
               $"Reason: {reason}. You'll receive a prepaid return label within 24 hours.";
    }

    /// <summary>
    /// The AI calls this when the customer explicitly wants a human agent.
    /// Returning a special sentinel value lets the bot service detect handoff.
    /// </summary>
    [KernelFunction("request_human_agent")]
    [Description("Transfers the customer to a live human support agent. Call this when the customer asks for a human, agent, or person, or when the issue is too complex.")]
    public Task<string> RequestHumanAgentAsync(
        [Description("Brief summary of what the customer needs help with")] string issueSummary)
    {
        // Returning this sentinel tells SemanticKernelBotService to return null,
        // which triggers agent handoff in the Application layer
        return Task.FromResult($"__HANDOFF__:{issueSummary}");
    }

    /// <summary>
    /// The AI calls this to look up product information.
    /// </summary>
    [KernelFunction("get_product_info")]
    [Description("Gets information about a product including availability, price, and description.")]
    public async Task<string> GetProductInfoAsync(
        [Description("Product name or ID to look up")] string productNameOrId)
    {
        await Task.Delay(10);

        // Stub — wire to your real Product catalog
        return $"Here's what I found for '{productNameOrId}': " +
               "This product is currently in stock. Price: $49.99. " +
               "Standard shipping applies (3-5 business days).";
    }
}
