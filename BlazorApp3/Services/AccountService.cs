using BlazorApp3.Interfaces;
using BlazorApp3.Domains;
using System.Threading.Tasks;
using System.Xml;
namespace BankApp.Services;
public class AccountService : IAccountService
{
    private bool isLoaded = false;
    private List<Login> _logins = new();
    private List<BankAccount> _accounts = new();
    private readonly IStorageService _localStorage;

    public List<Login> GetLogin() => _logins;
    public List<BankAccount> GetAccount() => _accounts; //backend list of created bank accounts.

    public AccountService(IStorageService localStorage)
    {
        _localStorage = localStorage;
    }


    /// <summary>
    /// Store account item in localstorage to "accounts", provides exception.
    /// </summary>
    /// <returns>_localStorage.AddItem()</returns>
    /// <exception cref="InvalidOperationException"></exception>
    private Task SaveAccount()
    {
        if (_localStorage == null)
            throw new InvalidOperationException("Service Uninitiazed");
        return _localStorage.AddItem("accounts", _accounts.OfType<BankAccount>().ToList());
    }

    /// <summary>
    /// Store login item in localstorage to "logins"
    /// </summary>
    /// <returns>_localstorage.AddItem()</returns>
    /// <exception cref="InvalidOperationException"></exception>
    private Task SaveLogin()
    {
        if (_localStorage == null)
            throw new InvalidOperationException("Service Uninitialized");
        return _localStorage.AddItem("logins", _logins.OfType<Login>().ToList());
    }

    /// <summary>
    /// Removes account items from localstorage.
    /// </summary>
    /// <returns>_localStorage.AddItem()</returns>
    /// <exception cref="InvalidOperationException"></exception>
    private Task DeleteAccount()
    {
        if (_localStorage == null || !_accounts.Any())
        {
            throw new InvalidOperationException("No Accounts Available For Deletion.");
        }
        return _localStorage.RemoveItem("accounts", _accounts.OfType<BankAccount>().ToList());
    }

    private Task DeleteLogin()
    {
        if (_localStorage == null || _accounts.Any())
        {
            throw new InvalidOperationException("No Logins Available For Deletion");
        }
        return _localStorage.RemoveItem("logins", _logins.OfType<Login>().ToList());
    }

    public async Task<Login> RegisterLogin(string username, string password)
    {
        var login = new Login(username, password);
        _logins.Add(login);
        await SaveLogin();
        return login;
    }

    /// <summary>
    /// Method to create an account and locally store it for later retrieval.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="currency"></param>
    /// <param name="accountType"></param>
    /// <param name="balance"></param>
    /// <returns>account</returns>
    public async Task<BankAccount> CreateAccount(string name, Currency currency, AccountType accountType, decimal balance)
    {
        var account = new BankAccount(name, accountType, currency, balance);
        _accounts.Add(account);
        await SaveAccount();
        return account;
    }

    /// <summary>
    /// method to remove an account from localstorage.
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    public async Task<BankAccount> RemoveAccount(BankAccount account)
    {
        account = _accounts.FirstOrDefault();
        _accounts.Remove(account);
        await DeleteAccount();
        return account;
    }

    public async Task<Login> RemoveLogin(Login login)
    {
        login = _logins.FirstOrDefault();
        _logins.Remove(login);
        await DeleteLogin();
        return login;
    }

    /// <summary>
    /// Method which ensures list of accounts is loaded up if any has been saved.
    /// </summary>
    public async Task EnsureLoaded()
    {
        if (isLoaded == false)
        {
            _accounts = await _localStorage.GetItem<List<BankAccount>> ("accounts") ?? new List<BankAccount>();
            isLoaded = true;
        }
    }
    public async Task LoadLogins()
    {
        if (isLoaded == false)
        {
            _logins = await _localStorage.GetItem<List<Login>> ("logins") ?? new List<Login>();
            isLoaded = true;
        }
    }

    //Method to transfer wealth between accounts.
    public async Task Transfer(Guid Sender, Guid Receiver, decimal amount)
    {
        //checks and match accounts with bankaccount list until the first match.
        var fromAccount = _accounts.OfType<BankAccount>().FirstOrDefault(x => x.id == Sender)
            ?? throw new KeyNotFoundException($"Account with ID {Sender} not found.");
        var toAccount = _accounts.OfType<BankAccount>().FirstOrDefault(x => x.id == Receiver)
            ?? throw new KeyNotFoundException($"Account with ID {Receiver} not found");
        fromAccount.TransferTo(toAccount, amount);
        await SaveAccount();
    }
}