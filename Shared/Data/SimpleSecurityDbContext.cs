using System;
using System.Configuration;
using System.Data.Entity;
using System.Text.RegularExpressions;
using Ucsb.Sa.Enterprise.Utility;

namespace Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity.Data
{
    public class SimpleSecurityDbContext : BaseDbContext<SimpleSecurityDbContext>
    {

        public SimpleSecurityDbContext(SimpleSecurityConfig config) : base(
            ConfigurationManager.ConnectionStrings[config.ConnectionStringName].ConnectionString
        )
        {
            Database.SetInitializer<SimpleSecurityDbContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
			modelBuilder.Configurations.Add(new AccessInfoMap());
	        modelBuilder.Configurations.Add(new AccessLogEntryMap());
		}

		public IDbSet<AccessInfo> AccessInfos { get; set; }
		public IDbSet<AccessLogEntry> AccessLogEntries { get; set; }

        private static string GetUnderscoreSplitString(string input)
        {
			//	NOT ACTUALLY USED


            if (!Regex.IsMatch(input, "[A-Z]"))
            {
                // If there are no uppercase characters, there's nothing we can do: just return the property
                return input;
            }

            // Remove all underscores, if they exist
            string result = input.Replace("_", String.Empty);

            // Split camel casing into underscores
            // Source: https://msdn.microsoft.com/en-us/data/jj819164.aspx?f=255&MSPPError=-2147217396
            result = Regex.Replace(result, ".[A-Z]", m => m.Value[0] + "_" + m.Value[1]);

            return result.ToLower();
        }

}
}
