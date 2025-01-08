namespace Catalog.API.Validations;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(i => i.Id).NotEmpty().WithMessage("Is required");
    }
}
