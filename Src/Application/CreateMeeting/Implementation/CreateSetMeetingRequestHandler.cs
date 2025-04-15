using System.Net.Security;
using Domain.Errors;
using Domain.Extensions;
using Domain.ValidationObjects;
using ErrorOr;
using Infrastructure.Repositories;
using MediatR;

namespace Application.CreateMeeting.Implementation;

public class CreateSetMeetingRequestHandler : IRequestHandler<CreateSetMeetingRequest, ErrorOr<Success>>
{
    private readonly IInMemoryMeetingRepository _repository;

    public CreateSetMeetingRequestHandler(IInMemoryMeetingRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<Success>> Handle(CreateSetMeetingRequest request, CancellationToken cancellationToken)
    {
        Random randomizer = new Random();

        // Randomize how the request is handled
        switch (randomizer.Next(0, 2))
        {
            case 0:
                return await Method1(request);
                break;
            
            case 1:
                return await Method2(request);
                break;
            
            default:
                return DomainError.NotImplemented;
                break;
        }
    }

    private async Task<ErrorOr<Success>> Method1(CreateSetMeetingRequest request)
    {
        Console.WriteLine("Using Method1");
       
        // TODO: Then -> ValidateAny
        ErrorOr<Success> res = new SetMeeting().ToErrorOr()
            .Then(res => res.AlterTakesPlaceWhen(request.TakesPlaceWhen))
            .Then(res => res.AlterAlreadyHappened(request.AlreadyHappened))
            .Then(res => res.AlterMaxAttendees(request.MaxAttendees))
            .Then(res => res.AlterAttendeesUserIds(request.AttendeesUserIds))
            .Then(_repository.Add);

        return res;
    }

    private async Task<ErrorOr<Success>> Method2(CreateSetMeetingRequest request)
    {
        Console.WriteLine("Using Method2");

        ErrorOr<Success> res = new SetMeeting().ToErrorOr()
            .ValidateAnyDo(res => res.SetTakesPlaceWhen(request.TakesPlaceWhen))
            .ValidateAnyDo(res => res.SetAlreadyHappened(request.AlreadyHappened))
            .ValidateAnyDo(res => res.SetMaxAttendees(request.MaxAttendees))
            .ValidateAnyDo(res => res.SetAttendeesUserIds(request.AttendeesUserIds))
            .Then(_repository.Add);

        return res;
    }
}