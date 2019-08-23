using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthentificationPage : ContentPage
    {
        public AuthentificationPage()
        {
            InitializeComponent();
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
        }
    }
}