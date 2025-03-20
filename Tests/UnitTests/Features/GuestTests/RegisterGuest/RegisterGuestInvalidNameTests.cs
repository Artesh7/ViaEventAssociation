using ViaEventAssociation.Core.Domain.Aggregates.Guests;

namespace UnitTests.Features.GuestTests;

public class RegisterGuestInvalidNameTests
{
    [Theory]
    [InlineData("J", "Doe", "john@via.dk", "https://example.com/profile.jpg")] // ❌ First name too short
    [InlineData("John", "D", "john@via.dk", "https://example.com/profile.jpg")] // ❌ Last name too short
    public void RegisterGuest_InvalidName_ShouldReturnError(string firstName, string lastName, string email, string profilePictureUrl)
    {
        // Act
        var result = Guest.RegisterGuest(firstName, lastName, email, profilePictureUrl);

        // Assert
        Assert.NotEqual(0, result.resultCode); 
        Assert.Contains("name", result.errors[0]); // Ensure name-related error message exists
    }
}