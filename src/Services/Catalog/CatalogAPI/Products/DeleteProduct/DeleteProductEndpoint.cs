
using CatalogAPI.Products.CreateProduct;

namespace CatalogAPI.Products.DeleteProduct
{
    //public record DeleteProductRequest(Guid id);
    public record DeleteProductResponse(bool isSuccess);
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}", async (Guid id, ISender sender) => 
            {
                var command = new DeleteProductCommand(id);

                var result = await sender.Send(command);

                return Results.Ok(new DeleteProductResponse(result.isSuccess));
            })
            .WithName("DeleteProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Product By Id")
            .WithDescription("Delete Product By Id"); 
        }
    }
}
