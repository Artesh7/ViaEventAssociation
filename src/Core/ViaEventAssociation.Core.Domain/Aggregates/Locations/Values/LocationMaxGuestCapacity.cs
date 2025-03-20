using System.Collections.Generic;
using System.Linq;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Locations.Values
{
    public sealed record LocationMaxGuestCapacity
    {
        public int Value { get; init; }

        private LocationMaxGuestCapacity(int value)
        {
            Value = value;
        }

        public static Result<LocationMaxGuestCapacity> Create(int value)
        {
            List<string> errors = Validate(value);
            return errors.Any() ? new Result<LocationMaxGuestCapacity>(errors) : new Result<LocationMaxGuestCapacity>(new LocationMaxGuestCapacity(value));
        }

        private static List<string> Validate(int value)
        {
            List<string> errors = new();
            if (value < 0)
            {
                errors.Add("Max guest capacity cannot be negative.");
            }
            return errors;
        }
    }
}