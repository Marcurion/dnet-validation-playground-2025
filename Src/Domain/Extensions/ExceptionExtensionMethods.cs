namespace Domain.Extensions;
using ErrorOr;

public static class ExceptionExtensionMethods
{
    public static Error AsErrorType(this Exception ex, ErrorType type = ErrorType.Unexpected)
    {
        return Error.Custom(type: (int)type, code: ex.GetType().Name, description: ex.Message, metadata: ex.Data as Dictionary<string, object>);
    }
}