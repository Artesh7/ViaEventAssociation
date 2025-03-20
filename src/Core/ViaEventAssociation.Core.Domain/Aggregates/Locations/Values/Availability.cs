using System;
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
            if (from >= to)
            {
                return new Result<Availability>(1, "Availability time range is invalid (From >= To).");
            }
            return new Result<Availability>(new Availability(from, to));
        }

        public override string ToString() => $"From {From} to {To}";
    }
}