
using BasketAPI.Data;

namespace BasketAPI.Basket.GetBasket;

public record GetBasketQuery(string UserName):IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart ShoppingCart);
public class GetBasketQueryHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        // get basket from database
        var basket = await repository.GetBasket(query.UserName);

        return new GetBasketResult(basket);
    }
}