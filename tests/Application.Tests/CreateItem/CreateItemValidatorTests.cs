using Application.Commands.CreateItem;
using FluentValidation.TestHelper;
using Xunit;

namespace Application.Tests.CreateItem
{
    public class CreateItemValidatorTests
    {
        private readonly CreateItemValidator _validator = new();

        [Fact]
        public void Validator_Fails_WhenNameIsEmpty()
        {
            var result = _validator.TestValidate(new CreateItemCommand("", 5m));
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Fact]
        public void Validator_Fails_WhenPriceIsZeroOrLess()
        {
            var result = _validator.TestValidate(new CreateItemCommand("Name", 0m));
            result.ShouldHaveValidationErrorFor(c => c.Price);
        }

        [Fact]
        public void Validator_Passes_ForValidCommand()
        {
            var result = _validator.TestValidate(new CreateItemCommand("Valid", 10m));
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}