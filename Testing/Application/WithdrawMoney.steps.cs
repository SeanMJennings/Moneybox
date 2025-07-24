using Moneybox.Domain.Entities;
using Moneybox.Domain.Services;
using Moq;

namespace Testing.Application;

public partial class WithdrawMoney : Specification
{
    private readonly Guid from_account_id = Guid.NewGuid();
    private Account from_account = null!;
    private User from_account_user = null!;
    private Mock<IAmANotificationService> notificationService = null!;

    protected override void before_each()
    {
        base.before_each();
        from_account = null!;
        from_account_user = new User(Guid.NewGuid(), "From User", "from@user.com");
        notificationService = new Mock<IAmANotificationService>();
    }

    private void a_from_account_with_balance(decimal balance)
    {
        from_account = new Account(from_account_id, from_account_user, 0m, balance);
    }

    private void withdrawing(decimal amount)
    {
        var withdrawMoney = new Moneybox.Application.Features.WithdrawMoney(
            new InMemoryDataRepository([from_account]),
            notificationService.Object
        );
        withdrawMoney.Execute(from_account_id, amount);
    }

    private void low_funds_notification_sent()
    {
        notificationService.Verify(x => x.NotifyFundsLow(from_account_user.Email), Times.Once);
    }

    private void withdrawal_is_successful()
    {
        var account = new InMemoryDataRepository([from_account]).GetAccountById(from_account_id);
        Assert.That(account.Balance, Is.EqualTo(500m));
    }
}