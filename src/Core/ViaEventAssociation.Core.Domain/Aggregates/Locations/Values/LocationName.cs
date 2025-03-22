using System.Collections.Generic;
using System.Linq;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Locations.Values
{
    public sealed record LocationName
    {
        public string Value { get; }

        private LocationName(string value)
        {
            Value = value;
        }

        public static Result<LocationName> Create(string value)
        {
            List<string> errors = Validate(value);
            return errors.Any()
                ? new Result<LocationName>(errors)
                : new Result<LocationName>(new LocationName(value));
        }

        private static List<string> Validate(string value)
        {
            List<string> errors = new();
            if (string.IsNullOrWhiteSpace(value))
            {
                errors.Add("Location name cannot be empty.");
            }
            return errors;
        }

        public override string ToString() => Value;
    }
}