using System;
using System.Collections.Generic;
using Vocabulary.ViewModels;
using Xamarin.Forms;

namespace Vocabulary.View
{
    public partial class AddWordsView : ContentPage
    {
        public AddWordsView()
        {
            InitializeComponent();

            BindingContext = new AddWordsViewModel();
        }
    }
}
