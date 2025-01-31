using Domain.Errors;
using Domain.Extensions;
using Domain.Interfaces;

namespace Application.Feature1;
using MediatR;
using ErrorOr;

public class Feature1RequestHandler : IRequestHandler<Feature1Request, ErrorOr<int>>
{
    private readonly IRepository<object> _repository;
    
    public Feature1RequestHandler(IRepository<object> repository)
    {
        _repository = repository;
    }
    
    public async Task<ErrorOr<int>> Handle(Feature1Request request, CancellationToken cancellationToken)
    {
        try
        {
            _repository.SaveAsync();
        }
        catch (Exception e)
        {
            //Console.WriteLine(e);
            return (List<Error>) [e.AsErrorType(), DomainError.NotImplemented];
        }

        return 1.ToErrorOr();
    }
}