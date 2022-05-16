using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace URL2App
{
    internal class Settings
    {
        const String SettingsFile = "appsettings.json";

        IConfiguration config;
        public Settings()
        {
            String fullPathToSettings = AppDomain.CurrentDomain.BaseDirectory + SettingsFile;
            if (!File.Exists(fullPathToSettings))
            {
                return;
            }
            config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(SettingsFile).Build();
        }
        public bool isDebug()
        {
            if (config == null) return false;
            return config.GetValue<bool>("isDebug");
        }

        public bool isDirectFileAccessAllowed()
        {
            if (config == null) return true;
            return config.GetValue<bool>("isDirectFileAccessAllowed");
        }

        public String grabKeyValueFromSettings(String keyToGet)
        {
            if (config == null) throw new ArgumentNullException("config null, Mostlikely due to missing or invalid appsettings.json");
            var valuesSection = config.GetSection("KeysToPath");
            foreach (IConfigurationSection section in valuesSection.GetChildren())
            {
                Console.WriteLine("checking " + keyToGet + " against key=" + section.GetValue<string>("key") + ", " + " with path=" + section.GetValue<string>("path"));
                var key = section.GetValue<string>("key");
                if (keyToGet.Equals(key))
                {
                    Console.WriteLine("Found matching key and path");
                    return section.GetValue<string>("path");
                }
            }
            return "";
        }
    }
}
