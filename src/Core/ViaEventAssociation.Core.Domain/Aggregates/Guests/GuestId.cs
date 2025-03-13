using ViaEventAssociation.Core.Domain.Common.Bases;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guests;

public class GuestId
{
    public Guid Id { get; }

    public GuestId(Guid id)
    {
        this.Id = id;
    }
}

