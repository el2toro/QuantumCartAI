namespace CustomerChat.Domain.Exceptions;

/// <summary>
/// Thrown when a domain rule is violated.
/// These are always translated to 400 Bad Request at the API boundary.
/// </summary>
public sealed class DomainException(string message) : Exception(message);

/// <summary>
/// Thrown when an entity requested by ID does not exist.
/// </summary>
public sealed class NotFoundException(string entityName, object key)
    : Exception($"{entityName} with key '{key}' was not found.");
