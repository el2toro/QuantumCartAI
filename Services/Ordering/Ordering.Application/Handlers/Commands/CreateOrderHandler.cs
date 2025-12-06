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
    AddressDto ShippingAddress,
    AddressDto BillingAddress,
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
        var address = new Address(command.ShippingAddress.Street,
            command.ShippingAddress.City,
            command.ShippingAddress.State,
            command.ShippingAddress.Country,
            command.ShippingAddress.ZipCode);

        var order = Order.CreateDraft(CustomerId.Of(command.CustomerId.ToString()),
            OrderNumber.Of(command.OrderNumber),
            address,
            address,
            Currency.EUR,
            command.CustomerNotes);

        foreach (var item in command.OrderItems)
        {
            order.AddOrderItem(ProductId.Of(item.ProductId.ToString()),
               item.ProductName,
               item.ProductImageUrl,
               item.ProductSku,
               item.UnitPrice,
               item.Quantity,
               item.Discount);
        }

        var createdOrder = await orderingRepository.CreateOrderAsync(order, cancellationToken);

        // await publishedEndpoint.Publish<OrderCreatedEvent>("result", cancellationToken);

        var result = createdOrder.Adapt<OrderDetailsDto>();

        return new CreateOrderResult(result);
    }
}
