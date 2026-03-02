namespace CustomerChat.Domain.Enums;

public enum ConversationStatus
{
    Open = 1,
    PendingAgent = 2,
    AssignedToAgent = 3,
    Resolved = 4,
    Closed = 5
}

public enum MessageType
{
    Text = 1,
    Image = 2,
    File = 3,
    SystemNotification = 4
}

public enum SenderType
{
    Customer = 1,
    Agent = 2,
    Bot = 3,
    System = 4
}

public enum AgentStatus
{
    Online = 1,
    Busy = 2,
    Away = 3,
    Offline = 4
}
