using Cart.Domain.Events;
using Cart.Domain.Interfaces;
using System.Text;
using System.Text.Json;
using EventStore.Client;

namespace Cart.Infrastructure.Repositories;

public class EventStoreCartRepository : ICartRepository
{
    private readonly EventStoreClient _client;
    private const string StreamPrefix = "cart-";

    public EventStoreCartRepository(EventStoreClient client)
    {
        _client = client;
    }

    public async Task<Domain.Entities.Cart?> GetByIdAsync(Guid cartId)
    {
        var streamName = $"{StreamPrefix}{cartId}";
        var result = _client.ReadStreamAsync(Direction.Forwards, streamName, StreamPosition.Start);

        if (await result.ReadState == ReadState.StreamNotFound)
            return null;

        var cart = new Domain.Entities.Cart(new Domain.ValueObjects.CartId(cartId));
        var events = new List<IDomainEvent>();

        await foreach (var resolvedEvent in result)
        {
            var domainEvent = Deserialize(resolvedEvent.Event);
            events.Add(domainEvent);
        }

        if (events.Any())
            cart.LoadFromHistory(events);

        return cart;
    }

    public async Task SaveAsync(Domain.Entities.Cart cart)
    {
        var streamName = $"{StreamPrefix}{cart.Id}";
        var events = cart.UncommittedEvents
            .Select(ToEventData)
            .ToList();

        if (!events.Any()) return;

        var expectedVersion = cart.Version == 0
            ? StreamRevision.None
            : StreamRevision.FromInt64(cart.Version - events.Count);

        await _client.AppendToStreamAsync(
            streamName,
            expectedVersion,
            events);

        cart.MarkCommitted();
    }

    private static EventData ToEventData(IDomainEvent domainEvent)
    {
        var eventId = Uuid.FromGuid(Guid.NewGuid());
        var typeName = domainEvent.GetType().Name;
        var json = JsonSerializer.Serialize(domainEvent, domainEvent.GetType());
        var data = Encoding.UTF8.GetBytes(json);

        var metadata = new Dictionary<string, string>
        {
            ["eventClrType"] = domainEvent.GetType().AssemblyQualifiedName!
        };
        var metadataJson = JsonSerializer.Serialize(metadata);
        var metadataBytes = Encoding.UTF8.GetBytes(metadataJson);

        return new EventData(eventId, typeName, data, metadataBytes);
    }

    private static IDomainEvent Deserialize(EventRecord eventRecorded)
    {
        var metadataJson = Encoding.UTF8.GetString(eventRecorded.Metadata.Span);
        var metadata = JsonSerializer.Deserialize<Dictionary<string, string>>(metadataJson)!;
        var clrType = metadata["eventClrType"];

        var eventType = Type.GetType(clrType)
            ?? throw new InvalidOperationException($"Cannot deserialize event type {clrType}");

        var json = Encoding.UTF8.GetString(eventRecorded.Data.Span);
        return (IDomainEvent)JsonSerializer.Deserialize(json, eventType)!;
    }
}
