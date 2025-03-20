using System;
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
            if (value == Guid.Empty)
            {
                return new Result<LocationId>(1, "Guid cannot be empty.");
            }
            return new Result<LocationId>(new LocationId(value));
        }

        public override string ToString() => Value.ToString();
    }
}