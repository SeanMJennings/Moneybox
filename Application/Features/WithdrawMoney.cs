using System;
using Moneybox.Application.DataAccess;
using Moneybox.Domain.Entities;
using Moneybox.Domain.Primitives;
using Moneybox.Domain.Services;

namespace Moneybox.Application.Features;

public class WithdrawMoney(IAmAnAccountRepository AccountRepository, IAmANotificationService NotificationService)
{
    public void Execute(Guid fromAccountId, decimal amount)
    {
        var account = AccountRepository.GetAccountById(fromAccountId);
        account.Withdraw(amount);
        AccountRepository.Update(account);
        NotifyOnNewBalance(account);
    }

    private void NotifyOnNewBalance(Account account)
    {
        if (account.Balance < Account.LowFundsThreshold) NotificationService.NotifyFundsLow(account.User.Email);
    }
}