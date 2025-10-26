using BasketAPI.Data;

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
public class StoreBasketCommandHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        // store basket to database(use marten upsert-if exists=update, else insert)
        await repository.StoreBasket(command.ShoppingCart, cancellationToken);

        return new StoreBasketResult(command.ShoppingCart.UserName);
    }
}