using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Configuration
{
    public interface IConfigurationStreamProviderFactory
    {
        IConfigurationStreamProvider Create();
    }
}
