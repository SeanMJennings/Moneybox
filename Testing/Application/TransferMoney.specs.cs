namespace Testing.Application;

public partial class TransferMoney
{
    [Test]
    public void alert_insufficient_funds()
    {
        Given(() => a_from_account_with_balance(1000m));
        And(() => a_to_account_with_balance(0m));
        When(Validating(() => transferring(1001m)));
        Then(Informs("Insufficient funds to make transfer"));
    }
    
    [Test]
    public void notify_low_funds()
    {
        Given(() => a_from_account_with_balance(1000m));
        And(() => a_to_account_with_balance(0m));
        When(Validating(() => transferring(501m)));
        Then(low_funds_notification_sent);
    }
    
    [Test]
    public void alert_reached_pay_in_limit()
    {
        Given(() => a_from_account_with_balance(3000m));
        And(() => a_to_account_with_balance(3000m));
        When(Validating(() => transferring(1001m)));
        Then(Informs("Account pay in limit reached"));
    }
    
    [Test]
    public void transfer_money_successfully()
    {
        Given(() => a_from_account_with_balance(1000m));
        And(() => a_to_account_with_balance(0m));
        When(() => transferring(500m));
        Then(transfer_is_successful);
    }
}