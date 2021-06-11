using Vocabulary.Model;
using Vocabulary.View;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Vocabulary.ViewModels
{
    public class VocabularyViewModel : BaseViewModel
    {
        private Words _selectedItem;

        public ObservableCollection<Words> ListWords { get;}

        public string LastWordAdd { get; private set; }

        public Command LoadItemsCommand { get; }

        public Command MenuSort { get; }

        public Command AddWords { get; }

        public Command<Words> ItemTapped { get; }

        public int CountWords { get; private set; }


        public VocabularyViewModel()
        {
            ListWords = DataStore.Items;

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommandAsync());

            ItemTapped = new Command<Words>(OnItemSelected);

            MenuSort = new Command(OnItemSort);

            AddWords = new Command(OnAddWords);

            CountWords = DataStore.Items.Count;

            LastWordAdd = DataStore.Items[0].DateTime;


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
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    ListWords.Add(item);
                }
                LastWordAdd = DataStore.Items[0].DateTime;

                CountWords = DataStore.Items.Count;

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

        public Words SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
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
                DataStore.CurrentWordsRepository.DeleteDataTable("Words", item.Id.ToString());
                await ExecuteLoadItemsCommandAsync();
            }
        }
    }
}
