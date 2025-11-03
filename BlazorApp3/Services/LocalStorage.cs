using BlazorApp3.Interfaces;
using Microsoft.JSInterop;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BankApp.Services
{
    public class LocalStorage : IStorageService
    {
        private readonly IJSRuntime _jsRuntime;
        private JsonSerializerOptions JsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter() }
        };
        public LocalStorage(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }
        //Waits for _jsRuntime variable before saving data locally.
        public async Task AddItem<T>(string key, T value)
        {
            try
            {
                var json = JsonSerializer.Serialize(value, JsonSerializerOptions);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, json);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
        //Waits for _jsRuntime variable before deleting data locally.
        public async Task RemoveItem<T>(string key, T value)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
        //waits for JsonSerialiser before retrieving data locally.
        public async Task<T?> GetItem<T>(string key)
        {
            try
            {
                var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
                if (string.IsNullOrEmpty(json))
                    return default;
                return JsonSerializer.Deserialize<T>(json, JsonSerializerOptions);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return default;
            }
            
        }
    }
}