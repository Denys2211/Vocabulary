using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Vocabulary.Model;

namespace Vocabulary.Services
{
    public class MockDataStore: IDataStore<Words>
    {
        public ObservableCollection<Words> Items { get; set; }

        public WordsRepository CurrentWordsRepository { get; }
        public object DataStore { get; private set; }

        public MockDataStore()
        {
            CurrentWordsRepository = new WordsRepository();
            Items = new ObservableCollection<Words>(CurrentWordsRepository.ReadDataBase().Reverse());

        }

        public async Task<bool> AddItemAsync(Words item)
        {
            Items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Words item)
        {
            var oldItem = Items.Where((Words arg) => arg.Id == item.Id).FirstOrDefault();
            Items.Remove(oldItem);
            Items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = Items.Where((Words arg) => arg.Id == id).FirstOrDefault();
            Items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Words> GetItemAsync(int id)
        {
            return await Task.FromResult(Items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Words>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(Items);
        }
    }
}
