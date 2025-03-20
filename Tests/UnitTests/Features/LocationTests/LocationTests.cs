namespace UnitTests.Features.LocationTests
{
    using System;
    using Xunit;
    using ViaEventAssociation.Core.Domain.Aggregates.Locations;
    using ViaEventAssociation.Core.Domain.Aggregates.Locations.Values;
    using ViaEventAssociation.Core.Tools.OperationResult;

    public class LocationTests
    {
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
                addressResult.payLoad);
        }

        [Fact]
        public void AddLocation_WithValidData_ReturnsSuccess()
        {
            var nameResult = LocationName.Create("Conference Room");
            Assert.Equal(0, nameResult.resultCode);

            var capacityResult = MaxCapacity.Create(50);
            Assert.Equal(0, capacityResult.resultCode);

            var fromTime = DateTime.UtcNow;
            var toTime = fromTime.AddHours(2);
            var availabilityResult = Availability.Create(fromTime, toTime);
            Assert.Equal(0, availabilityResult.resultCode);

            var addressResult = Address.Create(12345, "SampleCity", "SampleStreet", 10);
            Assert.Equal(0, addressResult.resultCode);

            var result = Location.AddLocation(
                nameResult.payLoad,
                capacityResult.payLoad,
                availabilityResult.payLoad,
                addressResult.payLoad);

            Assert.NotNull(result);
            Assert.Equal(0, result.resultCode);
            Assert.NotNull(result.payLoad);

            var location = result.payLoad;
            Assert.NotNull(location.Id); // Forudsætter at AggregateRoot eksponerer en Id-property
            Assert.Equal("Conference Room", location.Name.Value);
            Assert.Equal(50, location.MaxCapacity.Value);
            Assert.Equal(12345, location.Address.PostalCode);
        }

        [Fact]
        public void AddLocation_WithEmptyName_ReturnsError()
        {
            // Tester factory for LocationName med tomt navn
            var nameResult = LocationName.Create("");
            Assert.NotEqual(0, nameResult.resultCode);
            Assert.Contains("cannot be empty", nameResult.errorMessage, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void AddLocation_WithNegativeCapacity_ReturnsError()
        {
            // Tester factory for MaxCapacity med negativ værdi
            var capacityResult = MaxCapacity.Create(-1);
            Assert.NotEqual(0, capacityResult.resultCode);
            Assert.Contains("cannot be negative", capacityResult.errorMessage, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void AddLocation_WithInvalidAvailability_ReturnsError()
        {
            var nameResult = LocationName.Create("Valid Name");
            Assert.Equal(0, nameResult.resultCode);

            var capacityResult = MaxCapacity.Create(100);
            Assert.Equal(0, capacityResult.resultCode);

            var fixedTime = new DateTime(2025, 01, 01, 12, 0, 0, DateTimeKind.Utc);
            var availabilityResult = Availability.Create(fixedTime, fixedTime);
            Assert.NotEqual(0, availabilityResult.resultCode);
            Assert.Contains("time range is invalid", availabilityResult.errorMessage, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void AddLocation_WithEmptyCity_ReturnsError()
        {
            // Tester factory for Address med tomt by-navn
            var addressResult = Address.Create(12345, "", "Main Street", 42);
            Assert.NotEqual(0, addressResult.resultCode);
            Assert.Contains("City is required", addressResult.errorMessage, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void UpdateName_WithValidName_ReturnsSuccess()
        {
            var createResult = CreateValidLocation();
            Assert.Equal(0, createResult.resultCode);
            var location = createResult.payLoad;

            var newNameResult = LocationName.Create("New Room Name");
            Assert.Equal(0, newNameResult.resultCode);

            var updateResult = location.UpdateName(newNameResult.payLoad);

            Assert.Equal(0, updateResult.resultCode);
            Assert.Equal("New Room Name", location.Name.Value);
        }

        [Fact]
        public void UpdateName_WithEmptyName_ReturnsError()
        {
            var createResult = CreateValidLocation();
            Assert.Equal(0, createResult.resultCode);
            var location = createResult.payLoad;

            // Da den nye factory forhindrer oprettelse af et tomt navn,
            // testes tomt navn direkte på factory-niveau.
            var newNameResult = LocationName.Create("");
            Assert.NotEqual(0, newNameResult.resultCode);
            Assert.Contains("cannot be empty", newNameResult.errorMessage, StringComparison.OrdinalIgnoreCase);

            // Derfor kaldes UpdateName ikke med en ugyldig værdi.
            // Vi kan dog bekræfte, at locationens navn forbliver uændret.
            Assert.Equal("Valid Name", location.Name.Value);
        }

        [Fact]
        public void SetMaximumCapacity_WithValidValue_ReturnsSuccess()
        {
            var createResult = CreateValidLocation();
            Assert.Equal(0, createResult.resultCode);
            var location = createResult.payLoad;

            var newCapacityResult = MaxCapacity.Create(500);
            Assert.Equal(0, newCapacityResult.resultCode);

            var updateResult = location.SetMaximumCapacity(newCapacityResult.payLoad);

            Assert.Equal(0, updateResult.resultCode);
            Assert.Equal(500, location.MaxCapacity.Value);
        }

        [Fact]
        public void SetMaximumCapacity_WithNegativeValue_ReturnsError()
        {
            var createResult = CreateValidLocation();
            Assert.Equal(0, createResult.resultCode);
            var location = createResult.payLoad;

            var newCapacityResult = MaxCapacity.Create(-10);
            Assert.NotEqual(0, newCapacityResult.resultCode);
            Assert.Contains("cannot be negative", newCapacityResult.errorMessage, StringComparison.OrdinalIgnoreCase);

            // Kapaciteten skal forblive uændret
            Assert.Equal(100, location.MaxCapacity.Value);
        }

        [Fact]
        public void SetAvailability_WithValidRange_ReturnsSuccess()
        {
            var createResult = CreateValidLocation();
            Assert.Equal(0, createResult.resultCode);
            var location = createResult.payLoad;

            var newFrom = DateTime.UtcNow.AddDays(1);
            var newTo = newFrom.AddHours(3);
            var newAvailabilityResult = Availability.Create(newFrom, newTo);
            Assert.Equal(0, newAvailabilityResult.resultCode);

            var updateResult = location.SetAvailability(newAvailabilityResult.payLoad);

            Assert.Equal(0, updateResult.resultCode);
            Assert.Equal(newFrom, location.Availability.From);
            Assert.Equal(newTo, location.Availability.To);
        }

        [Fact]
        public void SetAvailability_WithInvalidRange_ReturnsError()
        {
            var createResult = CreateValidLocation();
            Assert.Equal(0, createResult.resultCode);
            var location = createResult.payLoad;

            var fixedTime = new DateTime(2025, 1, 1, 12, 0, 0, DateTimeKind.Utc);
            var newAvailabilityResult = Availability.Create(fixedTime, fixedTime);
            Assert.NotEqual(0, newAvailabilityResult.resultCode);
            Assert.Contains("time range is invalid", newAvailabilityResult.errorMessage, StringComparison.OrdinalIgnoreCase);

            // Tilgængeligheden skal forblive uændret
            Assert.True(location.Availability.From < location.Availability.To);
        }

        [Fact]
        public void SetAddress_WithValidData_ReturnsSuccess()
        {
            var createResult = CreateValidLocation();
            Assert.Equal(0, createResult.resultCode);
            var location = createResult.payLoad;

            var newAddressResult = Address.Create(99999, "NewCity", "SecondStreet", 101);
            Assert.Equal(0, newAddressResult.resultCode);

            var updateResult = location.SetAddress(newAddressResult.payLoad);

            Assert.Equal(0, updateResult.resultCode);
            Assert.Equal(99999, location.Address.PostalCode);
            Assert.Equal("NewCity", location.Address.City);
            Assert.Equal("SecondStreet", location.Address.Street);
            Assert.Equal(101, location.Address.HouseNumber);
        }

        [Fact]
        public void SetAddress_WithEmptyCity_ReturnsError()
        {
            var createResult = CreateValidLocation();
            Assert.Equal(0, createResult.resultCode);
            var location = createResult.payLoad;

            var badAddressResult = Address.Create(99999, "", "SecondStreet", 101);
            Assert.NotEqual(0, badAddressResult.resultCode);
            Assert.Contains("City is required", badAddressResult.errorMessage, StringComparison.OrdinalIgnoreCase);

            // Adressen skal forblive uændret
            Assert.Equal("TestCity", location.Address.City);
        }
    }
}
