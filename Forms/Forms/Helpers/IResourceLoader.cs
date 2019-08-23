using System.ComponentModel;
using System.Globalization;

namespace Forms.Helpers
{
    public interface IResourceLoader : INotifyPropertyChanged
    {
        ResourceLoader GetInstance();

        void SetInstance(ResourceLoader value);

        string GetString(string resourceName);

        void SetCultureInfo(CultureInfo cultureInfo);
    }
}
