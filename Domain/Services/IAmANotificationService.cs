using Moneybox.Domain.Primitives;

namespace Moneybox.Domain.Services
{
    public interface IAmANotificationService
    {
        void NotifyApproachingPayInLimit(Email emailAddress);

        void NotifyFundsLow(Email emailAddress);
    }
}
