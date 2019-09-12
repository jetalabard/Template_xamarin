using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Forms.Views
{
    /// <summary>
    /// authentification page.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthentificationPage : ContentPage
    {
        public AuthentificationPage()
        {
            InitializeComponent();

            CompanyLogo.Source = ImageSource.FromResource("Forms.Resources.img.company_logo.png", typeof(AuthentificationPage).Assembly);
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            PasswordEntry.Focus();
        }
    }
}