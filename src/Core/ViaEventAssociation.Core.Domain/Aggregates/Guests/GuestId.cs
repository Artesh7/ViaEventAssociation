using ViaEventAssociation.Core.Domain.Common.Bases;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guests;

public class GuestId
{
    
    public Guid Id { get; }

    private GuestId(Guid Id)
    {
        if (Id == Guid.Empty) throw new ArgumentException("GuestId cannot be empty");
        Id = Id;
    }


}

