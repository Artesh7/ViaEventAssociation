using ViaEventAssociation.Core.Domain.Aggregates.Guests;

namespace UnitTests.Features.GuestTests;

public class RegisterGuestInvalidEmailTests
{
    [Theory]
    [InlineData("John", "Doe", "invalid-email", "https://example.com/profile.jpg")] // ❌ Invalid email format
    [InlineData("John", "Doe", "john@gmail.com", "https://example.com/profile.jpg")] // ❌ Wrong domain
    public void RegisterGuest_InvalidEmail_ShouldReturnError(string firstName, string lastName, string email, string profilePictureUrl)
    {
        // Act
        var result = Guest.RegisterGuest(firstName, lastName, email, profilePictureUrl);

        // Assert
        Assert.NotEqual(0, result.resultCode); // ❌ Expecting failure
        Assert.Contains("Email", result.errors[0]); // Ensure email-related error message exists
    }
}