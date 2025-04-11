using FluentValidation;

namespace Domain.AttributeValidation;
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class SetRestrictionsAttribute : Attribute
{
    public Type RuleType { get; }
    public SetRestrictionsAttribute(Type ruleType)
    {
        if (!typeof(IValidator).IsAssignableFrom(ruleType))
        {
            throw new ArgumentException("RuleType must implement IValidator.");
        }
        RuleType = ruleType;
    }
    public void Validate(object value)
    {
        var validator = (IValidator)Activator.CreateInstance(RuleType);
        var context = new ValidationContext<object>(value);
        var result = validator.Validate(context);
        if (!result.IsValid)
        {
            throw new ValidationException(string.Join(", ", result.Errors.Select(e => e.ErrorMessage)));
        }
    }
}