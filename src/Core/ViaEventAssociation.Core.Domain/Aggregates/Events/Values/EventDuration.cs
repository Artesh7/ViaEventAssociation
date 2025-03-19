using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Events.Values
{
    public record EventDuration
    {
        public DateTime From { get; private set; }
        public DateTime To { get; private set; }
        public EventDuration(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }
        public static Result<EventDuration> Create(DateTime from, DateTime to)
        {
            List<string> errors = Validate(from,to);

            return errors.Any() ? new Result<EventDuration>(errors) : new Result<EventDuration>(new EventDuration(from, to));
        }
        private static List<string> Validate(DateTime from, DateTime to)
        {
            List<string> errors = new();
            if (from >= to) errors.Add("The 'From' date must be earlier than the 'To' date.");

            // Create DateTime objects representing 8:00 AM and midnight on the same day as 'from'
            DateTime eightAM = from.Date.AddHours(8);
            DateTime midnight = from.Date.AddDays(1); // Midnight of the next day
            DateTime oneAm = from.Date.AddHours(1);

            if (from < eightAM) errors.Add("The 'From' time cannot be before 8:00 AM.");
            if (from >= midnight) errors.Add("The 'From' time cannot be after midnight.");
            if (to <= oneAm) errors.Add("The 'To' time cannot be after 1 AM");

            // Validate that the period between 'from' and 'to' is smaller or equal to 10 hours
            TimeSpan duration = to - from;
            if (duration > TimeSpan.FromHours(10)) errors.Add("The duration between 'From' and 'To' cannot be more than 10 hours.");

            return errors;
        }
    }
}
