using ViaEventAssociation.Core.Domain.Common.Bases;
namespace ViaEventAssociation.Core.Domain.Aggregates.Guests;


    public class GuestName
    {
        public string Name { get; }

        private GuestName(string name)
        {
            this.Name = name;
        }
        
    }
