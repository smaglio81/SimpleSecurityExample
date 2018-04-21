using System.Web;
using Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity;

[assembly: PreApplicationStartMethod(typeof(SimpleSecurityConfig), "Register")]

namespace $rootnamespace$
{
	public class SimpleSecurityConfig
	{

		public static void Register()
		{
			SimpleSecurityConfig config = new SimpleSecurityConfig()
			{
				//	required
				//	this is name from the connectionStrings section of the config file
				ConnectionStringName = "<connection string name>",

				/*
				 * SQL SETUP
				 *
				 * The database tables and permissions are setup using the sc_CreateSimpleSecurityTables.sql
				 * script. It should
				 *		1) create a table to hold info on how can access the serivce
				 *		2) create a table to hold access log audit entries
				 *		3) the permissions to access the tables using EF
				 */

				//	optional
				//	the name of the database table which has the access permissions
				AccessTable = "[dbo].[tbl_SimpleSecurityAccess]",

				//	optional
				//	the name of the database table which has the audit log entries
				AccessLogTable = "[dbo].[tbl_SimpleSecurityAccessLog]"
			};

			SimpleSecurityConfigManager.SetConfig(config);
		}

	}

}
