namespace ViaEventAssociation.Core.Domain.Aggregates.Guests
{

    public class Guest
    {
        public GuestId Id { get; }
        public  FirstName FirstName { get; }
        public LastName LastName{ get; }
        public Email Email { get; }

        public Guest(GuestId id ,FirstName firstName,LastName lastName, Email email)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

    }
}

    



