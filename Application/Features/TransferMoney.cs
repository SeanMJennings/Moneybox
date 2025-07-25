using System;
using Moneybox.Application.DataAccess;
using Moneybox.Domain.Entities;
using Moneybox.Domain.Primitives;
using Moneybox.Domain.Services;

namespace Moneybox.Application.Features;

public class TransferMoney(IAmAnAccountRepository AccountRepository, IAmANotificationService NotificationService)
{
    public void Execute(Guid fromAccountId, Guid toAccountId, Money amount)
    {
        var from = AccountRepository.GetAccountById(fromAccountId);
        var to = AccountRepository.GetAccountById(toAccountId);
        
        Transfer(amount, from, to);
        UpdateAccounts(from, to);
        
        NotifyTransferringAccount(from);
        NotifyReceivingAccount(to);
    }
        
    private static void Transfer(Money amount, Account from, Account to)
    {
        from.Withdraw(amount);
        to.Deposit(amount);
    }
    
    private void UpdateAccounts(Account from, Account to)
    {
        AccountRepository.Update(from);
        AccountRepository.Update(to);
    }
    
    private void NotifyTransferringAccount(Account from)
    {
        if (from.Balance.HasLowFunds()) NotificationService.NotifyFundsLow(from.User.Email);
    }
        
    private void NotifyReceivingAccount(Account to)
    {
        if (to.Balance.ApproachingPayInLimit()) NotificationService.NotifyApproachingPayInLimit(to.User.Email);
    }
}