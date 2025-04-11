using System.Text;

namespace Domain.Extensions;

using ErrorOr;

public static class ErrorOrExtensionMethods
{
    public static ErrorOr<T> WithError<T>(this ErrorOr<T> obj, Error err)
    {
        if (obj.IsError)
        {
            obj.Errors.Add(err);
            return obj;
        }
        else
        {
            return err;
        }
    }


    public static string ConcatErrorCodes<T>(this ErrorOr<T> obj, string separator = "\n")
    {
        var stringBuilder = new StringBuilder();

        foreach (var err in obj.Errors)
        {
            stringBuilder.Append(err.Code + separator);
        }

        return stringBuilder.ToString();
    }


    public static string ConcatErrorDescriptions<T>(this ErrorOr<T> obj, string separator = "\n")
    {
        var stringBuilder = new StringBuilder();

        foreach (var err in obj.Errors)
        {
            stringBuilder.Append(err.Description + separator);
        }

        return stringBuilder.ToString();
    }

    public static List<Error> ExtendedBy(this List<Error> list, Error err)
    {
        list.Add(err);

        return list;
    }

    public static ErrorOr<T> GuardAgainst<T, TException>(
        this ErrorOr<T> input,
        Func<TException, Error> errorFactory)
        where TException : Exception
    {
        if (input.IsError) return input;

        try
        {
            return input.Value;
        }
        catch (TException ex)
        {
            return errorFactory(ex);
        }
    }


    public static ErrorOr<TOut> TryMap<TIn, TOut, TException>(
        this ErrorOr<TIn> input,
        Func<TIn, TOut> func,
        Func<TException, Error> errorFactory)
        where TException : Exception
    {
        return input.Then<TOut>(value =>
        {
            try
            {
                return func(value);
            }
            catch (TException ex)
            {
                return errorFactory(ex);
            }
        });
    }

    public static ErrorOr<T> Try<T, TException>(
        this ErrorOr<T> input,
        Func<T, T> func
    )
        where TException : Exception
    {
        return input.Then<T>(value =>
        {
            try
            {
                return func(value);
            }
            catch (TException ex)
            {
                return ex.AsErrorType(ErrorType.Validation);
            }
        });
    }
    public static ErrorOr<T> TryAny<T>(
        this ErrorOr<T> input,
        Func<T, T> func
    )
    {
        return input.Then<T>(value =>
        {
            try
            {
                return func(value);
            }
            catch (Exception ex)
            {
                return ex.AsErrorType(ErrorType.Validation);
            }
        });
    }

    public static ErrorOr<T> TryDo<T, TException>(
        this ErrorOr<T> input,
        Action<T> action
    )
        where TException : Exception
    {
        
        
        return input.Then<T>(value =>
        {
            try
            {
                action(value);
                return value;
            }
            catch (TException ex)
            {
                return ex.AsErrorType(ErrorType.Validation);
            }
        });
    }
    public static ErrorOr<T> TryDoAny<T>(
        this ErrorOr<T> input,
        Action<T> action
    )
    {
        
        
        return input.Then<T>(value =>
        {
            try
            {
                action(value);
                return value;
            }
            catch (Exception ex)
            {
                return ex.AsErrorType(ErrorType.Validation);
            }
        });
    }
}