namespace Testing.Application;

public partial class WithdrawMoney
{
    [Test]
    public void alert_insufficient_funds()
    {
        Given(() => a_from_account_with_balance(100m));
        When(Validating(() => withdrawing(101m)));
        Then(Informs("Insufficient funds to make withdrawal"));
    }

    [Test]
    public void notify_low_funds()
    {
        Given(() => a_from_account_with_balance(1000m));
        When(() => withdrawing(501m));
        Then(low_funds_notification_sent);
    }

    [Test]
    public void withdraw_money_successfully()
    {
        Given(() => a_from_account_with_balance(1000m));
        When(() => withdrawing(500m));
        Then(withdrawal_is_successful);
    }
}