using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Locations.Values
{
    public sealed record MaxCapacity
    {
        public int Value { get; init; }

        private MaxCapacity(int value)
        {
            Value = value;
        }

        public static Result<MaxCapacity> Create(int value)
        {
            if (value < 0)
            {
                return new Result<MaxCapacity>(1, "Max capacity cannot be negative.");
            }
            return new Result<MaxCapacity>(new MaxCapacity(value));
        }

        public override string ToString() => Value.ToString();
    }
}