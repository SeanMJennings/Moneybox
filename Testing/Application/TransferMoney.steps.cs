using Moneybox.Domain.Entities;
using Moneybox.Domain.Services;
using Moq;

namespace Testing.Application;

public partial class TransferMoney : Specification
{
    private readonly Guid from_account_id = Guid.NewGuid();
    private readonly Guid to_account_id = Guid.NewGuid();
    private Account from_account = null!;
    private User from_account_user = null!;
    private Account to_account = null!;
    private User to_account_user = null!;
    private Mock<IAmANotificationService> notificationService = null!;

    protected override void before_each()
    {
        base.before_each();
        from_account = null!;
        to_account = null!;
        from_account_user = new User(Guid.NewGuid(), "From User", "from@user.com");
        to_account_user = new User(Guid.NewGuid(), "To User", "to@user.com");
        notificationService = new Mock<IAmANotificationService>();
    }

    private void a_from_account_with_balance(decimal balance)
    {
        from_account = new Account(from_account_id, from_account_user, 0m, balance);
    }

    private void a_to_account_with_balance(decimal balance)
    {
        to_account = new Account(to_account_id, to_account_user, 0m, balance);
    }

    private void transferring(decimal amount)
    {
        var transferMoney = new Moneybox.Application.Features.TransferMoney(
            new InMemoryDataRepository([from_account, to_account]),
            notificationService.Object
        );
        transferMoney.Execute(from_account_id, to_account_id, amount);
    }
    
    private void low_funds_notification_sent()
    {
        notificationService.Verify(
            x => x.NotifyFundsLow(from_account_user.Email),
            Times.Once
        );
    }

    private void transfer_is_successful()
    {
        var fromAccount = new InMemoryDataRepository([from_account]).GetAccountById(from_account_id);
        var toAccount = new InMemoryDataRepository([to_account]).GetAccountById(to_account_id);
        
        Assert.That(fromAccount.Balance, Is.EqualTo(500m));
        Assert.That(toAccount.Balance, Is.EqualTo(500m));
    }
}