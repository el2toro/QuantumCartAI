using BuildingBlocks.CQRS;
using Chat.API.Repositories;

namespace Chat.API.Messages.SendMessage;

public record SendMessageCommand(string Message) : ICommand<SendMessageResult>;
public record SendMessageResult();

public class SendMessageHandler(IMessageRepository messageRepository)
    : ICommandHandler<SendMessageCommand, SendMessageResult>
{
    public async Task<SendMessageResult> Handle(SendMessageCommand command, CancellationToken cancellationToken)
    {
        await messageRepository.SendMessage(command.Message, cancellationToken);
        return new SendMessageResult();
    }
}
