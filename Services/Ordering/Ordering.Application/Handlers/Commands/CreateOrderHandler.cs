using BuildingBlocks.CQRS;
using Mapster;
using MassTransit;
using Ordering.Application.DTOs;
using Ordering.Application.Interfaces;
using Ordering.Domain.Entities;
using Ordering.Domain.Events;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Handlers.Commands;

public record CreateOrderCommand(
    Guid CustomerId,
    Address ShippingAddress,
    Address BillingAddress,
    string OrderNumber,
    string Currency,
    string CustomerNotes,
    List<OrderItemDto> OrderItems) : ICommand<CreateOrderResult>;
public record CreateOrderResult(OrderDetailsDto Order);
public class CreateOrderHandler(IOrderingRepository orderingRepository,
    IPublishEndpoint publishedEndpoint)
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {


        var order = Order.CreateDraft(CustomerId.Of(command.CustomerId.ToString()),
            OrderNumber.Of(command.OrderNumber),
            command.ShippingAddress,
            command.BillingAddress,
            Currency.EUR,
            command.CustomerNotes);

        foreach (var item in command.OrderItems)
        {
            //order.AddOrderItem(item);
        }

        var createdOrder = await orderingRepository.CreateOrderAsync(order, cancellationToken);

        await publishedEndpoint.Publish<OrderCreatedEvent>("result", cancellationToken);

        var result = createdOrder.Adapt<OrderDetailsDto>();

        return new CreateOrderResult(result);
    }
}
