//using BuildingBlocks.CQRS;
//using CatalogAPI.Models;
//using Marten;
namespace CatalogAPI.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);


// we register aall related validator-(IEnumerable<IValidator<TRequest>> validators)
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}
// as we had create the common validator interface in the building block so we had to remove this IValidator<CreateProductCommand> validator from createproducthandler args;
internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand cmd, CancellationToken token)
    {
        //business logic to create a product

        // check the validation results;
        // var validRes = await validator.ValidateAsync(cmd, token);

        // if validation fails, throw error
        // var errors = validRes.Errors.Select(e => e.ErrorMessage).ToList();
        // if(errors.Any())
        // {
        //     throw new ValidationException(errors.FirstOrDefault());
        // }

        var product = new Product
        {
            Name = cmd.Name,
            Category = cmd.Category,
            Description = cmd.Description,
            ImageFile = cmd.ImageFile,
            Price = cmd.Price
        };

        //save to database
        session.Store(product);
        await session.SaveChangesAsync(token);

        return new CreateProductResult(product.Id);

        // throw new NotImplementedException();
    }
}
