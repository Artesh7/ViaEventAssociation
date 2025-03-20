using ViaEventAssociation.Core.Domain.Aggregates.Guests;

namespace UnitTests.Features.GuestTests;

public class RegisterGuestInvalidProfilePictureTests
{
    [Theory]
    [InlineData("John", "Doe", "john@via.dk", "invalid-url")] // ❌ Invalid profile picture URL
    public void RegisterGuest_InvalidProfilePictureURL_ShouldReturnError(string firstName, string lastName, string email, string profilePictureUrl)
    {
        // Act
        var result = Guest.RegisterGuest(firstName, lastName, email, profilePictureUrl);

        // Assert
        Assert.NotEqual(0, result.resultCode); 
        Assert.Contains("profile picture", result.errors[0]); // Ensure URL-related error message exists
    }
}