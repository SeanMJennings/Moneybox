namespace Testing.Domain;

public partial class AccountSpecs
{
    [Test]
    public void can_create_valid_account()
    {
        Given(valid_inputs);
        When(creating_an_account);
        Then(the_account_is_created);
        And(the_balance_is_calculated_as_difference_between_paidin_and_withdrawal);
    }
}