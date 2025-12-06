using Mapster;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Mappings;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig.GlobalSettings.NewConfig<OrderId, Guid>()
         .MapWith(src => src.Value);

        TypeAdapterConfig.GlobalSettings.NewConfig<OrderItemId, Guid>()
         .MapWith(src => src.Value);

        TypeAdapterConfig.GlobalSettings.NewConfig<CustomerId, Guid>()
          .MapWith(src => src.Value);

        TypeAdapterConfig.GlobalSettings.NewConfig<ProductId, Guid>()
          .MapWith(src => src.Value);

        TypeAdapterConfig.GlobalSettings.NewConfig<OrderNumber, string>()
          .MapWith(src => src.Value);
    }
}
