using System.Collections.Generic;
using System.Linq;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Locations.Values
{
    public sealed record MaxCapacity
    {
        public int Value { get; }

        private MaxCapacity(int value)
        {
            Value = value;
        }

        public static Result<MaxCapacity> Create(int value)
        {
            List<string> errors = Validate(value);
            return errors.Any()
                ? new Result<MaxCapacity>(errors)
                : new Result<MaxCapacity>(new MaxCapacity(value));
        }

        private static List<string> Validate(int value)
        {
            List<string> errors = new();
            if (value < 0)
            {
                errors.Add("Max capacity cannot be negative.");
            }
            return errors;
        }

        public override string ToString() => Value.ToString();
    }
}