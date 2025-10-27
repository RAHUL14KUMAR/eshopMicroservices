using BasketAPI.Data;
using DiscountGRPC;

namespace BasketAPI.Basket.StoreBasket;
public record StoreBasketCommand(ShoppingCart ShoppingCart): ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

public class StoreBasketCpmmandValidator:AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCpmmandValidator()
    {
        RuleFor(x => x.ShoppingCart.UserName).NotEmpty().WithMessage("username is required");
        RuleFor(x => x.ShoppingCart).NotNull().WithMessage("cart cant be null");
    }
}
public class StoreBasketCommandHandler(IBasketRepository repository,DiscountProtoService.DiscountProtoServiceClient discountProto) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        // communicate with grpc server and calculate the latest price of products
        await DeductDiscounts(command.ShoppingCart, cancellationToken);
         
        // store basket to database(use marten upsert-if exists=update, else insert)
        await repository.StoreBasket(command.ShoppingCart, cancellationToken);

        return new StoreBasketResult(command.ShoppingCart.UserName);
    }
    private async Task DeductDiscounts(ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var discountRequest = new GetDiscountRequest { ProductName = item.ProductName };
            var discountResponse = await discountProto.GetDiscountAsync(discountRequest);
            item.Price -= discountResponse.Amount;
        }
    }
}