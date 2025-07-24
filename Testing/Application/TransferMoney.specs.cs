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
}