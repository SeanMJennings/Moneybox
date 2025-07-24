using System;
using Moneybox.Application.DataAccess;
using Moneybox.Domain.Behaviours;
using Moneybox.Domain.Entities;
using Moneybox.Domain.Services;

namespace Moneybox.Application.Features
{
    public class TransferMoney(IAmAnAccountRepository AccountRepository, IAmANotificationService NotificationService)
    {
        public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var from = AccountRepository.GetAccountById(fromAccountId);
            var to = AccountRepository.GetAccountById(toAccountId);

            var fromBalance = from.Balance - amount;
            
            CheckBalance(fromBalance, from);
            CheckPayIn(amount, to);
            Transfer(amount, from, to);

            AccountRepository.Update(from);
            AccountRepository.Update(to);
        }
        
        private void CheckBalance(decimal fromBalance, Account from)
        {
            if (fromBalance.IsOverdrawn()) throw new InvalidOperationException("Insufficient funds to make transfer");
            if (fromBalance.HasLowFunds()) NotificationService.NotifyFundsLow(from.User.Email);
        }
        
        private void CheckPayIn(decimal amount, Account to)
        {
            var paidIn = to.PaidIn + amount;
            if (paidIn > Account.PayInLimit) throw new InvalidOperationException("Account pay in limit reached");
            if (Account.PayInLimit - paidIn < Account.LowFundsThreshold) NotificationService.NotifyApproachingPayInLimit(to.User.Email);
        }
        
        private static void Transfer(decimal amount, Account from, Account to)
        {
            from.Withdraw(amount);
            to.Deposit(amount);
        }

    }
}
