using Moneybox.Domain.Primitives;

namespace Moneybox.Domain.Entities
{
    public class Account(Guid id, User user, Money Withdrawn, Money PaidIn) : Aggregate(id)
    {
        public const decimal PayInLimit = 4000m;
        public const decimal LowFundsThreshold = 500m;

        public User User { get; private set; } = user;

        public decimal Balance => PaidIn - Withdrawn;

        public Money Withdrawn { get; private set; } = Withdrawn;

        public Money PaidIn { get; private set; } = PaidIn;
        
        public void Withdraw(Money amount) => Withdrawn += amount;
        public void Deposit(Money amount) => PaidIn += amount;
    }
}
