using System.Collections.Generic;
using Domain.Errors;
using Domain.ValidationObjects;
using ErrorOr;

namespace Infrastructure.Repositories;

public class InMemoryMeetingRepository
{
    
    private List<MeetingEntity> list = [];

    public ErrorOr<Success> Add(AttributeMeeting addition)
    {
        return DomainError.NotImplemented;
    }
    
    public ErrorOr<Success> Add(ErrorOrMeeting addition)
    {
        return DomainError.NotImplemented;
    }
    
    public ErrorOr<Success> Add(DataAnnotationMeeting addition)
    {
        return DomainError.NotImplemented;
    }
    
    public ErrorOr<Success> Add(SetMeeting addition)
    {
        return DomainError.NotImplemented;
    }
}