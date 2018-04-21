using Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity.Configuration;

namespace Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity
{
    public static class SimpleSecurityConfigManager
    {

        private static SimpleSecurityConfig _Config = null;

        /// <summary>
        /// Gets a completed <see cref="SimpleSecurityConfig" /> object.
        /// </summary>
        /// <returns>A populated <see cref="SimpleSecurityConfig" />.</returns>
        public static SimpleSecurityConfig GetConfig()
        {
            //  if defined through code then the _Config value should already have been set.
            //  if its null, then pull the information from the application .config
            if (_Config == null)
            {
                _Config = GetConfigFromFile();
            }

            return _Config;
        }

        /// <summary>
        /// Sets the config for the SimpleSecurity service.
        /// </summary>
        /// <param name="config">The populated config to use.</param>
        /// <returns>The config.</returns>
        public static SimpleSecurityConfig SetConfig(SimpleSecurityConfig config)
        {
            _Config = config;
            return _Config;
        }

        internal static SimpleSecurityConfig GetConfigFromFile()
        {
            //  throws an exception if a configuration section is not found.
            var configSection = SimpleSecurityConfigurationSection.Configuration;

            var config = new SimpleSecurityConfig();

            config.ConnectionStringName = configSection.ConnectionStringName;
	        config.AccessTable = configSection.Database.AccessTable;
	        config.AccessLogTable = configSection.Database.AccessTableLog;
			//config.AccessSproc = configSection.Database.AccessSproc;
			//config.AccessLogSproc = configSection.Database.AccessLogSproc;

			return config;
        }
    }
}
