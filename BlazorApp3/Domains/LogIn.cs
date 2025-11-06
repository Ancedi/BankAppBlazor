using BlazorApp3.Interfaces;
using System.Text.Json.Serialization;

namespace BlazorApp3.Domains
{
    public class Login : ILogin
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public List<BankAccount> BankAccount { get; set; } = new();

        public Login (string username, string password)
        {
            Username = username;
            Password = password;
        }

        [JsonConstructor]
        public Login (string username, string password, List<BankAccount> bankaccount)
        {
            Username = username;
            Password = password;
            this.BankAccount = bankaccount;
        }
    }
}
