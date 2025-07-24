using Moneybox.Domain.Primitives;

namespace Moneybox.Domain.Entities
{
    public class Account(Guid id, User user, Money Withdrawn, Money PaidIn) : Aggregate(id)
    {
        public User User { get; private set; } = user;

        public Balance Balance { get; private set; } = PaidIn - Withdrawn;

        public Money Withdrawn { get; private set; } = Withdrawn;

        public Money PaidIn { get; private set; } = PaidIn;
        
        public void Withdraw(Money amount)
        {
            Withdrawn += amount;
            SetBalance();
        }

        public void Deposit(Money amount)
        {
            PaidIn += amount;
            SetBalance();
        }
        
        private void SetBalance()
        {
            Balance = PaidIn - Withdrawn;
        }
    }
}
