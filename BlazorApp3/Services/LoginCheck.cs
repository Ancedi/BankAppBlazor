namespace BlazorApp3.Services
{
    public class LoginCheck //Class to determine user is logged in across several blazor pages.
    {
        public bool isLoggedIn = false;
        public void LogIn() => isLoggedIn = true;
        public void LogOut() => isLoggedIn = false;
    }

}
