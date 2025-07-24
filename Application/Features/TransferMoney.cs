using System;
using Moneybox.Application.DataAccess;
using Moneybox.Domain.Behaviours;
using Moneybox.Domain.Entities;
using Moneybox.Domain.Primitives;
using Moneybox.Domain.Services;

namespace Moneybox.Application.Features;

public class TransferMoney(IAmAnAccountRepository AccountRepository, IAmANotificationService NotificationService)
{
    public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
    {
        var from = AccountRepository.GetAccountById(fromAccountId);
        var to = AccountRepository.GetAccountById(toAccountId);
        
        Transfer(amount, from, to);
        UpdateAccounts(from, to);
        
        NotifyOnNewBalance(from.Balance, from.User.Email);
        NotifyOnPayInLimits(to);
    }

    private void NotifyOnNewBalance(decimal fromBalance, Email fromEmail)
    {
        if (fromBalance.HasLowFunds()) NotificationService.NotifyFundsLow(fromEmail);
    }
        
    private void NotifyOnPayInLimits(Account to)
    {
        if (to.ApproachingPayInLimit()) NotificationService.NotifyApproachingPayInLimit(to.User.Email);
    }
        
    private static void Transfer(decimal amount, Account from, Account to)
    {
        from.Withdraw(amount);
        to.Deposit(amount);
    }
    
    private void UpdateAccounts(Account from, Account to)
    {
        AccountRepository.Update(from);
        AccountRepository.Update(to);
    }
}