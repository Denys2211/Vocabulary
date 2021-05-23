using System;
using Vocabulary.Model;
using Xamarin.Forms;

namespace Vocabulary.ViewModels
{
    public class AddWordsViewModel : BaseViewModel
    {
        private string english;
        private string ukrainian;

        public AddWordsViewModel()
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
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {

            CurrentWordsRepository.AddInDataBase("Words", English, Ukrainian);
            English = null;
            Ukrainian = null;
            OnPropertyChanged("English");
            OnPropertyChanged("Ukrainian");

            await Shell.Current.GoToAsync("..");
        }
    }
}
