﻿using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Ezfx.DataAnnotations.EmailOrPhoneAttribute
{
    internal static class AppSettings
    {
        private static volatile bool _settingsInitialized = false;
        private static object _appSettingsLock = new object();
        private static void EnsureSettingsLoaded()
        {
            if (!_settingsInitialized)
            {
                lock (_appSettingsLock)
                {
                    if (!_settingsInitialized)
                    {
                        NameValueCollection settings = null;

                        try
                        {
                            settings = ConfigurationManager.AppSettings;
                        }
                        catch (ConfigurationErrorsException) { }
                        finally
                        {
                            if (settings == null || !Boolean.TryParse(settings["dataAnnotations:dataTypeAttribute:disableRegEx"], out _disableRegEx))
                            {
                                //VSO480141 Disable RegEx by default for DataAnotationAttributes in .NET 4.7.2 
                                //_disableRegEx = BinaryCompatibility.Current.TargetsAtLeastFramework472;
                            }
                            _settingsInitialized = true;
                        }
                    }
                }
            }
        }

        private static bool _disableRegEx;
        internal static bool DisableRegEx
        {
            get
            {
                EnsureSettingsLoaded();
                return _disableRegEx;
            }
        }
    }
}