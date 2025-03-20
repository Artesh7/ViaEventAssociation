using System;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Locations.Values
{
    public sealed record LocationId
    {
        public Guid Value { get; }

        private LocationId(Guid value)
        {
            Value = value;
        }

        // Always generates a new non-empty Guid, so no validation is needed.
        public static Result<LocationId> Create()
        {
            return new Result<LocationId>(new LocationId(Guid.NewGuid()));
        }

        public override string ToString() => Value.ToString();
    }
}