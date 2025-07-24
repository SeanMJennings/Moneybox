namespace Moneybox.Domain.Behaviours;

public static class Decimal
{
    public static bool IsOverdrawn(this decimal amount)
    {
        return amount < 0;
    }
    
    public static bool HasLowFunds(this decimal amount)
    {
        return amount < 500m;
    }
}