namespace Core.Configuration
{
    public interface IConfigurationStreamProviderFactory
    {
        IConfigurationStreamProvider Create();
    }
}
