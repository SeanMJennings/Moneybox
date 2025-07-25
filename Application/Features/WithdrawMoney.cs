using System;
using Moneybox.Application.DataAccess;
using Moneybox.Domain.Entities;
using Moneybox.Domain.Primitives;
using Moneybox.Domain.Services;

namespace Moneybox.Application.Features;

public class WithdrawMoney(IAmAnAccountRepository AccountRepository, IAmANotificationService NotificationService)
{
    public void Execute(Guid fromAccountId, Money amount)
    {
        var account = AccountRepository.GetAccountById(fromAccountId);
        account.Withdraw(amount);
        AccountRepository.Update(account);
        NotifyWithdrawingAccount(account);
    }

    private void NotifyWithdrawingAccount(Account account)
    {
        if (account.Balance.HasLowFunds()) NotificationService.NotifyFundsLow(account.User.Email);
    }
}