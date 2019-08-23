using Core;
using Forms.Helpers;
using Forms.Resources.localization;
using Forms.ViewModels;
using Forms.Views;
using Prism;
using Prism.Ioc;
using Prism.Plugin.Popups;
using Xamarin.Forms;

namespace Forms
{
    /// <summary>
    /// Run application.
    /// </summary>
    public partial class App
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
            : this(null)
        {
        }

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            ResourceLoader.Initialize(loc.ResourceManager);

            await NavigationService.NavigateAsync(PageNameConstants.AUTHENTIFICATION_PAGE);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterNavigation(containerRegistry);
            RegisterDependencyInjection(containerRegistry);

            containerRegistry.RegisterPopupNavigationService();
        }

        protected void RegisterNavigation(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>(PageNameConstants.MAIN);
            containerRegistry.RegisterForNavigation<NavigationPage>(PageNameConstants.NAV);
            containerRegistry.RegisterForNavigation<Page>();
            containerRegistry.RegisterForNavigation<ContentPage>("Content");
            containerRegistry.RegisterForNavigation<MasterDetailPage>("MasterDetail");

            containerRegistry.RegisterForNavigation<ItemsPage, ItemsViewModel>(PageNameConstants.HOME);

            containerRegistry.RegisterForNavigation<AuthentificationPage, AuthentificationViewModel>(PageNameConstants.AUTHENTIFICATION);
            containerRegistry.RegisterForNavigation<AboutPage, AboutViewModel>(PageNameConstants.ABOUT);
        }

        protected void RegisterDependencyInjection(IContainerRegistry containerRegistry)
        {
            // containerRegistry.Register<IAuthenticationManager, AuthenticationManager>();
            // containerRegistry.Register<IStoreCredentialService, StoreCredentialService>();
            // containerRegistry.Register<IHashService, HashService>();
            // containerRegistry.Register<IAuthenticationService, DbAuthenticationService>();

            // Rest Authentification
            // var container = containerRegistry.GetContainer();
            // container.UseInstance(<UserRepository>(new UserRepository(false));
            // var autoInjectProps = Made.Of(propertiesAndFields: PropertiesAndFields.Name();
            // container.Register<UserRepository>(Reuse.InCurrentScope, autoInjectProps);
            // container.Register<UserRepository>();
            // container.RegisterInstance(false);
            // container.Register<ContractRepository>();

            // container.Register<ContractRepository>(Reuse.InCurrentScope, autoInjectProps);
            // container.RegisterInstance(false);

            // 3) Register string with key and Foo with strongly typed constructor specification
            // container.Register<UserRepository>(Made.Of(() => new UserRepository(Arg.Of<bool>(false))));
            // container.RegisterInstance("my string", serviceKey: "someSetting");
        }
    }
}
