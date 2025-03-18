namespace ViaEventAssociation.Core.Domain.Aggregates.Guests;

public class GuestLastName
{
    public string Name { get; }

    private GuestLastName(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Last name cannot be empty");
        if (value.Length < 2 || value.Length > 25) throw new ArgumentException("Last name must be between 2 and 25 characters");

        Name = char.ToUpper(value[0]) + value.Substring(1).ToLower();
    }
}