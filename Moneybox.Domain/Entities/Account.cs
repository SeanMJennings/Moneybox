using Moneybox.Domain.Primitives;

namespace Moneybox.Domain.Entities
{
    public class Account(Guid id, User user, Money Withdrawn, Money PaidIn) : Aggregate(id)
    {
        public const decimal PayInLimit = 4000m;

        public User User { get; private set; } = user;

        public decimal Balance { get; private set; } = PaidIn - Withdrawn;

        public Money Withdrawn { get; private set; } = Withdrawn;

        public Money PaidIn { get; private set; } = PaidIn;
    }
}
