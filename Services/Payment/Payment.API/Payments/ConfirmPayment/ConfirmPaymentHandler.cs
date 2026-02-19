using BuildingBlocks.CQRS;
using BuildingBlocks.Messaging.Events;
using Mapster;
using MassTransit;

namespace Payment.API.Payments.ConfirmPayment;

public record ConfirmPaymentCommand(Guid OrderId, Guid CustomerId, decimal Amount, string PaymentMethod)
    : ICommand<ConfirmPaymentResult>;
public record ConfirmPaymentResult();

public class ConfirmPaymentHandler(IPublishEndpoint publishEndpoint)
    : ICommandHandler<ConfirmPaymentCommand, ConfirmPaymentResult>
{

    public async Task<ConfirmPaymentResult> Handle(ConfirmPaymentCommand command, CancellationToken cancellationToken)
    {
        var paymentSucceededEvent = command.Adapt<PaymentSucceededEvent>();
        await publishEndpoint.Publish(paymentSucceededEvent);

        return new ConfirmPaymentResult();
    }
}
