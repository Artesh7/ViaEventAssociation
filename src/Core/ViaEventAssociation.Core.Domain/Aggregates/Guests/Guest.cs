namespace ViaEventAssociation.Core.Domain.Aggregates.Guests
{

    public class Guest
    {
        public GuestId Id { get; }
        public GuestName Name { get; }
        public Email Email { get; }

        public Guest(GuestId id ,GuestName name, Email email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

    }
}

    



