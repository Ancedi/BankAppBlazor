using BlazorApp3.Interfaces;
using System.Text.Json.Serialization;

namespace BlazorApp3.Domains
{
    public class LogIn : ILogIn
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public List<BankAccount> BankAccount;

        public LogIn (string username, string password)
        {
            Username = username;
            Password = password;
        }

        [JsonConstructor]
        public LogIn (string username, string password, List<BankAccount> bankaccount)
        {
            Username = username;
            Password = password;
            this.BankAccount = bankaccount;
        }
    }
}
