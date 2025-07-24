namespace Testing.Domain;

[TestFixture]
public partial class MoneySpecsShould
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
    
    [Test]
    public void multiply_money()
    {
        Given(a_positive_decimal_value);
        And(converting_to_money);
        When(multiplying_by_two);
        Then(the_amount_is_doubled);
    }
}