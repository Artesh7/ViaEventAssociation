using System;
using Xunit;
using ViaEventAssociation.Core.Domain.Aggregates.Locations;
using ViaEventAssociation.Core.Domain.Aggregates.Locations.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.LocationTests
{
    // Address-tests
    public class AddressTests
    {
        [Fact]
        public void CreateAddress_WithValidData_ReturnsSuccess()
        {
            // Arrange
            int postalCode = 12345;
            string city = "TestCity";
            string street = "Main Street";
            int houseNumber = 42;

            // Act
            var result = Address.Create(postalCode, city, street, houseNumber);

            // Assert
            Assert.Equal(0, result.resultCode);                  // expect success
            Assert.NotNull(result.payLoad);
            Assert.Equal("Main Street 42, 12345 TestCity", result.payLoad.ToString());
        }

        [Fact]
        public void CreateAddress_WithEmptyCity_ReturnsError()
        {
            // Arrange
            int postalCode = 12345;
            string city = "";    
            string street = "Main Street";
            int houseNumber = 42;

            // Act
            var result = Address.Create(postalCode, city, street, houseNumber);

            // Assert
            Assert.NotEqual(0, result.resultCode);  
            // check if it contains "City is required"
            Assert.Contains("City is required", result.errorMessage, StringComparison.OrdinalIgnoreCase);
        }
    }

    // Availability-tests
    public class AvailabilityTests
    {
        [Fact]
        public void CreateAvailability_WithValidRange_ReturnsSuccess()
        {
            // Arrange
            var fromTime = DateTime.UtcNow;
            var toTime = fromTime.AddHours(2);

            // Act
            var result = Availability.Create(fromTime, toTime);

            // Assert
            Assert.Equal(0, result.resultCode);
            Assert.NotNull(result.payLoad);
            Assert.Contains("From", result.payLoad.ToString());
        }

        [Fact]
        public void CreateAvailability_WithInvalidRange_ReturnsError()
        {
            // Arrange
            var time = DateTime.UtcNow;

            // Act
            var result = Availability.Create(time, time);

            // Assert
            Assert.NotEqual(0, result.resultCode);
            Assert.Contains("time range is invalid", result.errorMessage, StringComparison.OrdinalIgnoreCase);
        }
    }

    // LocationId-tests
    public class LocationIdTests
    {
        [Fact]
        public void CreateLocationId_WithValidGuid_ReturnsSuccess()
        {
            // Arrange
            var guid = Guid.NewGuid();

            // Act
            var result = LocationId.Create(guid);

            // Assert
            Assert.Equal(0, result.resultCode);
            Assert.Equal(guid, result.payLoad.Value);
        }

        [Fact]
        public void CreateLocationId_WithEmptyGuid_ReturnsError()
        {
            // Arrange
            var guid = Guid.Empty;

            // Act
            var result = LocationId.Create(guid);

            // Assert
            Assert.NotEqual(0, result.resultCode);
            Assert.Contains("Guid cannot be empty", result.errorMessage, StringComparison.OrdinalIgnoreCase);
        }
    }

    // LocationMaxGuestCapacity-tests
    public class LocationMaxGuestCapacityTests
    {
        [Fact]
        public void CreateLocationMaxGuestCapacity_WithValidValue_ReturnsSuccess()
        {
            // Arrange
            int capacity = 150;

            // Act
            var result = LocationMaxGuestCapacity.Create(capacity);

            // Assert
            Assert.Equal(0, result.resultCode);
            Assert.NotNull(result.payLoad);
            Assert.Equal(capacity, result.payLoad.Value);
        }

        [Fact]
        public void CreateLocationMaxGuestCapacity_WithNegativeValue_ReturnsError()
        {
            // Arrange
            int capacity = -5;

            // Act
            var result = LocationMaxGuestCapacity.Create(capacity);

            // Assert
            Assert.NotEqual(0, result.resultCode);
            Assert.Contains("cannot be negative", result.errorMessage, StringComparison.OrdinalIgnoreCase);
        }
    }

    // LocationName-tests
    public class LocationNameTests
    {
        [Fact]
        public void CreateLocationName_WithValidValue_ReturnsSuccess()
        {
            // Arrange
            var name = "Conference Room";

            // Act
            var result = LocationName.Create(name);

            // Assert
            Assert.Equal(0, result.resultCode);
            Assert.Equal(name, result.payLoad.Value);
        }

        [Fact]
        public void CreateLocationName_WithEmptyValue_ReturnsError()
        {
            // Arrange
            var name = "";

            // Act
            var result = LocationName.Create(name);

            // Assert
            Assert.NotEqual(0, result.resultCode);
            Assert.Contains("cannot be empty", result.errorMessage, StringComparison.OrdinalIgnoreCase);
        }
    }

    // MaxCapacity-tests
    public class MaxCapacityTests
    {
        [Fact]
        public void CreateMaxCapacity_WithValidValue_ReturnsSuccess()
        {
            // Arrange
            int capacity = 100;

            // Act
            var result = MaxCapacity.Create(capacity);

            // Assert
            Assert.Equal(0, result.resultCode);
            Assert.NotNull(result.payLoad);
            Assert.Equal(100, result.payLoad.Value);
        }

        [Fact]
        public void CreateMaxCapacity_WithNegativeValue_ReturnsError()
        {
            // Arrange
            int capacity = -1;

            // Act
            var result = MaxCapacity.Create(capacity);

            // Assert
            Assert.NotEqual(0, result.resultCode);
            Assert.Contains("cannot be negative", result.errorMessage, StringComparison.OrdinalIgnoreCase);
        }
    }

    // Location-aggregate-tests
    public class LocationTests
    {
        // help method to create a valid Location.
        private Result<Location> CreateValidLocation()
        {
            var nameResult = LocationName.Create("Valid Name");
            Assert.Equal(0, nameResult.resultCode);

            var capacityResult = MaxCapacity.Create(100);
            Assert.Equal(0, capacityResult.resultCode);

            var fromTime = DateTime.UtcNow;
            var toTime = fromTime.AddHours(2);
            var availabilityResult = Availability.Create(fromTime, toTime);
            Assert.Equal(0, availabilityResult.resultCode);

            var addressResult = Address.Create(12345, "TestCity", "Main Street", 42);
            Assert.Equal(0, addressResult.resultCode);

            return Location.AddLocation(
                nameResult.payLoad,
                capacityResult.payLoad,
                availabilityResult.payLoad,
                addressResult.payLoad
            );
        }

        [Fact]
        public void AddLocation_WithValidData_ReturnsSuccess()
        {
            // Arrange
            var nameResult = LocationName.Create("Conference Room");
            var capacityResult = MaxCapacity.Create(50);
            var fromTime = DateTime.UtcNow;
            var toTime = fromTime.AddHours(2);
            var availabilityResult = Availability.Create(fromTime, toTime);
            var addressResult = Address.Create(12345, "SampleCity", "SampleStreet", 10);

            // expect success in all helper-objects
            Assert.Equal(0, nameResult.resultCode);
            Assert.Equal(0, capacityResult.resultCode);
            Assert.Equal(0, availabilityResult.resultCode);
            Assert.Equal(0, addressResult.resultCode);

            // Act
            var result = Location.AddLocation(
                nameResult.payLoad,
                capacityResult.payLoad,
                availabilityResult.payLoad,
                addressResult.payLoad);

            // Assert
            Assert.Equal(0, result.resultCode);
            Assert.NotNull(result.payLoad);
            Assert.NotNull(result.payLoad.Id);
            Assert.Equal("Conference Room", result.payLoad.Name.Value);
            Assert.Equal(50, result.payLoad.MaxCapacity.Value);
            Assert.Equal(12345, result.payLoad.Address.PostalCode);
        }

        [Fact]
        public void AddLocation_WithEmptyName_ReturnsError()
        {
            // Arrange
            var nameResult = LocationName.Create(""); //invalid
            // Assert
            Assert.NotEqual(0, nameResult.resultCode);
            Assert.Contains("cannot be empty", nameResult.errorMessage, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void AddLocation_WithNegativeCapacity_ReturnsError()
        {
            // Arrange
            var capacityResult = MaxCapacity.Create(-1);
            // Assert
            Assert.NotEqual(0, capacityResult.resultCode);
            Assert.Contains("cannot be negative", capacityResult.errorMessage, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void AddLocation_WithInvalidAvailability_ReturnsError()
        {
            // Arrange
            var nameResult = LocationName.Create("Valid Name");
            Assert.Equal(0, nameResult.resultCode);

            var capacityResult = MaxCapacity.Create(100);
            Assert.Equal(0, capacityResult.resultCode);

            // fromTime == toTime => invslid
            var fixedTime = new DateTime(2025, 1, 1, 12, 0, 0, DateTimeKind.Utc);
            var availabilityResult = Availability.Create(fixedTime, fixedTime);

            // Assert
            Assert.NotEqual(0, availabilityResult.resultCode);
            Assert.Contains("time range is invalid", availabilityResult.errorMessage, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void AddLocation_WithEmptyCity_ReturnsError()
        {
            // Arrange
            var addressResult = Address.Create(12345, "", "Main Street", 42);
            // Assert
            Assert.NotEqual(0, addressResult.resultCode);
            Assert.Contains("City is required", addressResult.errorMessage, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void UpdateName_WithValidName_ReturnsSuccess()
        {
            // Arrange
            var createResult = CreateValidLocation();
            Assert.Equal(0, createResult.resultCode);
            var location = createResult.payLoad;

            var newNameResult = LocationName.Create("New Room Name");
            Assert.Equal(0, newNameResult.resultCode);

            // Act
            var updateResult = location.UpdateName(newNameResult.payLoad);

            // Assert
            Assert.Equal(0, updateResult.resultCode);
            Assert.Equal("New Room Name", location.Name.Value);
        }

        [Fact]
        public void UpdateName_WithEmptyName_ReturnsError()
        {
            // Arrange
            var createResult = CreateValidLocation();
            Assert.Equal(0, createResult.resultCode);
            var location = createResult.payLoad;

            var newNameResult = LocationName.Create(""); // invalid
            Assert.NotEqual(0, newNameResult.resultCode);
            Assert.Contains("cannot be empty", newNameResult.errorMessage, StringComparison.OrdinalIgnoreCase);

            Assert.Equal("Valid Name", location.Name.Value);
        }

        [Fact]
        public void SetMaximumCapacity_WithValidValue_ReturnsSuccess()
        {
            // Arrange
            var createResult = CreateValidLocation();
            var location = createResult.payLoad;

            var newCapacityResult = MaxCapacity.Create(500);
            Assert.Equal(0, newCapacityResult.resultCode);

            // Act
            var updateResult = location.SetMaximumCapacity(newCapacityResult.payLoad);

            // Assert
            Assert.Equal(0, updateResult.resultCode);
            Assert.Equal(500, location.MaxCapacity.Value);
        }

        [Fact]
        public void SetMaximumCapacity_WithNegativeValue_ReturnsError()
        {
            // Arrange
            var createResult = CreateValidLocation();
            var location = createResult.payLoad;

            var newCapacityResult = MaxCapacity.Create(-10); //invalid
            Assert.NotEqual(0, newCapacityResult.resultCode);
            Assert.Contains("cannot be negative", newCapacityResult.errorMessage, StringComparison.OrdinalIgnoreCase);
            
            // Checks that the old capacity still applies
            Assert.Equal(100, location.MaxCapacity.Value);
        }

        [Fact]
        public void SetAvailability_WithValidRange_ReturnsSuccess()
        {
            // Arrange
            var createResult = CreateValidLocation();
            var location = createResult.payLoad;

            var newFrom = DateTime.UtcNow.AddDays(1);
            var newTo = newFrom.AddHours(3);
            var newAvailabilityResult = Availability.Create(newFrom, newTo);
            Assert.Equal(0, newAvailabilityResult.resultCode);

            // Act
            var updateResult = location.SetAvailability(newAvailabilityResult.payLoad);

            // Assert
            Assert.Equal(0, updateResult.resultCode);
            Assert.Equal(newFrom, location.Availability.From);
            Assert.Equal(newTo, location.Availability.To);
        }

        [Fact]
        public void SetAvailability_WithInvalidRange_ReturnsError()
        {
            // Arrange
            var createResult = CreateValidLocation();
            var location = createResult.payLoad;

            var fixedTime = new DateTime(2025, 1, 1, 12, 0, 0, DateTimeKind.Utc);
            var newAvailabilityResult = Availability.Create(fixedTime, fixedTime);
            Assert.NotEqual(0, newAvailabilityResult.resultCode);
            Assert.Contains("time range is invalid", newAvailabilityResult.errorMessage, StringComparison.OrdinalIgnoreCase);

            // Checks that the old availability still applies
            Assert.True(location.Availability.From < location.Availability.To);
        }

        [Fact]
        public void SetAddress_WithValidData_ReturnsSuccess()
        {
            // Arrange
            var createResult = CreateValidLocation();
            var location = createResult.resultCode == 0 ? createResult.payLoad : null;

            var newAddressResult = Address.Create(99999, "NewCity", "SecondStreet", 101);
            Assert.Equal(0, newAddressResult.resultCode);

            // Act
            var updateResult = location.SetAddress(newAddressResult.payLoad);

            // Assert
            Assert.Equal(0, updateResult.resultCode);
            Assert.Equal(99999, location.Address.PostalCode);
            Assert.Equal("NewCity", location.Address.City);
            Assert.Equal("SecondStreet", location.Address.Street);
            Assert.Equal(101, location.Address.HouseNumber);
        }

        [Fact]
        public void SetAddress_WithEmptyCity_ReturnsError()
        {
            // Arrange
            var createResult = CreateValidLocation();
            var location = createResult.payLoad;

            var badAddressResult = Address.Create(99999, "", "SecondStreet", 101);
            Assert.NotEqual(0, badAddressResult.resultCode);
            Assert.Contains("City is required", badAddressResult.errorMessage, StringComparison.OrdinalIgnoreCase);

            // Adresse keep the same
            Assert.Equal("TestCity", location.Address.City);
        }
    }
}
