using FluentValidation;

namespace Application.Commands.CreateItem
{
    public class CreateItemValidator : AbstractValidator<CreateItemCommand>
    {
        public CreateItemValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name je obavezan");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price mora biti veća od 0");
        }
    }
}