namespace Testing.Domain;

public partial class MoneySpecs
{
    [Test]
    public void provide_amount_to_2_dp()
    {
        Given(a_positive_decimal_value);
        When(converting_to_money);
        Then(money_is_formatted_to_2_dp);
    }
    
    [Test]
    public void not_allow_negative_amount()
    {
        Given(a_negative_decimal_value);
        When(Validating(converting_to_money));
        Then(Informs("Amount cannot be negative."));
    }
}