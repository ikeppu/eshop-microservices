
using CatalogAPI.Products.GetProductByCategory;

namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductRequest(
        Guid Id,
        string Name,
        List<string> Category,
        string Description,
        string ImageFile,
        decimal Price);
    
    public record UpdateProductResponse(bool isSuccess);
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (UpdateProductRequest request, ISender sender) => 
            { 
                var command = request.Adapt<UpdateProductCommand>();

                var result = await sender.Send(command);

                return Results.Ok(new UpdateProductResponse(result.isSuccess));
            })
          .WithName("UpdateProduct")
          .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .ProducesProblem(StatusCodes.Status404NotFound)
          .WithSummary("Update Product")
          .WithDescription("Update Product"); ;
        }
    }
}
