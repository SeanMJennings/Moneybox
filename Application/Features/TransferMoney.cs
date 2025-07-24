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

        var fromBalance = from.Balance - amount;
            
        CheckBalance(fromBalance, from.User.Email);
        CheckPayIn(amount, to);
        Transfer(amount, from, to);
        UpdateAccounts(from, to);
    }

    private void CheckBalance(decimal fromBalance, Email fromEmail)
    {
        if (fromBalance.IsOverdrawn()) throw new InvalidOperationException("Insufficient funds to make transfer");
        if (fromBalance.HasLowFunds()) NotificationService.NotifyFundsLow(fromEmail);
    }
        
    private void CheckPayIn(decimal amount, Account to)
    {
        var paidIn = to.PaidIn + amount;
        if (to.WillExceedPayInLimit(paidIn)) throw new InvalidOperationException("Account pay in limit reached");
        if (to.WillApproachPayInLimit(paidIn)) NotificationService.NotifyApproachingPayInLimit(to.User.Email);
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