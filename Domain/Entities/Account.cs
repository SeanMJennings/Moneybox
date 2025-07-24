using Moneybox.Domain.Primitives;

namespace Moneybox.Domain.Entities
{
    public class Account(Guid id, User user, Money Withdrawn, Money PaidIn) : Aggregate(id)
    {
        private const decimal PayInLimit = 4000m;
        public const decimal LowFundsThreshold = 500m;

        public User User { get; private set; } = user;

        public decimal Balance => PaidIn - Withdrawn;

        public Money Withdrawn { get; private set; } = Withdrawn;

        public Money PaidIn { get; private set; } = PaidIn;
        
        public void Withdraw(Money amount) => Withdrawn += amount;
        public void Deposit(Money amount) => PaidIn += amount;
        public bool WillApproachPayInLimit(Money amount)
        {
            var paidIn = PaidIn + amount;
            return paidIn > PayInLimit || PayInLimit - paidIn < LowFundsThreshold;
        }
        public bool WillExceedPayInLimit(Money amount)
        {
            return PaidIn + amount > PayInLimit;
        }
    }
}
