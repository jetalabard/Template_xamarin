using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Forms.Models;
using Forms.Views;
using Prism.Navigation;
using System.Windows.Input;
using Core;
using System.Linq;
using Prism.Commands;

namespace Forms.ViewModels
{
    public class ItemsViewModel : ViewModelBase
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        internal ICommand AddItemCommand { get; }

        internal ICommand ItemTappedCommand { get; }

        

        public ItemsViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
            AddItemCommand = new Command(AddItem_Clicked);
            ItemTappedCommand = new DelegateCommand<Item>(OnItemTappedCommandExecuted);
        }

        async void OnItemTappedCommandExecuted(Item item)
        {
            NavigationParameters navParameters = new NavigationParameters
            {
                { ItemDetailViewModel.ITEM , item },
            };
            await NavigationService.NavigateAsync(PageNameConstants.DETAIL_ITEM,navParameters);
        }


        internal async void AddItem_Clicked()
        {
            await NavigationService.NavigateAsync(PageNameConstants.NEW_ITEM);
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}