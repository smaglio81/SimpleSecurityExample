namespace Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity
{
    public class SimpleSecurityConfig
    {
        public string ConnectionStringName { get; set; }

		public string AccessTable { get; set; }
		public string AccessLogTable { get; set; }
        //public string AccessSproc { get; set; }
        //public string AccessLogSproc { get; set; }

        //internal const string DefaultAccessSproc = "[dbo].[usp_GetSimpleSecurityAccess]";
        //internal const string DefaultAccessLogSproc = "[dbo].[usp_InsertSimpleSecurityAccessLogEntry]";

	    internal const string DefaultAccessTable = "[dbo].[tbl_SimpleSecurityAccess]";
	    internal const string DefaultAccessLogTable = "[dbo].[tbl_SimpleSecurityAccessLog]";


    }
}
