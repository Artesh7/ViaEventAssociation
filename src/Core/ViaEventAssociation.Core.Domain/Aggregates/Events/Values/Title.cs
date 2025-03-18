using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Events.Values
{
    public record Title
    {
        public string Value { get; private set; }

        public Title(string value)
        {
            Value = value;
        }
        public static Result<Title> Create(string value)
        {
            List<string> errors = Validate(value);

            return errors.Any() ? new Result<Title>(errors) : new Result<Title>(new Title(value));
        }
        private static List<string> Validate(string value)
        {
            List<string> errors = new();
            if (string.IsNullOrEmpty(value)) errors.Add("Title cannot be empty");
            if (value.Length < 3) errors.Add("Title must be at least 3 characters");
            if (value.Length > 75) errors.Add("Title must be at most 75 characters");

            return errors;
        }
    }
}
