using System.ComponentModel.DataAnnotations;

namespace CustomerChat.Infrastructure.Services.AI;

/// <summary>
/// Strongly-typed configuration for Azure OpenAI.
/// Bound from appsettings.json "AzureOpenAI" section.
/// Using options pattern keeps config validated at startup, not at runtime.
/// </summary>
public sealed class AzureOpenAIOptions
{
    public const string SectionName = "AzureOpenAI";

    [Required]
    public string Endpoint { get; set; } = string.Empty;

    [Required]
    public string ApiKey { get; set; } = string.Empty;

    [Required]
    public string DeploymentName { get; set; } = string.Empty;

    /// <summary>
    /// The underlying model (e.g. gpt-4o, gpt-4-turbo).
    /// Used for logging and diagnostics.
    /// </summary>
    public string ModelId { get; set; } = "gpt-4o";

    /// <summary>
    /// When true, use StubBotResponseService instead.
    /// Useful for local dev without Azure access.
    /// </summary>
    public bool UseStub { get; set; } = false;
}
