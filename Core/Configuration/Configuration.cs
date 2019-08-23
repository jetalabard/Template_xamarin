using System.Threading;
using System.Threading.Tasks;

namespace Core.Configuration
{
    public class Configuration
    {
        public string BaseUrl { get; set; }

        public static async Task<string> GetBaseUrl()
        {
            using (var cts = new CancellationTokenSource())
            {
                // Create or get a cancellation token from somewhere
                var config = await ConfigurationManager.Instance.GetAsync(cts.Token);
                return config.BaseUrl;

                // Use the configuration value
            }
        }
    }
}
