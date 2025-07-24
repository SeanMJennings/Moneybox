namespace Testing.Application;

public partial class TransferMoneySpecs
{
    [Test]
    public void alert_insufficient_funds()
    {
        Given(() => a_from_account_with_balance(1000m));
        And(() => a_to_account_with_balance(0m));
        When(Validating(() => transferring(1001m)));
        Then(Informs("Insufficient funds"));
    }
    
    [Test]
    public void notify_low_funds()
    {
        Given(() => a_from_account_with_balance(1000m));
        And(() => a_to_account_with_balance(0m));
        When(() => transferring(501m));
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
    public void notify_approaching_pay_in_limit()
    {
        Given(() => a_from_account_with_balance(501m));
        And(() => a_to_account_with_balance(3001m));
        When(() => transferring(500m));
        Then(approaching_pay_in_limit_notification_sent);
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