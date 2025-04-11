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
        var sut = new SetterMeeting() {TakesPlaceWhen = DateTime.Now + TimeSpan.FromDays(100)};

        sut.SetAlreadyHappened(false);
        var ex = Record.Exception(() => sut.SetAlreadyHappened(true));
        Assert.NotNull(ex);
        Assert.IsType<ValidationException>(ex);


    }
    
    [Fact]
     void SetterMeetingThrowingValidationException_BreaksChain()
     {
         // Create Meeting in the future 
         var sut = new SetterMeeting() {TakesPlaceWhen = DateTime.Now + TimeSpan.FromDays(100)};
 
         sut.SetAlreadyHappened(false);

         // Naming Proposal: .ValidateAny() .ValidateAnyDo() .ValidateExplicit() .ValidateExplicitDo()
         var res = sut.ToErrorOr<SetterMeeting>()
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
}