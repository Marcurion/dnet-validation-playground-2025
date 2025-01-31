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


    public static string ConcatErrorCodes<T>(this ErrorOr<T> obj, string separator =  "\n")
    {
        var stringBuilder = new StringBuilder();

        foreach (var err in obj.Errors)
        {
            stringBuilder.Append(err.Code + separator);
        }

        return stringBuilder.ToString();
    }


    public static string ConcatErrorDescriptions<T>(this ErrorOr<T> obj, string separator =  "\n")
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
    
}