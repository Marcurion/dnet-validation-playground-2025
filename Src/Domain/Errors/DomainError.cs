using System.Security.Cryptography;

namespace Domain.Errors;
using ErrorOr;

public static class DomainError
{
    public static Error NotImplemented = Error.NotFound(
        code: "DomainError.NotImplemented",
        description: "The Method you are calling is not implemented yet");
    
    public static Error Validator = Error.NotFound(
        code: "DomainError.Validator",
        description: "MediatR Validation failed");
}