using System;
using System.Collections.Generic;
using System.Linq;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Locations.Values
{
    public sealed record Availability
    {
        public DateTime From { get; init; }
        public DateTime To { get; init; }

        private Availability(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }

        public static Result<Availability> Create(DateTime from, DateTime to)
        {
            List<string> errors = Validate(from, to);
            return errors.Any() ? new Result<Availability>(errors) : new Result<Availability>(new Availability(from, to));
        }

        private static List<string> Validate(DateTime from, DateTime to)
        {
            List<string> errors = new();
            if (from >= to)
            {
                errors.Add("Availability time range is invalid (From >= To).");
            }
            return errors;
        }

        public override string ToString() => $"From {From} to {To}";
    }
}