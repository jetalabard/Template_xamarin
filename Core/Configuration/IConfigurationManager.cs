using System.Threading;
using System.Threading.Tasks;

namespace Core.Configuration
{
    public interface IConfigurationManager
    {
        Task<Configuration> GetAsync(CancellationToken cancellationToken);
    }
}
