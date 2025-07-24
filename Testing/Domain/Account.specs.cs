namespace Testing.Domain;

public partial class AccountSpecs
{
    [Test]
    public void cannot_create_account_with_negative_balance()
    {
        Given(negative_balance);
        When(Validating(creating_an_account));
        Then(Informs("Insufficient funds"));
    }
    
    [Test]
    public void cannot_create_account_with_balance_exceeding_pay_in_limit()
    {
        Given(somebody_with_pay_in_limit_exceeded);
        When(Validating(creating_an_account));
        Then(Informs("Account pay in limit reached"));
    }
    
    [Test]
    public void can_create_valid_account()
    {
        Given(valid_inputs);
        When(creating_an_account);
        Then(the_account_is_created);
        And(the_balance_is_calculated_as_difference_between_paidin_and_withdrawal);
    }
}