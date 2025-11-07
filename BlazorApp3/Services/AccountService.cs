using BankAppBlazor.Interfaces;
using BankAppBlazor.Domains;
using System.Threading.Tasks;
using System.Xml;
using System.ComponentModel;
namespace BankAppBlazor.Services;
public class AccountService : IAccountService
{
    private bool isLoaded = false; //bool automatically set to false, expected to be true after data retrieval.
    private List<BankAccount> _accounts = new();
    private readonly IStorageService _localStorage;

    public List<BankAccount> GetAccount() => _accounts; //backend list of created bank accounts.

    public AccountService(IStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    private Task SaveAccount()
    {
        if (_localStorage == null)
            throw new InvalidOperationException("Service Uninitiazed");
        return _localStorage.AddItem("accounts", _accounts.OfType<BankAccount>().ToList());
    }

    private Task DeleteAccount()
    {
        if (_localStorage == null)
            throw new InvalidOperationException("Service Uninitialized");
        return _localStorage.RemoveItem("accounts", _accounts.OfType<BankAccount>().ToList());
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

    public event Action? ApplyRate;

    private void Notify() => ApplyRate?.Invoke();

    public void AppliedRate(BankAccount bankAccount)
    {
        var lastUpdatedThreshold = bankAccount.LastUpdated.AddDays(1);
        if (lastUpdatedThreshold <= DateTime.Now)
        {
            Notify();
            bankAccount.ApplyInterestRate();
        }  
    }
}