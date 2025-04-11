using Domain.Errors;
using Domain.ValidationObjects;
using FluentValidation;

namespace Domain.AttributeValidation;

public static class AttributeValidationRules
{
    public class ProperAttributeMeeting : AbstractValidator<AttributeMeeting>
    {
        public ProperAttributeMeeting()
        {
            // List of users smaller than maximum
            RuleFor(x => x.AttendeesUserIds)
                .Must((model, attendeesUserIds) => attendeesUserIds?.Count <= model.MaxAttendees)
                .WithMessage(DomainError.Meetings.TooManyAttendees.Description);
            
            // When AlreadyHappened is set to true, the meeting took place in the past
            RuleFor(x => x.AlreadyHappened)
                .Must((model, _) => model.TakesPlaceWhen.ToUniversalTime() <= DateTime.UtcNow)
                .When(x => x.AlreadyHappened)
                .WithMessage(DomainError.Meetings.CompletedMeetingsInThePast.Description);
        }
    }
}