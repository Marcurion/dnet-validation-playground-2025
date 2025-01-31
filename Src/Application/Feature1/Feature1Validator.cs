using FluentValidation;

namespace Application.Feature1;

public class Feature1Validator : AbstractValidator<Feature1Request>
{
    public Feature1Validator()
    {
        RuleFor(r => r.RequestInfo)
            .NotEmpty()
            .NotNull()
            .WithMessage("RequestInfo should be populated");
    }
}