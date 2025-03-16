using System.Text.RegularExpressions;
using ViaEventAssociation.Core.Domain.Common.Bases;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guests;

public class Email
{
    public string EmailAddress { get; }

    private Email(string emailAddress)
    {
        if (string.IsNullOrWhiteSpace(emailAddress)) throw new ArgumentException("Email cannot be empty");
        if (!IsValidEmail(emailAddress)) throw new ArgumentException("Invalid email format");
        if (!emailAddress.EndsWith("@via.dk")) throw new ArgumentException("Email must end with '@via.dk'");

        EmailAddress = emailAddress.ToLower();
    }
    private static bool IsValidEmail(string email)
    {
        return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
    }

}