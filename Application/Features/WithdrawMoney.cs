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
        var newBalance = account.Balance - amount;

        CheckWithdrawal(newBalance, account.User.Email);

        account.Withdraw(amount);
        AccountRepository.Update(account);
    }

    private void CheckWithdrawal(decimal newBalance, Email accountEmail)
    {
        switch (newBalance)
        {
            case < 0: throw new InvalidOperationException("Insufficient funds to make withdrawal");
            case < Account.LowFundsThreshold: 
                NotificationService.NotifyFundsLow(accountEmail);
                break;
        }
    }
}