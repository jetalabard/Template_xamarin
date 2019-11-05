using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Forms.Views
{
    /// <summary>
    /// Page to show section "about".
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }
    }
}