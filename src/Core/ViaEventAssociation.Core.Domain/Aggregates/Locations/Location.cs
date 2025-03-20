using System;
using System.Collections.Generic;
using System.Linq;
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
            // Use a separate validation method to collect any errors.
            List<string> errors = Validate(name, capacity, availability, address);
            if (errors.Any())
            {
                return new Result<Location>(errors);
            }

            // Create LocationId via its factory method.
            var idResult = LocationId.Create(Guid.NewGuid());
            if (idResult.resultCode != 0)
            {
                return new Result<Location>(idResult.errors);
            }

            var location = new Location(idResult.payLoad, name, capacity, availability, address);
            return new Result<Location>(location);
        }

        private static List<string> Validate(
            LocationName name,
            MaxCapacity capacity,
            Availability availability,
            Address address)
        {
            List<string> errors = new();
            if (string.IsNullOrWhiteSpace(name.Value))
            {
                errors.Add("Location name cannot be empty.");
            }
            if (capacity.Value < 0)
            {
                errors.Add("Max capacity cannot be negative.");
            }
            if (availability.From >= availability.To)
            {
                errors.Add("Availability time range is invalid (From >= To).");
            }
            if (string.IsNullOrWhiteSpace(address.City))
            {
                errors.Add("City is required.");
            }
            return errors;
        }
        
        public Result<Location> UpdateName(LocationName newName)
        {
            if (string.IsNullOrWhiteSpace(newName.Value))
            {
                return new Result<Location>(1, "New location name cannot be empty.");
            }

            _name = newName;
            return new Result<Location>(this);
        }
        
        public Result<Location> SetMaximumCapacity(MaxCapacity newCapacity)
        {
            if (newCapacity.Value < 0)
            {
                return new Result<Location>(2, "Max capacity cannot be negative.");
            }

            _maxCapacity = newCapacity;
            return new Result<Location>(this);
        }
        
        public Result<Location> SetAvailability(Availability newAvailability)
        {
            if (newAvailability.From >= newAvailability.To)
            {
                return new Result<Location>(3, "Availability time range is invalid (From >= To).");
            }

            _availability = newAvailability;
            return new Result<Location>(this);
        }
        
        public Result<Location> SetAddress(Address newAddress)
        {
            if (string.IsNullOrWhiteSpace(newAddress.City))
            {
                return new Result<Location>(4, "City is required.");
            }

            _address = newAddress;
            return new Result<Location>(this);
        }

        // Expose read-only properties to match UML attribute names
        public LocationName Name => _name;
        public MaxCapacity MaxCapacity => _maxCapacity;
        public Availability Availability => _availability;
        public Address Address => _address;
    }
}
