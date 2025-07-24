namespace Moneybox.Domain.Primitives;

public readonly record struct Money
{
    private decimal Amount { get; }
    
    public static Money Zero => new(0);

    public Money(decimal amount)
    {
        Validation.BasedOn((errors) =>
        {
            if (amount < 0) errors.Add("Amount cannot be negative.");
        });
        Amount = amount;
    }

    public override string ToString()
    {
        return Amount.ToString("F");
    }
    
    public static implicit operator decimal(Money money) => money.Amount;
    
    public static implicit operator Money(decimal amount) => new(amount);
}