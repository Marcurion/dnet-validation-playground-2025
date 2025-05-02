using Domain.Errors;
using Domain.Primitives;
using FluentValidation;

namespace Domain.ValidationObjects;

public class ValueMeeting : ValueObject<PlainMeeting, ValueMeeting>
{
   protected override void Validate()
   {
      if (Value.AlreadyHappened && Value.TakesPlaceWhen.ToUniversalTime() > DateTime.UtcNow)
      {
         throw new ValidationException(DomainError.Meetings.CompletedMeetingsInThePast.Description);
      }

      if (Value.MaxAttendees < Value.AttendeesUserIds.Count)
      {
         throw new ValidationException(DomainError.Meetings.TooManyAttendees.Description);
      }
      
      base.Validate();
   }
}