using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Interfaces;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Handlers.Events;

public class PaymentSucceededEventHandler(IOrderingRepository orderingRepository)
    : IConsumer<PaymentSucceededEvent>
{
    public async Task Consume(ConsumeContext<PaymentSucceededEvent> context)
    {
        var paymentSucceededEvent = context.Message;
        var order = await orderingRepository
            .GetOrderByIdAsync(OrderId.Of(paymentSucceededEvent.OrderId.ToString()), CancellationToken.None);

        order.AddPayment(paymentSucceededEvent.Amount, paymentSucceededEvent.PaymentMethod);
        order.Confirm();
    }
}
