namespace ViaEventAssociation.Core.Domain.Aggregates.Guests
{

    public class Guest
    {
        public GuestId Id { get; }
        public  FirstName FirstName { get; }
        public LastName LastName{ get; }
        public Email Email { get; }

        public Guest(FirstName firstName,LastName lastName, Email email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

    }
}

    



