using ViaEventAssociation.Core.Domain.Aggregates.Guests;

namespace UnitTests.Features.GuestTests;

public class RegisterGuestTests
{
        [Theory]
        [InlineData("John", "Doe", "john@via.dk", "https://example.com/profile.jpg")] // Valid Case
        public void RegisterGuest_ValidInput_ShouldCreateGuestSuccessfully(string firstName, string lastName, string email, string profilePictureUrl)
        {
            // Act
            var result = Guest.RegisterGuest(firstName, lastName, email, profilePictureUrl);

            // Assert
            Assert.Equal(0, result.resultCode); // Success
            Assert.NotNull(result.payLoad);
            Assert.Equal(email, result.payLoad.Email.Value);
            Assert.Equal(firstName, result.payLoad.Name.FirstName);
            Assert.Equal(lastName, result.payLoad.Name.LastName);
        }

        [Theory]
        [InlineData("John", "Doe", "invalid-email", "https://example.com/profile.jpg")] // Invalid email format
        [InlineData("John", "Doe", "john@gmail.com", "https://example.com/profile.jpg")] // Wrong domain
        public void RegisterGuest_InvalidEmail_ShouldReturnError(string firstName, string lastName, string email, string profilePictureUrl)
        {
            // Act
            var result = Guest.RegisterGuest(firstName, lastName, email, profilePictureUrl);

            // Assert
            Assert.NotEqual(0, result.resultCode);
            Assert.Contains("Email", result.errors[0]); // Ensure email-related error message exists
        }

        [Theory]
        [InlineData("J", "Doe", "john@via.dk", "https://example.com/profile.jpg")] // First name too short
        [InlineData("John", "D", "john@via.dk", "https://example.com/profile.jpg")] // Last name too short
        public void RegisterGuest_InvalidName_ShouldReturnError(string firstName, string lastName, string email, string profilePictureUrl)
        {
            // Act
            var result = Guest.RegisterGuest(firstName, lastName, email, profilePictureUrl);

            // Assert
            Assert.NotEqual(0, result.resultCode);
            Assert.Contains("name", result.errors[0]); // Ensure name-related error message exists
        }

        [Theory]
        [InlineData("John", "Doe", "john@via.dk", "invalid-url")] // Invalid profile picture URL
        public void RegisterGuest_InvalidProfilePictureURL_ShouldReturnError(string firstName, string lastName, string email, string profilePictureUrl)
        {
            // Act
            var result = Guest.RegisterGuest(firstName, lastName, email, profilePictureUrl);

            // Assert
            Assert.NotEqual(0, result.resultCode);
            Assert.Contains("profile picture", result.errors[0]); // Ensure URL-related error message exists
        }
}