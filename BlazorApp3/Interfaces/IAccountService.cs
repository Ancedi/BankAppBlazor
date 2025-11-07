using BankAppBlazor.Domains;
namespace BankAppBlazor.Interfaces;

//Interface
public interface IAccountService
{
    Task<BankAccount> CreateAccount(string name, Currency currency, AccountType accountType, decimal balance);
    Task<BankAccount> EraseAccount(BankAccount account);

    List<BankAccount> GetAccount();
    Task EnsureLoaded();
    Task Transfer(Guid Sender, Guid Receiver, decimal amount);
    event Action? ApplyRate;
    void AppliedRate(BankAccount bankAccount);
}