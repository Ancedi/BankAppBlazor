namespace BlazorApp3.Services
{
    public class LoginCheck
    {
        public bool isLoggedIn = false;
        public void LogIn() => isLoggedIn = true;
        public void LogOut() => isLoggedIn = false;
    }

}
