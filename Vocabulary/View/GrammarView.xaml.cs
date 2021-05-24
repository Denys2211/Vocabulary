using System;
using System.Collections.Generic;
using Vocabulary.ViewModels;
using Xamarin.Forms;

namespace Vocabulary.View
{
    public partial class GrammarView : ContentPage
    {
        public GrammarView()
        {
            InitializeComponent();

            BindingContext = new GrammarViewModel();
        }
    }
}
