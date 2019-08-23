using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Core;
using Forms.Models;
using Forms.Services;
using Forms.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace Forms.ViewModels
{
    public class ItemsViewModel : ViewModelBase
    {
        public ObservableCollection<Item> Items { get; set; }

        private readonly IDataStore<Item> _store;

        public Command LoadItemsCommand { get; set; }

        internal ICommand AddItemCommand { get; }

        internal ICommand ItemTappedCommand { get; }

        public ItemsViewModel(INavigationService navigationService, IDataStore<Item> store)
            : base(navigationService)
        {
            Title = "Browse";
            _store = store;
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

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Items = new ObservableCollection<Item>(await _store.GetItemsAsync());
        }

        private async void OnItemTappedCommandExecuted(Item item)
        {
            NavigationParameters navParameters = new NavigationParameters
            {
                { ItemDetailViewModel.ITEM, item },
            };
            await NavigationService.NavigateAsync(PageNameConstants.DETAIL_ITEM, navParameters);
        }

        internal async void AddItem_Clicked()
        {
            await NavigationService.NavigateAsync(PageNameConstants.NEW_ITEM);
        }

        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
            {
                return;
            }

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