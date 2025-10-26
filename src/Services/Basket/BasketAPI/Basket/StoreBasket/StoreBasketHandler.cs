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
public class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        // store basket to database(use marten upsert-if exists=update, else insert)

        return new StoreBasketResult("rahul");
    }
}