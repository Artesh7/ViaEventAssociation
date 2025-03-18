using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Events.Values
{
    public record Description
    {
        public string Value { get; private set; }
        public Description(string value)
        {
            Value = value;
        }
        public static Result<Description> Create(string value)
        {
            List<string> errors = Validate(value);

            return errors.Any() ? new Result<Description>(errors) : new Result<Description>(new Description(value));
        }
        private static List<string> Validate(string value)
        {
            List<string> errors = new();
            if (string.IsNullOrEmpty(value)) errors.Add("Description cannot be empty");
            if (value.Length > 251) errors.Add("Description must be at most 250 characters");

            return errors;
        }
    }
}
