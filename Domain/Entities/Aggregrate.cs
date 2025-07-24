namespace Moneybox.Domain.Entities;

public abstract class Aggregate(Guid Id)
{
    public Guid Id { get; } = Id;
}