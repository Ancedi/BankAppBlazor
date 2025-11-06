namespace BankApp.Interfaces;

//Interface
public interface IAccountService
{
    Task<Login> RegisterLogin(string username, string password);
    Task<BankAccount> CreateAccount(string name, Currency currency, AccountType accountType, decimal balance);
    Task<Login> RemoveLogin(Login login);
    Task<BankAccount> RemoveAccount(BankAccount account);
    List<BankAccount> GetAccount();
    List<Login> GetLogin();
    Task EnsureLoaded();
    Task LoadLogins();
    Task Transfer(Guid Sender, Guid Receiver, decimal amount);

}