namespace Moneybox.Domain.Services
{
    public interface IAmANotificationService
    {
        void NotifyApproachingPayInLimit(string emailAddress);

        void NotifyFundsLow(string emailAddress);
    }
}
