namespace Moneybox.Domain.Primitives;

public readonly record struct Balance
{
    public const decimal PayInLimit = 4000m;
    public const decimal LowFundsThreshold = 500m;
    private decimal Amount { get; }
    
    public static Balance New(decimal amount)
    {
        return new Balance(amount);
    }

    public Balance(decimal amount)
    {
        Validation.BasedOn(errors =>
        {
            switch (amount)
            {
                case < 0:
                    errors.Add("Insufficient funds");
                    break;
                case > PayInLimit:
                    errors.Add("Account pay in limit reached");
                    break;
            }
        });
        Amount = amount;
    }

    public override string ToString()
    {
        return Amount.ToString("F");
    }
    
    public bool ApproachingPayInLimit() =>  PayInLimit - Amount < LowFundsThreshold;
    public bool HasLowFunds() => Amount < LowFundsThreshold;

    public static implicit operator decimal(Balance balance) => balance.Amount;
    
    public static implicit operator Balance(decimal amount) => new(amount);
}