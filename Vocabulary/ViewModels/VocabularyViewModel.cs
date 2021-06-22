using Vocabulary.Model;
using Vocabulary.Views;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Vocabulary.ViewModels
{
    public class VocabularyViewModel : BaseViewModel
    { 
        private string textSearch;

        private Words _selectedItem;

        public Command SearchData { get; }

        public Command SearchBtnPress { get; }

        public ObservableCollection<Words> ListWords { get; set; }

        public string LastWordAdd { get; private set; }

        public Command LoadItemsCommand { get; }

        public Command MenuSort { get; }

        public Command AddWords { get; }

        public Command<Words> ItemTapped { get; }

        public int CountWords { get; private set; }

        public string TextSearch
        {
            get { return textSearch; }
            set { textSearch = value; OnPropertyChanged(); }
        }
        bool _isVisibleStatus;
        public bool IsVisibleStatus
        {
            get { return _isVisibleStatus; }
            set
            {
                _isVisibleStatus = value;
                OnPropertyChanged();
            }
        }
        public Words SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        public VocabularyViewModel()
        {
            ListWords = new ObservableCollection<Words>();

            _ = ExecuteLoadItemsCommandAsync();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommandAsync());

            ItemTapped = new Command<Words>(OnItemSelected);

            MenuSort = new Command(OnItemSort);

            AddWords = new Command(OnAddWords);

            SearchData = new Command(async () => await SearchCommandAsync());

            SearchBtnPress = new Command(async () => await SearchCommandAsync());

            CountWords = ListWords.Count;

            LastWordAdd = ListWords[0].DateTime;


        }
        public async Task SearchCommandAsync()
        {
            if (!string.IsNullOrEmpty(TextSearch))
            {
                var searchData = ListWords.Where(x => x.EnglishWords.ToLower().Contains(TextSearch.ToLower())).ToList();
                
                if (ListWords.Count() == 0)
                {
                    IsVisibleStatus = true;
                }
                else
                {
                    IsVisibleStatus = false;
                }
                ListWords.Clear();
                foreach (var item in searchData)
                {
                    ListWords.Add(item);
                }
            }
            else
            {
                await ExecuteLoadItemsCommandAsync();
                IsVisibleStatus = false;
            }
        }
        async void OnAddWords()
        {
            await PopupNavigation.Instance.PushAsync(new PopupAddWordsView());
        }
        async void OnItemSort()
        {
            await PopupNavigation.Instance.PushAsync(new PopupMenuItemView());
        }
        public async Task ExecuteLoadItemsCommandAsync()
        {
            IsBusy = true;

            try
            {
                ListWords.Clear();
                var items = await DataStore.ReadDataBase(true);
                items = items.Reverse();
                foreach (var item in items)
                {
                    ListWords.Add(item);
                }
                LastWordAdd = ListWords[0].DateTime;

                CountWords = ListWords.Count;

                OnPropertyChanged("LastWordAdd");

                OnPropertyChanged("CountWords");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        async void OnItemSelected(Words item)
        {
            if (item == null)
            {
                return;
            }

            bool result = await Application.Current.MainPage.DisplayAlert($"'{item.EnglishWords}'", $"Do you want to delete an element?", "Yes", "No");

            if (result)
            {
                await DataStore.DeleteDataTable("Words", item.Id.ToString());
                await ExecuteLoadItemsCommandAsync();
            }
        }
    }
}
