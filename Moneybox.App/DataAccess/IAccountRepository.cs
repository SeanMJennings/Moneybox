using System;
using Moneybox.App.Domain;
using Moneybox.Domain.Entities;

namespace Moneybox.App.DataAccess
{
    public interface IAccountRepository
    {
        Account GetAccountById(Guid accountId);

        void Update(Account account);
    }
}
