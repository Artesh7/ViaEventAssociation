using ViaEventAssociation.Core.Domain.Common.Bases;
namespace ViaEventAssociation.Core.Domain.Aggregates.Guests;


    public class FirstName
    {
        public string Name { get; }

        private FirstName(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("First name cannot be empty");
            if (value.Length < 2 || value.Length > 25) throw new ArgumentException("First name must be between 2 and 25 characters");

            Name = char.ToUpper(value[0]) + value.Substring(1).ToLower();
        }
        
    }
