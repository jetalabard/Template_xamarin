using Core.Configuration;

namespace Forms.UWP.Configuration
{
    public class UwpConfigurationStreamProviderFactory : IConfigurationStreamProviderFactory
    {
        public IConfigurationStreamProvider Create()
        {
            return new UwpConfigurationStreamProvider();
        }
    }
}
