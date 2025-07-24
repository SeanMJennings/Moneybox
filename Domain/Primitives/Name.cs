using System.Text.RegularExpressions;

namespace Moneybox.Domain.Primitives;

public readonly record struct Name
{
    private string value { get; }

    private Name(string name)
    {
        Validation.BasedOn(errors =>
        {
            if (string.IsNullOrEmpty(name))
            {
                errors.Add("Name cannot be empty");
            }
            else if (Regex.IsMatch(name,@"[^a-zA-Z\s]", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                errors.Add("Name can only have alphabetical characters");
            }
        });
        value = name;
    }
    
    public override string ToString()
    {
        return value;
    }

    public static implicit operator string(Name name) => name.value;
    
    public static implicit operator Name(string name) => new(name);
}