using System.ComponentModel.DataAnnotations;
using Domain.Errors;
using ErrorOr;

namespace Domain.ValidationObjects;

public class ErrorOrMeeting
{
    public DateTime TakesPlaceWhen { get; protected set; }

    public bool AlreadyHappened { get; protected set; }

    public uint MaxAttendees { get; protected set; }

    public List<uint> AttendeesUserIds { get; protected set; }

    // NOTEABLE: I decided to offer both the Set... and Alter... methods even though only one is required,
    // the reason is since the AlterMethod returns the object itself as ErrorOr type it works well with
    // a functional chain approach:
    // ErrorOr<T> overallResult = obj.ToErrorOr().Then(obj => obj.Alter...).Then(obj => obj.Alter...).Then(obj => obj.Alter...).Then(obj => obj.Alter...);
    // see: TODO
    public ErrorOr<Success> SetTakesPlaceWhen(DateTime value)
    {
       return this.AlterTakesPlaceWhen(value).Else(errors => errors).Then((res) => Result.Success);
    }

    public ErrorOr<ErrorOrMeeting> AlterTakesPlaceWhen(DateTime value)
    {
        this.TakesPlaceWhen = value;
        return this;
    }
    
    public ErrorOr<Success> SetAlreadyHappened(bool value)
    {
       return this.AlterAlreadyHappened(value).Else(errors => errors).Then((res) => Result.Success);

    }

    public ErrorOr<ErrorOrMeeting> AlterAlreadyHappened(bool value)
    {
        if (value == true && TakesPlaceWhen.ToUniversalTime() > DateTime.UtcNow)
            return DomainError.Meetings.CompletedMeetingsInThePast;

        AlreadyHappened = value;
        return this;
    }
    
    public ErrorOr<Success> SetMaxAttendees(uint value)
    {
       return this.AlterMaxAttendees(value).Else(errors => errors).Then((res) => Result.Success);

    }

    public ErrorOr<ErrorOrMeeting> AlterMaxAttendees(uint value)
    {
        MaxAttendees = value;
        return this;
    }
    
    public ErrorOr<Success> SetAttendeesUserIds(List<uint> value)
    {
       return this.AlterAttendeesUserIds(value).Else(errors => errors).Then((res) => Result.Success);

    }

    public ErrorOr<ErrorOrMeeting> AlterAttendeesUserIds(List<uint> value)
    {
        if (value.Count >= MaxAttendees)
            return DomainError.Meetings.TooManyAttendees;
        
        AttendeesUserIds = value;
        return this;
    }
}