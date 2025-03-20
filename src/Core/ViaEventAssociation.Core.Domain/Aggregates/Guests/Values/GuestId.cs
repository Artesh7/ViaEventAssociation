using ViaEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guests.Values;

public record GuestId
{
    public Guid Value { get; init; }

    // 🔹 Constructor to allow instantiation
    public GuestId(Guid value)
    {
        Value = value;
    }

    public static Result<GuestId> Create(Guid value)
    {
        List<string> errors = Validate(value);

        return errors.Any() ? new Result<GuestId>(errors) : new Result<GuestId>(new GuestId(value));
    }

    private static List<string> Validate(Guid value)
    {
        List<string> errors = new();

        if (value == Guid.Empty)
            errors.Add("Guest ID cannot be empty.");

        return errors;
    }
}


