using System;
using Moneybox.Domain.Entities;

namespace Moneybox.Application.DataAccess
{
    public interface IAmAnAccountRepository
    {
        Account GetAccountById(Guid accountId);

        void Update(Account account);
    }
}
