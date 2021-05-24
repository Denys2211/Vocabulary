using System;
using System.Collections.Generic;
using Vocabulary.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vocabulary
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(VocabularyView), typeof(VocabularyView));
        }
        
    }
}
