namespace Application.Feature1;
using MediatR;
using ErrorOr;

public class Feature1Request : IRequest<ErrorOr<int>>
{
    public string RequestInfo { get; set; }
}