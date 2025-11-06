namespace BlazorApp3.Interfaces
{
    public interface ILogin
    {
        string Username { get; }
        string Password { get; }
        List<BankAccount> BankAccount { get; }
    }
}
