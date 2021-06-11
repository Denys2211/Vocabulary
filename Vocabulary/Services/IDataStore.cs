using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Vocabulary.Model;

namespace Vocabulary.Services
{
    public interface IDataStore<T>
    {
        ObservableCollection<Words> Items { get; set; }
        WordsRepository CurrentWordsRepository { get; }
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(int id);
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
