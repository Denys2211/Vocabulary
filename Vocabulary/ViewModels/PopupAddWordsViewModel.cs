using System;
using Rg.Plugins.Popup.Services;
using Vocabulary.View;
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

            CurrentWordsRepository.AddInDataBase("Words", English, Ukrainian);
            await PopupNavigation.Instance.PopAsync(true);

        }
    }
}
