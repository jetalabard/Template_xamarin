using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using Forms.Models;
using Forms.Services;
using Prism.Navigation;
using Prism.Mvvm;
using Prism.Commands;

namespace Forms.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, INotifyPropertyChanged
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>() ?? new MockDataStore();
        protected INavigationService NavigationService { get; private set; }
        internal string ParentPageName { get; set; }
        public static string PARENT_PARAMETER = "PARENT_PARAMETER";
        public DelegateCommand<string> OnNavigateCommand { get; set; }

        private async void NavigateAsync(string page)
        {
            await NavigationService.NavigateAsync(new Uri(page, UriKind.Relative));
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value, () => RaisePropertyChanged(nameof(IsNotBusy))); }
        }

        public bool IsNotBusy
        {
            get { return !IsBusy; }
        }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
            OnNavigateCommand = new DelegateCommand<string>(NavigateAsync);
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
