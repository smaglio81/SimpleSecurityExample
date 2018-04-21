using System.Configuration;

namespace Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity.Configuration
{
    public class SimpleSecurityDatabaseConfigurationElement : ConfigurationElement
    {

        #region variables

        private const string __ConfigExceptionMessage =
            "Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity, SimpleSecurityConfigurationSection.{0} property is not set. " +
            "Please set the necessary value within the configuration file. This should be " +
            "in the <simpleSecurity>/<database> config section with a key/value pair in the form " +
            "<database ... {1}=\"{2}\" .../>.";

        private static SimpleSecurityConfigurationSection _Configuration;

		#endregion

		#region public properties

		/// <summary>
		/// The name of the table which holds access entries.
		/// </summary>
		[ConfigurationProperty(name: "accessTable", DefaultValue = SimpleSecurityConfig.DefaultAccessTable)]
		public string AccessTable
		{
			get
			{
				var result = (string)this["accessTable"];
				if (string.IsNullOrEmpty(result))
				{
					throw new ConfigurationErrorsException(
						string.Format(
							__ConfigExceptionMessage,
							"AccessTable",
							"accessTable",
							SimpleSecurityConfig.DefaultAccessTable
						)
					);
				}

				return result;
			}
			set { this["accessTable"] = value; }
		}

		///// <summary>
	    /// The name of the table which holds access entries.
		///// </summary>
		//[ConfigurationProperty(name: "accessSproc", DefaultValue = SimpleSecurityConfig.DefaultAccessSproc)]
		//      public string AccessSproc
		//      {
		//          get
		//          {
		//              var result = (string)this["accessSproc"];
		//              if (string.IsNullOrEmpty(result))
		//              {
		//                  throw new ConfigurationErrorsException(
		//                      string.Format(
		//                          __ConfigExceptionMessage,
		//                          "AccessSproc",
		//                          "accessSproc",
		//                          SimpleSecurityConfig.DefaultAccessSproc
		//                      )
		//                  );
		//              }

		//              return result;
		//          }
		//          set { this["accessSproc"] = value; }
		//      }

		/// <summary>
		/// The name of the table with audit log entries
		/// </summary>
		[ConfigurationProperty(name: "accessTableLog", DefaultValue = SimpleSecurityConfig.DefaultAccessLogTable)]
        public string AccessTableLog
        {
            get
            {
                var result = (string)this["accessTableLog"];
                if (string.IsNullOrEmpty(result))
                {
                    throw new ConfigurationErrorsException(
                        string.Format(
                            __ConfigExceptionMessage,
							"AccessTableLog",
							"accessLogaccessTableLogSproc",
                            SimpleSecurityConfig.DefaultAccessLogTable
						)
                    );
                }

                return result;
            }
            set { this["accessTableLog"] = value; }
        }

		///// <summary>
		/// The name of the table with audit log entries
		///// </summary>
		//[ConfigurationProperty(name: "accessLogSproc", DefaultValue = SimpleSecurityConfig.DefaultAccessLogSproc)]
		//public string AccessLogSproc
		//{
		// get
		// {
		//  var result = (string)this["accessLogSproc"];
		//  if (string.IsNullOrEmpty(result))
		//  {
		//   throw new ConfigurationErrorsException(
		//    string.Format(
		//	    __ConfigExceptionMessage,
		//	    "AccessLogSproc",
		//	    "accessLogSproc",
		//	    SimpleSecurityConfig.DefaultAccessLogSproc
		//    )
		//   );
		//  }

		//  return result;
		// }
		// set { this["accessLogSproc"] = value; }
		//}

		#endregion

	}
}
