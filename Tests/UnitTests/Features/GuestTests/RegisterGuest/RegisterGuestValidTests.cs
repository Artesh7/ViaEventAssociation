using ViaEventAssociation.Core.Domain.Aggregates.Guests;

namespace UnitTests.Features.GuestTests;

public class RegisterGuestValidTests
{
    [Fact]
    public void RegisterGuest_ValidInput_ShouldCreateGuestSuccessfully()
    {
        // Arrange
        string firstName = "John";
        string lastName = "Doe";
        string email = "john@via.dk";
        string profilePictureUrl = "https://example.com/profile.jpg";

        // Act
        var result = Guest.RegisterGuest(firstName, lastName, email, profilePictureUrl);

        // Assert
        Assert.Equal(0, result.resultCode); // ✅ Expecting success
        Assert.NotNull(result.payLoad);
        Assert.Equal(email, result.payLoad.Email.Value);
        Assert.Equal(firstName, result.payLoad.Name.FirstName);
        Assert.Equal(lastName, result.payLoad.Name.LastName);
    }
}
