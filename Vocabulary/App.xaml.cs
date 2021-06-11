using Vocabulary.Services;
using Xamarin.Forms;

namespace Vocabulary
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<WordsRepository>();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
