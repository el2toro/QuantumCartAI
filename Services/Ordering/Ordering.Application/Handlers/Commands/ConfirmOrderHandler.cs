using BuildingBlocks.CQRS;
using Mapster;
using Ordering.Application.DTOs;
using Ordering.Application.Interfaces;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Handlers.Commands;

public record ConfirmOrderCommand(Guid OrderId) : ICommand<ConfirmOrderResult>;
public record ConfirmOrderResult(OrderDetailsDto OrderDetailsDto);
public class ConfirmOrderHandler(IOrderingRepository orderingRepository)
    : ICommandHandler<ConfirmOrderCommand, ConfirmOrderResult>
{
    public async Task<ConfirmOrderResult> Handle(ConfirmOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await orderingRepository.GetOrderByIdAsync(OrderId.Of(command.OrderId.ToString()), cancellationToken);

        // Simulate payment processing
        var paymentId = Guid.NewGuid().ToString();
        var gatewayTransactionId = Guid.NewGuid().ToString();

        var payment = order.AddPayment(order.TotalAmount, "card", gatewayTransactionId, "4242");

        order.MarkPaymentAsPaid(payment.Id, "PaymentGatewayOk");

        // order.Confirm();
        var updated = await orderingRepository.UpdateOrderAsync(order, cancellationToken);

        var response = updated.Adapt<OrderDetailsDto>();

        return new ConfirmOrderResult(response);
    }
}
