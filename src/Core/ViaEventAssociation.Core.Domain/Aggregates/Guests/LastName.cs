namespace ViaEventAssociation.Core.Domain.Aggregates.Guests;

public class LastName
{
    public string Name { get; }

    private LastName(string name)
    {
        this.Name = name;
    }
}