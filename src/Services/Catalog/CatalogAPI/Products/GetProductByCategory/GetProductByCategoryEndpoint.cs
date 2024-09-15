
using CatalogAPI.Products.GetProductById;

namespace CatalogAPI.Products.GetProductByCategory
{
    //public record GetProductsByCategoryRequest();
    public record GetProductByCategoryResponse(IEnumerable<Product> Product);
    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByCategoryQuery(category));

                var response = result.Adapt<GetProductByCategoryResponse>();

                return Results.Ok(response);
            })
          .WithName("GetProductsByCategory")
          .Produces<GetProductByCategoryEndpoint>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .WithSummary("Get Products by category")
          .WithDescription("Get Products by category");
        }
    }
}
