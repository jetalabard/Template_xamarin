using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Forms.Helpers
{
    public interface IResourceLoader : INotifyPropertyChanged
    {
        ResourceLoader GetInstance();
        void SetInstance(ResourceLoader value);

        //static void Initialize(ResourceManager resourceManager);

        string GetString(string resourceName);

        void SetCultureInfo(CultureInfo cultureInfo);
    }
}
