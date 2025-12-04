
namespace Ordering.Domain.Services;

public interface IOrderNumberGenerator
{
    Task<string> GenerateOrderNumberAsync(CancellationToken cancellationToken = default);
}

public class OrderNumberGenerator : IOrderNumberGenerator
{
    //private readonly IApplicationDbContext _context;

    //public OrderNumberGenerator(IApplicationDbContext context)
    //{
    //    _context = context;
    //}

    public async Task<string> GenerateOrderNumberAsync(CancellationToken cancellationToken = default)
    {
        var today = DateTime.UtcNow.ToString("yyyyMMdd");
        //var existingOrdersToday = await _context.Orders
        //    .CountAsync(o => o.OrderDate.Date == DateTime.UtcNow.Date, cancellationToken);

        //var sequenceNumber = existingOrdersToday + 1;
        //return $"ORD-{today}-{sequenceNumber:D5}";
        return string.Empty;
    }
}
