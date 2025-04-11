using System.Runtime.CompilerServices;

namespace Domain.AttributeValidation;
// Helper methods for validation
public static class AttributeValidationHelper
{
    // Reusable helper method to validate property values using attributes
    public static void ValidatePropertyValue<T>(ref T field, T value, object instance, [CallerMemberName] string propertyName = "")
    {
        // Get the property info 
        var propertyInfo = instance.GetType().GetProperty(propertyName);
        if (propertyInfo == null)
            throw new ArgumentException($"Property {propertyName} not found");
        // Get the validation attribute
        var attr = (SetRestrictionsAttribute)Attribute.GetCustomAttribute(
            propertyInfo, typeof(SetRestrictionsAttribute));
        // Validate the value if the attribute exists
        attr?.Validate(value); // Throws exception if invalid
        // If validation passes, update the field
        field = value;
    }
}