using System;
using Moneybox.Application.DataAccess;
using Moneybox.Domain.Entities;
using Moneybox.Domain.Services;

namespace Moneybox.Application.Features
{
    public class TransferMoney(IAmAnAccountRepository amAnAccountRepository, IAmANotificationService amANotificationService)
    {
        public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var from = amAnAccountRepository.GetAccountById(fromAccountId);
            var to = amAnAccountRepository.GetAccountById(toAccountId);

            var fromBalance = from.Balance - amount;
            if (fromBalance < 0m)
            {
                throw new InvalidOperationException("Insufficient funds to make transfer");
            }

            if (fromBalance < 500m)
            {
                amANotificationService.NotifyFundsLow(from.User.Email);
            }

            var paidIn = to.PaidIn + amount;
            if (paidIn > Account.PayInLimit)
            {
                throw new InvalidOperationException("Account pay in limit reached");
            }

            if (Account.PayInLimit - paidIn < 500m)
            {
                amANotificationService.NotifyApproachingPayInLimit(to.User.Email);
            }
            //
            // from.Balance = from.Balance - amount;
            // from.Withdrawn = from.Withdrawn - amount;
            //
            // to.Balance = to.Balance + amount;
            // to.PaidIn = to.PaidIn + amount;

            amAnAccountRepository.Update(from);
            amAnAccountRepository.Update(to);
        }
    }
}
