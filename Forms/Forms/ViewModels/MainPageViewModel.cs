using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using Core;
using Core.Dto;
using Forms.Helpers;
using Forms.Models;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace Forms.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ICommand DisconnectCommand { get; }

        public ICommand ItemTappedCommand { get; }

        public ICommand SettingsCommand { get; }

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _currentProfil = ApplicationContext.Instance.CurrentUser;

            SetMenuItems();
            SetProfilePicture();

            DisconnectCommand = new Command(OnDisconnectCommand);
            ItemTappedCommand = new DelegateCommand<HomeMenuItem>(OnItemTappedCommandExecuted);
        }

        private async void OnItemTappedCommandExecuted(HomeMenuItem obj)
        {
            if (string.IsNullOrWhiteSpace(obj.PageUri) == false)
            {
                await NavigationService.NavigateAsync(PageNameConstants.NAV + "/" + obj.PageUri);
            }
        }

        private void SetProfilePicture()
        {
            if (_currentProfil != null)
            {
                if (string.IsNullOrEmpty(_currentProfil.PictureByte))
                {
                    ProfilPicture = ImageSource.FromResource("MZP.Forms.Resources.img.portrait_placeholder.png", typeof(MainPageViewModel).Assembly);
                }
                else
                {
                    ProfilPicture = ImageSource.FromStream(() =>
                    {
                        byte[] byteArray = Convert.FromBase64String(_currentProfil.PictureByte);
                        return new MemoryStream(byteArray);
                    });
                }
            }
        }

        private void SetMenuItems()
        {
            List<HomeMenuItemList> itemSource = new List<HomeMenuItemList>();

            HomeMenuItemList list = new HomeMenuItemList("Général")
            {
                new HomeMenuItem { Id = PageType.Home, Title = "Accueil", PageUri = PageNameConstants.HOME },
            };
            itemSource.Add(list);

            string code = ApplicationContext.Instance?.CurrentUser?.Role?.Code;

            if (code == RoleEnum.Admin.ToString())
            {
                list = new HomeMenuItemList("Administration")
                {
                    new HomeMenuItem { Id = PageType.Administration, Title = "Add Item", PageUri = PageNameConstants.NEW_ITEM },
                };
                itemSource.Add(list);
            }

            list.Add(new HomeMenuItem { Id = PageType.About, Title = "A Propos", PageUri = PageNameConstants.ABOUT });

            _listMenuItem = itemSource;
        }

        private async void OnDisconnectCommand(object obj)
        {
            ApplicationContext.Instance.CurrentUser = null;
            await NavigationService.NavigateAsync(PageNameConstants.AUTHENTIFICATION_PAGE);
        }

        private ImageSource _profilPicture;

        public ImageSource ProfilPicture
        {
            get { return _profilPicture; }
            set { SetProperty(ref _profilPicture, value); }
        }

        private List<HomeMenuItemList> _listMenuItem;

        public List<HomeMenuItemList> ListMenuItem
        {
            get { return _listMenuItem; }
            set { SetProperty(ref _listMenuItem, value); }
        }

        private UserDto _currentProfil;

        public UserDto CurrentProfil
        {
            get { return _currentProfil; }
            set { SetProperty(ref _currentProfil, value); }
        }
    }
}
