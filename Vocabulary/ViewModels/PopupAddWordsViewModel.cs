using System;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Vocabulary.ViewModels
{
    public class PopupAddWordsViewModel : BaseViewModel
    {
        private string english;
        private string ukrainian;

        public PopupAddWordsViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(english)
                && !String.IsNullOrWhiteSpace(ukrainian);
        }

        public string English
        {
            get => english;
            set => SetProperty(ref english, value);
        }

        public string Ukrainian
        {
            get => ukrainian;
            set => SetProperty(ref ukrainian, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            await PopupNavigation.Instance.PopAsync(true);
        }

        private async void OnSave()
        {
            var items = await DataStore.ReadDataBase(true);
            foreach (var item in items)
            {
                if (item.EnglishWords == English)
                {
                    await Application.Current.MainPage.DisplayAlert("Notification", "Word is Exist", "Ok");
                    return;
                }
            }
            await DataStore.AddInDataBase("Words", English, Ukrainian);
            await PopupNavigation.Instance.PopAsync(true);

        }
    }
}
