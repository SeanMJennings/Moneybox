using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class WithdrawMoney(IAccountRepository accountRepository, INotificationService notificationService)
    {
        private IAccountRepository accountRepository = accountRepository;
        private INotificationService notificationService = notificationService;

        public void Execute(Guid fromAccountId, decimal amount)
        {
            // TODO:
        }
    }
}
