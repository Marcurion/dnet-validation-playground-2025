using System.ComponentModel.DataAnnotations;

namespace Domain.ValidationObjects;

public class SetMeeting
{
    public DateTime TakesPlaceWhen { get; private set; }

    public bool AlreadyHappened { get; private set; }

    public uint MaxAttendees { get; private set; }

    public List<uint> AttendeesUserIds { get; private set; }

    public void SetTakesPlaceWhen(DateTime value)
    {
        this.TakesPlaceWhen = value;
    }

    public SetMeeting AlterTakesPlaceWhen(DateTime value)
    {
        SetTakesPlaceWhen(value);
        return this;
    }

    public void SetAlreadyHappened(bool value)
    {
        if (value == true && TakesPlaceWhen.ToUniversalTime() > DateTime.UtcNow)
            throw new ValidationException(
                $"A meeting can only have happened when {nameof(TakesPlaceWhen)} lies in the past.");

        this.AlreadyHappened = value;
    }

    public SetMeeting AlterAlreadyHappened(bool value)
    {
        SetAlreadyHappened(value);
        return this;
    }
    
    public void SetMaxAttendees(uint value)
    {
        this.MaxAttendees = value;
    }

    public SetMeeting AlterMaxAttendees(uint value)
    {
        SetMaxAttendees(value);
        return this;
    }
    
    public void SetAttendeesUserIds(List<uint> value)
    {
        if (value.Count > MaxAttendees)
            throw new ValidationException($"You can not have more attendees than {nameof(MaxAttendees)} allows");
        
        this.AttendeesUserIds = value;
    }

    public SetMeeting AlterAttendeesUserIds(List<uint> value)
    {
        SetAttendeesUserIds(value);
        return this;
    }
    
}