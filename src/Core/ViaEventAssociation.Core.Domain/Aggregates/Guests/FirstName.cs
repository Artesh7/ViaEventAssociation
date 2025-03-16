using ViaEventAssociation.Core.Domain.Common.Bases;
namespace ViaEventAssociation.Core.Domain.Aggregates.Guests;


    public class FirstName
    {
        public string Name { get; }

        private FirstName(string name)
        {
            this.Name = name;
        }
        
    }
