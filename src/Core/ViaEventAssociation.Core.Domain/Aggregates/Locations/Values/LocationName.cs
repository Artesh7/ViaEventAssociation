using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Locations.Values
{
    public sealed record LocationName
    {
        public string Value { get; init; }

        private LocationName(string value)
        {
            Value = value;
        }

        public static Result<LocationName> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return new Result<LocationName>(1, "Location name cannot be empty.");
            }
            return new Result<LocationName>(new LocationName(value));
        }

        public override string ToString() => Value;
    }
}