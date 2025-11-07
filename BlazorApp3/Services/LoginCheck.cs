using BankAppBlazor.Services;

namespace BlazorApp3.Services
{
    public class LoginCheck
    {
        private readonly IStorageService _localStorage;
        public LoginCheck(IStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public bool isLoggedIn = false;

        /// <summary>
        /// Determines isLoggedIn is true and stores it in localStorage.
        /// </summary>
        public void LogIn()
        {
            isLoggedIn = true;
            SaveLogState();
        }

        /// <summary>
        /// Determines isLoggedIn is false and stores it in localStorage.
        /// </summary>
        public void LogOut()
        {
            isLoggedIn = false;
            SaveLogState();
        }

        private Task SaveLogState()
        {
            if (_localStorage == null)
                throw new InvalidOperationException("Service Uninitialized");
            return _localStorage.AddItem("logcheck", isLoggedIn);
        }

        public async Task LoadLoginState()
        {
            isLoggedIn = await _localStorage.GetItem<bool>("logcheck");
        }
    }
}
