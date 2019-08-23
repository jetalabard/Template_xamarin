﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Configuration
{
    public interface IConfigurationManager
    {
        Task<Configuration> GetAsync(CancellationToken cancellationToken);
    }
}
