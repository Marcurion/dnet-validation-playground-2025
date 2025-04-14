using System.ComponentModel.DataAnnotations;

namespace Domain.ValidationObjects;

public class SetMeeting
{
    public DateTime TakesPlaceWhen { get; set; }
    
    public bool AlreadyHappened { get; protected set; }
    
    public uint MaxAttendees { get; set; }
    
    public List<uint> AttendeesUserIds { get; set; }

    public void SetAlreadyHappened(bool value)
    {
        if (value == true && TakesPlaceWhen.ToUniversalTime() > DateTime.UtcNow)
            throw new ValidationException($"A meeting can only have happened when {nameof(TakesPlaceWhen)} lies in the past.");
            
        AlreadyHappened = value;
    }     
    
    public SetMeeting AlterAlreadyHappened(bool value)
       {
           if (value == true && TakesPlaceWhen.ToUniversalTime() > DateTime.UtcNow)
               throw new ValidationException($"A meeting can only have happened when {nameof(TakesPlaceWhen)} lies in the past.");
               
           AlreadyHappened = value;
           return this;
       }
}