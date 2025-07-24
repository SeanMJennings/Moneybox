using Moneybox.Domain.Entities;
using Moneybox.Domain.Primitives;
using Shouldly;

namespace Testing.Domain;

public partial class AccountSpecs : Specification
{
    private Guid id;
    private User user = null!;
    private Account account = null!;
    private Money PaidIn = Money.Zero;
    private Money Withdrawn = 100m;

    protected override void before_each()
    {
        base.before_each();
        id = Guid.NewGuid();
        user = null!;
        account = null!;
    }

    private void valid_inputs()
    {
        id = Guid.NewGuid();
        user = new User(Guid.NewGuid(), "Jackie Chan", "jackie.chan@gmail.com");
        
    }

    private void creating_an_account()
    {
        account = new Account(id, user, Withdrawn, PaidIn);
    }    
    
    private void the_account_is_created()
    {
        account.ShouldNotBeNull();
        account.Id.ShouldBe(id);
        account.User.ShouldBe(user);
        account.PaidIn.ShouldBe(PaidIn);
        account.Withdrawn.ShouldBe(Withdrawn);
    }

    private void the_balance_is_calculated_as_difference_between_paidin_and_withdrawal()
    {
        account.Balance.ShouldBe(-100m);
    }
}