﻿using System;
using Android.Content;
using Core.Configuration;

namespace Forms.Droid.Configuration
{
    public class AndroidConfigurationStreamProviderFactory : IConfigurationStreamProviderFactory
    {
        private readonly Func<Context> _contextProvider;

        public AndroidConfigurationStreamProviderFactory(Func<Context> contextProvider)
        {
            _contextProvider = contextProvider;
        }

        public IConfigurationStreamProvider Create()
        {
            return new AndroidConfigurationStreamProvider(_contextProvider);
        }
    }
}