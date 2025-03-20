using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Locations.Values
{
    public sealed record Address
    {
        public int PostalCode { get; init; }
        public string City { get; init; }
        public string Street { get; init; }
        public int HouseNumber { get; init; }

        private Address(int postalCode, string city, string street, int houseNumber)
        {
            PostalCode = postalCode;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
        }

        public static Result<Address> Create(int postalCode, string city, string street, int houseNumber)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return new Result<Address>(1, "City is required.");
            }
            return new Result<Address>(new Address(postalCode, city, street, houseNumber));
        }

        public override string ToString() =>
            $"{Street} {HouseNumber}, {PostalCode} {City}";
    }
}