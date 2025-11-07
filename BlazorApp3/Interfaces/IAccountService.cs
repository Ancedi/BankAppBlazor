namespace BankApp.Interfaces;

//Interface
public interface IAccountService
{
    Task<BankAccount> CreateAccount(string name, Currency currency, AccountType accountType, decimal balance);
    Task<BankAccount> EraseAccount(string name, Currency currency, AccountType accountType, decimal balance);
    List<BankAccount> GetAccount();
    Task EnsureLoaded();
    Task Transfer(Guid Sender, Guid Receiver, decimal amount);

}