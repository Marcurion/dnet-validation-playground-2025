using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Domain.Errors;
using Domain.Extensions;
using Domain.ValidationObjects;
using ErrorOr;

namespace Tests.UnitTests;

public class ValidationObjectsTests
{
    
    [Fact]
    void SetterMeeting_ThrowsValidationException_WhenCompletedInTheFuture()
    {
        // Create Meeting in the future 
        var sut = new SetMeeting() {TakesPlaceWhen = DateTime.Now + TimeSpan.FromDays(100)};

        sut.SetAlreadyHappened(false);
        var ex = Record.Exception(() => sut.SetAlreadyHappened(true));
        Assert.NotNull(ex);
        Assert.IsType<ValidationException>(ex);
    }
    
    [Fact]
     void SetterMeetingThrowingValidationException_BreaksChain()
     {
         // Create Meeting in the future 
         var sut = new SetMeeting() {TakesPlaceWhen = DateTime.Now + TimeSpan.FromDays(100)};
 
         sut.SetAlreadyHappened(false);

         // Naming Proposal: .ValidateAny() .ValidateAnyDo() .ValidateExplicit() .ValidateExplicitDo()
         var res = sut.ToErrorOr<SetMeeting>()
                 //.GuardAgainst<SetterMeeting, ValidationException>(ex => ex.AsErrorType())
                 //.TryDo<SetterMeeting, ValidationException>(sut => sut.SetAlreadyHappened(false))
                 .TryAny(sut => sut.AlterAlreadyHappened(true))
             //.ValidateAny(sut => sut.AlterAlreadyHappened(true))
             //.ValidateExplicit<SetterMeeting, ValidationException>(sut => sut.AlterAlreadyHappened(true))
             //.TryDo<SetterMeeting, ValidationException>(sut => sut.SetAlreadyHappened(false))
             ;//.Else(DomainError.NotImplemented);

         Assert.True(res.IsError);
         Assert.Equal(ErrorType.Validation, res.FirstError.Type);
     }

    [Fact]
    async Task LocalizedErrorMessage_WorksWithGermanSystemLanguage()
    {
        
        LocalizedErrors.LocalizedErrorLoader.LoadAll();

        var err = DomainError.Meetings.CompletedMeetingsInThePast;
        
        Assert.StartsWith("Um ein", err.Description, StringComparison.Ordinal);
        Assert.Equal("de-DE", CultureInfo.CurrentUICulture.Name);
    }
    
    [Fact]
    async Task LocalizedErrorMessage_SupportsEnglish()
    {
        
        LocalizedErrors.LocalizedErrorLoader.LoadAll();
        LocalizedErrors.StaticLocalizer.UiCulture = CultureInfo.GetCultureInfo("en-US");
        var err = DomainError.Meetings.CompletedMeetingsInThePast;
        
        Assert.StartsWith("To complete", err.Description, StringComparison.Ordinal);
        Assert.Equal("en-US", LocalizedErrors.StaticLocalizer.UiCulture.Name);
    }

    [Fact]
    void AttributeValidation_Works()
    {
        LocalizedErrors.LocalizedErrorLoader.LoadAll();
        LocalizedErrors.StaticLocalizer.UiCulture = CultureInfo.GetCultureInfo("en-US");
        // Create new meeting for max 5 in the future
        // Order of object initializer assignments matter so our object remains valid with each property initializer
        var sut = new AttributeMeeting() { MaxAttendees = 5, AttendeesUserIds = [1,2,3], TakesPlaceWhen = DateTime.Now + TimeSpan.FromDays(100)};

        sut.AlreadyHappened = false; // no issue
        
        var ex = Record.Exception(() => sut.AlreadyHappened = true); // throws exception since meeting is not in the past
        Assert.NotNull(ex);
        Assert.IsType<FluentValidation.ValidationException>(ex);
        Assert.Contains("must be in the past", ex.Message);
    }

    [Fact]
    void ErrorOrMeeting_SetProperty_Uses_AlterProperty_Correctly()
    {
        // Only SetAlreadyHappened enforces business rules, so if we can get an DomainError with Alter...() then we are good
        ErrorOrMeeting sut = new ErrorOrMeeting();
        var res = sut.ToErrorOr()
            .Then(obj => obj.AlterTakesPlaceWhen(DateTime.Now + TimeSpan.FromDays(100)));
        Assert.False(res.IsError);

        res = res.Then(obj => obj.AlterMaxAttendees(5)); // No issue

        res = res.Then(obj => obj.AlterAlreadyHappened(false)); // Does nothing
        Assert.False(res.IsError);

        // Violation: Meeting in the future can not already have happened
        res = res.Then(obj => obj.AlterAlreadyHappened(true));
        Assert.True(res.IsError);
        Assert.Contains(DomainError.Meetings.CompletedMeetingsInThePast, res.Errors);
        Assert.IsType<ErrorOr<ErrorOrMeeting>>(res);
    }
}