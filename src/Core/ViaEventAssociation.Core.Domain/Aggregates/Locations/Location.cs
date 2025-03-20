using System;
using ViaEventAssociation.Core.Domain.Aggregates.Locations.Values;
using ViaEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Locations
{
    public sealed class Location : AggregateRoot<LocationId>
    {
        private LocationName _name;
        private MaxCapacity _maxCapacity;
        private Availability _availability;
        private Address _address;

        // Private constructor ensures creation is controlled by AddLocation
        private Location(
            LocationId id,
            LocationName name,
            MaxCapacity maxCapacity,
            Availability availability,
            Address address
        ) : base(id)
        {
            _name = name;
            _maxCapacity = maxCapacity;
            _availability = availability;
            _address = address;
        }

        public static Result<Location> AddLocation(
            LocationName name,
            MaxCapacity capacity,
            Availability availability,
            Address address)
        {
            // Create the LocationId via its factory method (always valid).
            var idResult = LocationId.Create();
            // If you want to check it anyway, you can, but it never fails.

            var location = new Location(
                idResult.payLoad,
                name,
                capacity,
                availability,
                address
            );

            return new Result<Location>(location);
        }

        // Since validation is handled by the value objects, we just set the fields.
        public Result<Location> UpdateName(LocationName newName)
        {
            _name = newName;
            return new Result<Location>(this);
        }

        public Result<Location> SetMaximumCapacity(MaxCapacity newCapacity)
        {
            _maxCapacity = newCapacity;
            return new Result<Location>(this);
        }

        public Result<Location> SetAvailability(Availability newAvailability)
        {
            _availability = newAvailability;
            return new Result<Location>(this);
        }

        public Result<Location> SetAddress(Address newAddress)
        {
            _address = newAddress;
            return new Result<Location>(this);
        }

        // Expose read-only properties to match UML
        public LocationName Name => _name;
        public MaxCapacity MaxCapacity => _maxCapacity;
        public Availability Availability => _availability;
        public Address Address => _address;
    }
}
