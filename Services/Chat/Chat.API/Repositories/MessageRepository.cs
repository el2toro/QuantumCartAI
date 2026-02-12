namespace Chat.API.Repositories;

public interface IMessageRepository
{
    Task<string> SendMessage(string message, CancellationToken cancellationToken);
}

public class MessageRepository : IMessageRepository
{
    public async Task<string> SendMessage(string message, CancellationToken cancellationToken)
    {
        return await Task.FromResult(message);
    }
}
