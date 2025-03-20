using System.Collections.Generic;
using System.Linq;
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
            List<string> errors = Validate(city);
            return errors.Any() ? new Result<Address>(errors) : new Result<Address>(new Address(postalCode, city, street, houseNumber));
        }

        private static List<string> Validate(string city)
        {
            List<string> errors = new();
            if (string.IsNullOrWhiteSpace(city))
            {
                errors.Add("City is required.");
            }
            return errors;
        }

        public override string ToString() =>
            $"{Street} {HouseNumber}, {PostalCode} {City}";
    }
}