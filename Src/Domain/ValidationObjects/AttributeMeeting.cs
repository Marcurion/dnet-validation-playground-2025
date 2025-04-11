using Domain.AttributeValidation;

namespace Domain.ValidationObjects;

[SetRestrictions(typeof(AttributeValidationRules.ProperAttributeMeeting))]
public class AttributeMeeting
{
    private DateTime _takesPlaceWhen;

    public DateTime TakesPlaceWhen
    {
        get => _takesPlaceWhen;
        set => AttributeValidationHelper.ValidateAndSet(ref _takesPlaceWhen, value, this);
    }

    private bool _alreadyHappened;
    public bool AlreadyHappened 
    {
        get => _alreadyHappened;
        set => AttributeValidationHelper.ValidateAndSet(ref _alreadyHappened, value, this);
    }

    private uint _maxAttendees;
    public uint MaxAttendees 
    {
        get => _maxAttendees;
        set => AttributeValidationHelper.ValidateAndSet(ref _maxAttendees, value, this);
    }

    private List<uint> _attendeesUserIds;  
    public List<uint> AttendeesUserIds 
    {
        get => _attendeesUserIds;
        set => AttributeValidationHelper.ValidateAndSet(ref _attendeesUserIds, value, this);
    }
}