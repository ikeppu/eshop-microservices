using Marten.Schema;

namespace CatalogAPI.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (await session.Query<Product>().AnyAsync())
            {
                return;
            }

            session.Store<Product>(GetPreconfiguredProducts());

            await session.SaveChangesAsync();
        }

        private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>
        {
            new Product()
            {
                Id = Guid.NewGuid(),
                Name = "IPhone X",
                Description = "This phone is amazing",
                ImageFile = "product-1.png",
                Price  = 950.00M,
                Category = new List<string>{ "Smart Phone" }
            }
        };
    }
}
