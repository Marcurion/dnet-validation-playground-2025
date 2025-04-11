using System.ComponentModel.DataAnnotations;
using Domain.Errors;
using ErrorOr;

namespace Domain.ValidationObjects;

public class ErrorOrMeeting
{
    public DateTime TakesPlaceWhen { get; set; }

    public bool AlreadyHappened { get; protected set; }

    public uint MaxAttendees { get; set; }

    public List<uint> AttendeesUserIds { get; set; }

    public ErrorOr<Success> SetAlreadyHappened(bool value)
    {
        if (value == true && TakesPlaceWhen.ToUniversalTime() > DateTime.UtcNow)
            return DomainError.Meetings.CompletedMeetingsInThePast;

        AlreadyHappened = value;
        return Result.Success;
    }

    public ErrorOr<ErrorOrMeeting> AlterAlreadyHappened(bool value)
    {
        if (value == true && TakesPlaceWhen.ToUniversalTime() > DateTime.UtcNow)
            return DomainError.Meetings.CompletedMeetingsInThePast;

        AlreadyHappened = value;
        return this;
    }
}