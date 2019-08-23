using System;

using Forms.Models;
using Prism.Navigation;

namespace Forms.ViewModels
{
    public class ItemDetailViewModel : ViewModelBase
    {
        public static string ITEM = "ITEM";



        public Item Item { get; set; }

        public ItemDetailViewModel(INavigationService navigationService) : base(navigationService)
        {
        }


        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);


            if (parameters.ContainsKey(ITEM))
            {
                Item = parameters[ITEM] as Item;
                Title = Item.Text;
            }

        }

    }
}
