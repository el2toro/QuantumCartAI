namespace Chat.API.Repositories;

public interface IMessageRepository
{
    Task SendMessage(string message, CancellationToken cancellationToken);
}

public class MessageRepository : IMessageRepository
{
    public Task SendMessage(string message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
