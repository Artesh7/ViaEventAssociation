namespace ViaEventAssociation.Core.Domain.Aggregates.Guests
{

    public class Guest
    {
        public GuestId Id { get; }
        public  GuestFirstName GuestFirstName { get; }
        public GuestLastName GuestLastName{ get; }
        public Email Email { get; }
        public ProfilePictureURL ProfilePictureURL { get; }

        public Guest(GuestFirstName guestFirstName,GuestLastName guestLastName, Email email, ProfilePictureURL profilePictureURL)
        {
            GuestFirstName = guestFirstName;
            GuestLastName = guestLastName;
            Email = email;
            ProfilePictureURL = profilePictureURL;

        }

    }
}

    



