using BlazorApp3.Domains;

namespace BankApp.Interfaces;
    /// <summary>
    /// Contains Bank App Account methods.
    /// </summary>

//interface
public interface IBankAccount
{
    Guid id {get;}
    string Name { get; }
    string Password { get; }
    AccountType AccountType { get; }
    Currency Currency { get; }
    decimal Balance { get; }
    DateTime LastUpdated { get; }
    List<Transaction> Transaction { get; }
    void Withdraw(decimal amount);
    void Deposit(decimal amount);

}