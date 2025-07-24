using System;
using Moneybox.Application.DataAccess;
using Moneybox.Domain.Services;

namespace Moneybox.Application.Features
{
    public class WithdrawMoney(IAmAnAccountRepository amAnAccountRepository, IAmANotificationService amANotificationService)
    {
        private IAmAnAccountRepository _amAnAccountRepository = amAnAccountRepository;
        private IAmANotificationService _amANotificationService = amANotificationService;

        public void Execute(Guid fromAccountId, decimal amount)
        {
            // TODO:
        }
    }
}
