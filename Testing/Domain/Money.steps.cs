using Moneybox.Domain.Primitives;
using Shouldly;

namespace Testing.Domain;

public partial class MoneySpecs : Specification
{
    private const decimal positive_amount = 123.4567m;
    private const decimal negative_amount = -123.4567m;
    private decimal amount;
    private Money money;

    protected override void before_each()
    {
        base.before_each();
        amount = 0m;
        money = Money.Zero;
    }

    private void a_positive_decimal_value()
    {
        amount = positive_amount;
    }    
    
    private void a_negative_decimal_value()
    {
        amount = negative_amount;
    }

    private void converting_to_money()
    {
        money = new Money(amount);
    }

    private void money_is_formatted_to_2_dp()
    {
        money.ToString().ShouldBe($"{Math.Round(positive_amount, 2)}");
        ((decimal)money).ShouldBe(positive_amount);
    }
}