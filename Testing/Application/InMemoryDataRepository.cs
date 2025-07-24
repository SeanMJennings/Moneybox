using Moneybox.Application.DataAccess;
using Moneybox.Domain.Entities;

namespace Testing.Application;

public class InMemoryDataRepository(List<Account> accounts) : IAmAnAccountRepository
{
    public Account GetAccountById(Guid accountId)
    {
        return accounts.FirstOrDefault(a => a.Id == accountId) ?? throw new InvalidOperationException("Account not found");
    }

    public void Update(Account account)
    {
        var existingAccount = accounts.FirstOrDefault(a => a.Id == account.Id);
        if (existingAccount == null) throw new InvalidOperationException("Account not found");

        accounts.Remove(existingAccount);
        accounts.Add(account);
    }
}