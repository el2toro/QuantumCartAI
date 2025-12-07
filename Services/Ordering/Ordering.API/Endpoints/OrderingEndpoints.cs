using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Ordering.Application.DTOs;
using Ordering.Application.DTOs.Requests;
using Ordering.Application.Handlers.Commands;
using Ordering.Application.Handlers.Queries;

namespace Ordering.AAPI.Endpoints;

public class OrderingEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/orders")
            .WithTags("Orders");
        // .RequireAuthorization();
        //.WithOpenApi();

        // Get all orders for current customer
        app.MapGet("orders", async (Guid customerId, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersQuery(customerId));
            return Results.Ok(result.Orders);
        })
        .WithName("GetCustomerOrders")
        .WithSummary("Get all orders for current customer")
        //.Produces<PaginatedList<OrderSummaryDto>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized);


        // Get order by ID
        app.MapGet("orders/{orderId:guid}", async (Guid orderId, ISender sender) =>
        {
            var result = await sender.Send(new GetOrderByIdQuery(orderId));
            return Results.Ok(result.Order);
        })
        .WithName("GetOrderById")
        .WithSummary("Get order details by ID")
        .Produces<OrderDetailsDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status403Forbidden);
        // .RequireAuthorization(PolicyNames.OrderOwnerOrAdmin);

        // Create order draft
        app.MapPost("orders", async (CreateOrderRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateOrderCommand>();
            var result = await sender.Send(command);

            return Results.Created($"/orders/{result.Order.Id}", result.Order);
        })
        .WithName("CreateOrderDraft")
        .WithSummary("Create a new order draft")
        //.Produces<OrderDraftDto>(StatusCodes.Status201Created)
        .Produces<ValidationProblem>(StatusCodes.Status400BadRequest);

        //// Confirm order
        //group.MapPost("/{id:guid}/confirm", (ISender sender) =>
        //{

        //})
        //    .WithName("ConfirmOrder")
        //    .WithSummary("Confirm an order draft")
        //    .Produces(StatusCodes.Status204NoContent)
        //    .Produces(StatusCodes.Status404NotFound)
        //    .Produces<ValidationProblem>(StatusCodes.Status400BadRequest);
        //// .RequireAuthorization(PolicyNames.OrderOwnerOrAdmin);

        //// Cancel order
        //group.MapPost("/{id:guid}/cancel", CancelOrder)
        //    .WithName("CancelOrder")
        //    .WithSummary("Cancel an order")
        //    .Produces(StatusCodes.Status204NoContent)
        //    .Produces(StatusCodes.Status404NotFound)
        //    .Produces<ValidationProblem>(StatusCodes.Status400BadRequest)
        //    .RequireAuthorization(PolicyNames.OrderOwnerOrAdmin);

        //// Mark order as paid
        //group.MapPost("/{id:guid}/pay", MarkOrderAsPaid)
        //    .WithName("MarkOrderAsPaid")
        //    .WithSummary("Mark order as paid")
        //    .Produces(StatusCodes.Status204NoContent)
        //    .Produces(StatusCodes.Status404NotFound)
        //    .Produces<ValidationProblem>(StatusCodes.Status400BadRequest)
        //    .RequireAuthorization(PolicyNames.OrderOwnerOrAdmin);

        //// Ship order
        //group.MapPost("/{id:guid}/ship", ShipOrder)
        //    .WithName("ShipOrder")
        //    .WithSummary("Ship an order")
        //    .Produces(StatusCodes.Status204NoContent)
        //    .Produces(StatusCodes.Status404NotFound)
        //    .Produces<ValidationProblem>(StatusCodes.Status400BadRequest)
        //    .RequireAuthorization(PolicyNames.AdminOnly);

        //// Deliver order
        //group.MapPost("/{id:guid}/deliver", DeliverOrder)
        //    .WithName("DeliverOrder")
        //    .WithSummary("Mark order as delivered")
        //    .Produces(StatusCodes.Status204NoContent)
        //    .Produces(StatusCodes.Status404NotFound)
        //    .Produces<ValidationProblem>(StatusCodes.Status400BadRequest)
        //    .RequireAuthorization(PolicyNames.AdminOnly);

        //// Return order
        //group.MapPost("/{id:guid}/return", ReturnOrder)
        //    .WithName("ReturnOrder")
        //    .WithSummary("Return an order")
        //    .Produces(StatusCodes.Status204NoContent)
        //    .Produces(StatusCodes.Status404NotFound)
        //    .Produces<ValidationProblem>(StatusCodes.Status400BadRequest)
        //    .RequireAuthorization(PolicyNames.OrderOwnerOrAdmin);

        //// Get order price
        //group.MapPost("/{id:guid}/calculate-price", CalculateOrderPrice)
        //    .WithName("CalculateOrderPrice")
        //    .WithSummary("Calculate order price with taxes and shipping")
        //    .Produces<OrderPriceDto>(StatusCodes.Status200OK)
        //    .Produces(StatusCodes.Status404NotFound)
        //    .Produces<ValidationProblem>(StatusCodes.Status400BadRequest)
        //    .RequireAuthorization(PolicyNames.OrderOwnerOrAdmin);

        //// Add item to order
        //group.MapPost("/{id:guid}/items", AddOrderItem)
        //    .WithName("AddOrderItem")
        //    .WithSummary("Add item to order draft")
        //    .Produces<OrderItemResponseDto>(StatusCodes.Status200OK)
        //    .Produces(StatusCodes.Status404NotFound)
        //    .Produces<ValidationProblem>(StatusCodes.Status400BadRequest)
        //    .RequireAuthorization(PolicyNames.OrderOwnerOrAdmin);

        //// Update item quantity
        //group.MapPut("/{id:guid}/items/{productId:guid}", UpdateOrderItemQuantity)
        //    .WithName("UpdateOrderItemQuantity")
        //    .WithSummary("Update order item quantity")
        //    .Produces(StatusCodes.Status204NoContent)
        //    .Produces(StatusCodes.Status404NotFound)
        //    .Produces<ValidationProblem>(StatusCodes.Status400BadRequest)
        //    .RequireAuthorization(PolicyNames.OrderOwnerOrAdmin);

        //// Remove item from order
        //group.MapDelete("/{id:guid}/items/{productId:guid}", RemoveOrderItem)
        //    .WithName("RemoveOrderItem")
        //    .WithSummary("Remove item from order draft")
        //    .Produces(StatusCodes.Status204NoContent)
        //    .Produces(StatusCodes.Status404NotFound)
        //    .RequireAuthorization(PolicyNames.OrderOwnerOrAdmin);

        //// Clear order items
        //group.MapDelete("/{id:guid}/items", ClearOrderItems)
        //    .WithName("ClearOrderItems")
        //    .WithSummary("Clear all items from order draft")
        //    .Produces(StatusCodes.Status204NoContent)
        //    .Produces(StatusCodes.Status404NotFound)
        //    .RequireAuthorization(PolicyNames.OrderOwnerOrAdmin);
    }
}
