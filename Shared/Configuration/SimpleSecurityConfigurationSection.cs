using System;
using System.Configuration;

namespace Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity.Configuration
{
    public class SimpleSecurityConfigurationSection : ConfigurationSection
    {

        #region variables

        private static readonly string __ConfigurationNotSet =
            "SimpleSecurity could not find a configuration section within the " +
            "applications .config file. This can be accomplished by adding" + Environment.NewLine +
            Environment.NewLine +
            "<configSections>" + Environment.NewLine +
            "	<section name=\"clientExtensions\" type=\"Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity.Configuration.SimpleSecurityConfigurationSection,Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity\" />" + Environment.NewLine +
            "</configSections>" + Environment.NewLine +
            Environment.NewLine +
            "<connectionStrings>" + Environment.NewLine +
            "	<connectionString name=\"{AppDatabaseName}\" connectionString=\"Initial Catalog={AppDatabaseName};Data Source={appdatabasename}.sql.{env}.sa.ucsb.edu,2433;Integrated Security=SSPI;\" />" + Environment.NewLine +
            "</connectionStrings>" + Environment.NewLine +
            Environment.NewLine +
            "<simpleSecurity connectionStringName=\"{AppDatabaseName}\">" + Environment.NewLine +
			"	(optional: <database accessTable=\"[dbo].[tbl_SimpleSecurityAccess]\" accessTableLog=\"[dbo].[tbl_SimpleSecurityAccessLog]\" />)" + Environment.NewLine +
            "</simpleSecurity>" + Environment.NewLine +
            Environment.NewLine +
            "to your applications .config file. ";

        private const string __ConfigExceptionMessage =
            "Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity, SimpleSecurityConfigurationSection.{0} property is not set. " +
            "Please set the necessary value within the configuration file. This should be " +
            "in the <simpleSecurity> config section with a key/value pair in the form " +
            "<simpleSecurity ... {1}=\"{2}\" .../>.";

        private const string __ConfigExceptionElementMessage =
            "Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity, SimpleSecurityConfigurationSection.{0} property is not set properly. " +
            "Please set the necessary value within the configuration file. This should be " +
            "in the <simpleSecurity>/<{1}> config section with a key/value pair in the form " +
            "<{1} ... />.";

        private static SimpleSecurityConfigurationSection _Configuration;

        #endregion

        #region static properties

        /// <summary>
        /// Retrieves an new instance of a <see cref="SimpleSecurityConfigurationSection" /> populated
        /// with the current information held within the application configuration file.
        /// </summary>
        public static SimpleSecurityConfigurationSection Configuration
        {
            get
            {
                if (_Configuration == null)
                {
                    _Configuration =
                        (SimpleSecurityConfigurationSection)ConfigurationManager.GetSection("simpleSecurity");


                    if (_Configuration == null)
                        throw new ConfigurationErrorsException(__ConfigurationNotSet);
                }

                return _Configuration;
            }
        }

        #endregion

        #region public properties

        /// <summary>
        /// A unique name for this session transfer handler definition.
        /// </summary>
        [ConfigurationProperty(name: "connectionStringName", IsRequired = true)]
        public string ConnectionStringName
        {
            get
            {
                var result = (string)this["connectionStringName"];
                if (string.IsNullOrEmpty(result))
                {
                    throw new ConfigurationErrorsException(
                        string.Format(
                            __ConfigExceptionMessage,
                            "ConnectionStringName",
                            "connectionStringName",
                            @"{AppDatabaseName}"
                        )
                    );
                }

                return result;
            }
            set { this["connectionStringName"] = value; }
        }

        /// <summary>
        /// A unique name for this session transfer handler definition.
        /// </summary>
        [ConfigurationProperty(name: "database")]
        public SimpleSecurityDatabaseConfigurationElement Database
        {
            get
            {
                var val = this["database"];
                if (val == null) return null;

                if(val is SimpleSecurityDatabaseConfigurationElement)
                {
                    var database = (SimpleSecurityDatabaseConfigurationElement) this["database"];
                    return database;
                }

                throw new ConfigurationErrorsException(
                    string.Format(
                        __ConfigExceptionElementMessage,
                        "Database",
                        "database"
                    )
                );
            }
        }

        #endregion

    }
}
