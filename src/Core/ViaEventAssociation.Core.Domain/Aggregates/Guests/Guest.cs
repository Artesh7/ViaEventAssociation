using ViaEventAssociation.Core.Domain.Aggregates.Events.Values;
using ViaEventAssociation.Core.Domain.Aggregates.Guests.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guests;

public class Guest
{
    public GuestId Id { get; private set; }
    public Email Email { get; private set; }
    public GuestName Name { get; private set; }
    public ProfilePictureUrl ProfilePictureUrl { get; private set; }

    private Guest(GuestId id, GuestName name, Email email, ProfilePictureUrl profilePictureUrl)
    {
        Id = id;
        Name = name;
        Email = email;
        ProfilePictureUrl = profilePictureUrl;
    }

    public static Result<Guest> RegisterGuest(string firstName, string lastName, string email, string profilePictureUrl)
    {
        List<string> errors = new();

        // 🔹 Validate Email
        var emailResult = Email.Create(email);
        if (emailResult.resultCode != 0) errors.AddRange(emailResult.errors);

        // 🔹 Validate Name
        var nameResult = GuestName.Create(firstName, lastName);
        if (nameResult.resultCode != 0) errors.AddRange(nameResult.errors);

        // 🔹 Validate Profile Picture URL
        var profilePictureResult = ProfilePictureUrl.Create(profilePictureUrl);
        if (profilePictureResult.resultCode != 0) errors.AddRange(profilePictureResult.errors);

        // 🔹 Return errors if validation fails
        if (errors.Count > 0)
            return new Result<Guest>(errors);

        // 🔹 Create and return a new Guest if validation passes
        return new Result<Guest>(new Guest(
            new GuestId(Guid.NewGuid()), 
            nameResult.payLoad, 
            emailResult.payLoad, 
            profilePictureResult.payLoad
        ));
    }
}
