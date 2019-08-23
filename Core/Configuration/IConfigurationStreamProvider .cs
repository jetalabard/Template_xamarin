﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Core.Configuration
{
    public interface IConfigurationStreamProvider : IDisposable
    {
        Task<Stream> GetStreamAsync();
    }
}