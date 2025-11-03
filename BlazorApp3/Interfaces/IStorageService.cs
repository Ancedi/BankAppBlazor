using BlazorApp3.Interfaces;
namespace BlazorApp3.Interface
{
 //interface
    public interface IStorageService
    {
        //Save
        Task AddItem<T>(string key, T value);

        //Retrieve
        Task<T?> GetItem<T>(string key);

        //Delete
        Task RemoveItem<T>(string key, T value);
    }
}
