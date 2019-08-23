using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Configuration
{
    public class ConfigurationManager : IConfigurationManager
    {
        private readonly SemaphoreSlim _semaphoreSlim;
        private bool _initialized;

        private static IConfigurationStreamProviderFactory _factory;

        private Configuration _configuration;

        public static IConfigurationManager Instance { get; } = new ConfigurationManager();

        public static void Initialize(IConfigurationStreamProviderFactory factory)
        {
            _factory = factory;
        }

        public async Task<Configuration> GetAsync(CancellationToken cancellationToken)
        {
            await InitializeAsync(cancellationToken).ConfigureAwait(false);

            if (_configuration == null)
                throw new InvalidOperationException("Configuration should not be null");

            return _configuration;
        }

        protected ConfigurationManager()
        {
            _semaphoreSlim = new SemaphoreSlim(1, 1);
        }

        private async Task InitializeAsync(CancellationToken cancellationToken)
        {
            if (_initialized)
                return;

            try
            {
                await _semaphoreSlim.WaitAsync(cancellationToken).ConfigureAwait(false);

                if (_initialized)
                    return;

                var configuration = await ReadAsync().ConfigureAwait(false);
                _initialized = true;
                _configuration = configuration;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }


        private async Task<Configuration> ReadAsync()
        {
            using (var streamProvider = _factory.Create())
            using (var stream = await streamProvider.GetStreamAsync().ConfigureAwait(false))
            {
                var configuration = Deserialize<Configuration>(stream);
                return configuration;
            }
        }

        private T Deserialize<T>(Stream stream)
        {
            if (stream == null || !stream.CanRead)
                return default;

            using (var sr = new StreamReader(stream))
            using (var jtr = new Newtonsoft.Json.JsonTextReader(sr))
            {
                var js = new Newtonsoft.Json.JsonSerializer();
                var value = js.Deserialize<T>(jtr);
                return value;
            }
        }

    }
}
