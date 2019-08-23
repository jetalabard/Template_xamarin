using Core.Configuration;

namespace Forms.iOS.Configuration
{
    public class IOSConfigurationStreamProviderFactory : IConfigurationStreamProviderFactory
    {
        public IConfigurationStreamProvider Create()
        {
            return new IOSConfigurationStreamProvider();
        }
    }
}