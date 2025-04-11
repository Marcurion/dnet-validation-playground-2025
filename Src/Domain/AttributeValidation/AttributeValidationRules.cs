using FluentValidation;

namespace Domain.AttributeValidation;

public static class AttributeValidationRules
{
    public class StringInBounds : AbstractValidator<string>
    {
        public StringInBounds()
        {
            RuleFor(x => x)
                .NotEmpty().WithMessage("Property cannot be empty")
                .Length(5, 10).WithMessage("Property must be between 5 and 10 characters");
        }
    }
}