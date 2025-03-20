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
            if (value < 0)
            {
                return new Result<LocationMaxGuestCapacity>(1, "Max guest capacity cannot be negative.");
            }
            return new Result<LocationMaxGuestCapacity>(new LocationMaxGuestCapacity(value));
        }
    }
}