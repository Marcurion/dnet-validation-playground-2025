using System.Globalization;
using System.Reflection;
using ErrorOr;

namespace Domain.Errors;

public class LocalizedErrors
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class LocalizedErrorAttribute : Attribute
    {
        public string Culture { get; }
        public string Message { get; }

        public LocalizedErrorAttribute(string culture, string message)
        {
            Culture = culture;
            Message = message;
        }
    }
public static class StaticLocalizer
{
    public static CultureInfo UiCulture = CultureInfo.CurrentUICulture;
    
    // Dictionary<ErrorCode, Dictionary<Culture, Message>>
    private static readonly Dictionary<string, Dictionary<string, string>> _errorMessages =
        new(StringComparer.OrdinalIgnoreCase);

    public static string Get(string code)
    {
        // TODO: CultureInfo should be configurable for better testability
        var culture = UiCulture.Name;

        if (_errorMessages.TryGetValue(code, out var cultures) &&
            cultures.TryGetValue(culture, out var message))
        {
            return message;
        }

        return code; // fallback
    }

    public static IReadOnlyDictionary<string, Dictionary<string, string>> Err => _errorMessages;

    public static void Register(string code, string culture, string message)
    {
        if (!_errorMessages.ContainsKey(code))
            _errorMessages[code] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        _errorMessages[code][culture] = message;
    }
}
public static class LocalizedErrorLoader
{
    public static void LoadErrorsFromType(Type typeWithErrors)
    {
        var fields = typeWithErrors
            .GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (var field in fields)
        {
            var attrs = field.GetCustomAttributes<LocalizedErrorAttribute>();
            Error error;

            try
            {
                error = (Error) field.GetValue(null);
            }
            catch (NullReferenceException e)
            {
                continue;
            }
            
            foreach (var attr in attrs)
            {
                StaticLocalizer.Register(error.Code, attr.Culture, attr.Message);
            }
        }
    }

    public static void LoadAll()
    {
        LoadErrorsFromType(typeof(DomainError.Meetings));
        // LoadErrorsFromType(typeof(DomainError.Whatever));
    }
}
}