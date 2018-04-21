# Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity

Adds a very simple security authorization system into a WebApi service.
This was designed to be used with the Campus API Gateway until an OAuth system
can be put in place.




## Installation

From nuget:

`Install-Package Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity.Debug`



## Configuration / Setup


### sc_CreateSimpleSecurityTables.sql

This file should automatically be created in `/SimpleSecurityDb/sc_CreateSimpleSecurityTables.sql`.

This will
* Create a table to hold info on how can access the serivce
* Create a table to hold access log audit entries
* Create the permissions to access the tables using EF
* You will need to add the [SimpleSecurityRole] to your domain accounts permissions.


### sc_AddUser.sql

This file should automatically be created in `/SimpleSecurityDb/sc_AddUser.sql`.

This file should help in creating a new allow permissions for a user.


### SimpleSecurityConfig.cs (In Code Configuration)

This file should automatically be created under `/App_Start/SimpleSecurityConfig.cs`.

> Only use In Code Configuration or Web.Config Configuration. But, don't use both.

```
using System.Web;
using Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity;

[assembly: PreApplicationStartMethod(typeof(SimpleSecurityConfig), "Register")]

namespace ExampleWebApp
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
```


### Web.Config (Web.Config Configuration)

It is possible to configure the usage through the web.config file, but it's preferred to use In Code Configuration (above).

> Only use In Code Configuration or Web.Config Configuration. But, don't use both.

```
<configuration>
	<configSections>
		<section name="simpleSecurity" type="Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity.Configuration.SimpleSecurityConfigurationSection, Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity" />
	</configSections>

	<appSettings>
		<add key="applicationName" value="Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity.Web" />
		<add key="environment" value="local" />
	</appSettings>

	<connectionStrings>
		<add name="SimpleSecurity" connectionString="Initial Catalog=SimpleSecurity;Data Source=localhost;Integrated Security=SSPI;" providerName="System.Data.SqlClient" />
	</connectionStrings>

	<!-- connectionStringName is required -->
	<simpleSecurity connectionStringName="SimpleSecurity">
		<!-- database is optional -->
		<database accessTable="[dbo].[tbl_SimpleSecurityAccess]" accessLogTable="[dbo].[tbl_SimpleSecurityAccessLog]" />
	</simpleSecurity>

```