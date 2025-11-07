
using BankAppBlazor.Services;
using Microsoft.JSInterop.Infrastructure;
using System.Text.Json.Serialization;
using System.Transactions;
using BankAppBlazor.Domains;

namespace BankAppBlazor.Domains;
public class BankAccount : IBankAccount
{
    public Guid id { get; private set; } = Guid.NewGuid();

    public string Name { get; private set; }
    public AccountType AccountType { get; }

    public Currency Currency { get; }
    public decimal Rate { get; } = 0.01m;
    public decimal Balance { get; private set; }
    public DateTime LastUpdated { get; private set; }
    public List<Transaction> Transaction { get; set; } = new();

    public BankAccount(string name, AccountType accountType, Currency currency, decimal balance)
    {
        Name = name;
        AccountType = accountType;
        Balance = balance;
        Currency = currency;
        LastUpdated = DateTime.Now;
    }

    [JsonConstructor]
    public BankAccount(Guid id, string name, AccountType accountType, Currency currency, decimal balance, DateTime lastUpdated, List<Transaction> transaction)
    {
        this.id = id;
        Name = name;
        AccountType = accountType;
        Balance = balance;
        Currency = currency;
        LastUpdated = lastUpdated;
        this.Transaction = transaction;
    }

    //Specifies the amount to put in.
    public void Deposit(decimal amount)
    {
        throw new NotImplementedException();
    }

    //Specifies the amount to take out.
    public void Withdraw(decimal amount)
    {
        throw new NotImplementedException();
    }

    //applies interest rate to current balance when called.
    public void ApplyInterestRate()
    {
        Balance *= (1+Rate);
    }

    //Specifies giver and recipient of a user determined amount.
    public void TransferTo(BankAccount toAccount, decimal amount)
    {
        
        //prevents transfers if the set amount is over available user currency.
        if (Balance >= amount)
        {
            
            //updates giver-user's wealth post-transaction and logs it.
            Balance -= amount;
            LastUpdated = DateTime.UtcNow;
            Transaction.Add(new Transaction
            {
                TransactionType = TransactionType.TransferOut,
                Amount = amount,
                BalanceAfter = Balance,
                Sender = id, //FromAccountID = Id
                Receiver = toAccount.id, //ToAccountID = toAccount.Id
            });
            
            //updates recipient-user's wealth post-transaction and logs it.
            toAccount.Balance += amount;
            toAccount.LastUpdated = DateTime.UtcNow;
            toAccount.Transaction.Add(new Transaction
            {
                TransactionType = TransactionType.TransferIn,
                Amount = amount,
                BalanceAfter = Balance,
            });
        }
        else
        {
            return;
        }
}
}