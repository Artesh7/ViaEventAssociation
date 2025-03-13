namespace ViaEventAssociation.Core.Domain.Aggregates.Guests;

public class Email
{
    public string EmailAddress { get; }

    private Email(string emailAddress)
    {
        this.EmailAddress = emailAddress;
    }

}