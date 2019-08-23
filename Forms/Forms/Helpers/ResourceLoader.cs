using System.ComponentModel;
using System.Globalization;
using System.Resources;

namespace Forms.Helpers
{
    public class ResourceLoader : INotifyPropertyChanged
    {
        private readonly ResourceManager _manager;
        private CultureInfo _cultureInfo;

        private ResourceLoader(ResourceManager resourceManager)
        {
            _manager = resourceManager;
            _cultureInfo = CultureInfo.CurrentUICulture;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static ResourceLoader Instance { get; private set; }

        public string this[string key] => GetString(key);

        public static void Initialize(ResourceManager resourceManager)
        {
            ResourceLoader loader = new ResourceLoader(resourceManager);
            Instance = loader;
        }

        public string GetString(string resourceName)
        {
            string stringRes = _manager.GetString(resourceName, _cultureInfo);
            return stringRes;
        }

        public void SetCultureInfo(CultureInfo cultureInfo)
        {
            _cultureInfo = cultureInfo;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
