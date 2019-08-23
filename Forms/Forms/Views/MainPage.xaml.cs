using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Forms.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            MasterBehavior = MasterBehavior.Popover;
        }
    }
}