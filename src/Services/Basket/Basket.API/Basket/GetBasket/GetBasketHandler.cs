namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string username) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart Cart);

    public class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            return new GetBasketResult(new ShoppingCart("swn"));
        }
    }
}
    