using AspireApp1.Web.Components.Interface;
using BlazorApp3.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorApp3
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IStorageService, LocalStorage>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<LoginCheck>();

            await builder.Build().RunAsync();
        }
    }
}
