using System.Text.RegularExpressions;

namespace Moneybox.Domain.Primitives;

public readonly record struct Email
{
    private string value { get; }

    private Email(string email)
    {
        Validation.BasedOn(errors =>
        {
            if (string.IsNullOrEmpty(email))
            {
                errors.Add("Email cannot be empty");
            }
            else if (!Regex.IsMatch(email,@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                errors.Add("Email must be valid");
            }
        });
        value = email;
    }
    
    public override string ToString()
    {
        return value;
    }

    public static implicit operator string(Email email) => email.value;
    
    public static implicit operator Email(string email) => new(email);
}