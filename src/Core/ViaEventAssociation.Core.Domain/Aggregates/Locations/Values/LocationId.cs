using System;
using System.Collections.Generic;
using System.Linq;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Locations.Values
{
    public sealed record LocationId
    {
        public Guid Value { get; init; }

        private LocationId(Guid value)
        {
            Value = value;
        }

        public static Result<LocationId> Create(Guid value)
        {
            List<string> errors = Validate(value);
            return errors.Any() ? new Result<LocationId>(errors) : new Result<LocationId>(new LocationId(value));
        }

        private static List<string> Validate(Guid value)
        {
            List<string> errors = new();
            if (value == Guid.Empty)
            {
                errors.Add("Guid cannot be empty.");
            }
            return errors;
        }

        public override string ToString() => Value.ToString();
    }
}