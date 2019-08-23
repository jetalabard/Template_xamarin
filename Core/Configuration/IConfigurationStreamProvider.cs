using System;
using System.IO;
using System.Threading.Tasks;

namespace Core.Configuration
{
    public interface IConfigurationStreamProvider : IDisposable
    {
        Task<Stream> GetStreamAsync();
    }
}
