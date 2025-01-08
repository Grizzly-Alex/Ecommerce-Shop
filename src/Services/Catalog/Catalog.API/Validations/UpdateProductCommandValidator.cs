namespace Catalog.API.Validations;


public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(i => i.Id).NotEmpty().WithMessage("Is required");
        RuleFor(i => i.Name)
            .NotEmpty().WithMessage("Is required")
            .Length(2, 100).WithMessage("must be between 2 and 100 characters");
        RuleFor(i => i.Category).NotEmpty().WithMessage("Is required");
        RuleFor(i => i.ImageFile).NotEmpty().WithMessage("Is required");
        RuleFor(i => i.Price).GreaterThan(0).WithMessage("Must be greater than 0");
    }
}
