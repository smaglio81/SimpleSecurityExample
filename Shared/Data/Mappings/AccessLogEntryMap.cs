using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Text;

namespace Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity.Data
{
    public class AccessLogEntryMap : EntityTypeConfiguration<AccessLogEntry>
	{
		public AccessLogEntryMap()
		{
			var config = SimpleSecurityConfigManager.GetConfig();

			ToTable(config.AccessLogTable);
			HasKey(t => t.Id);
		}
	}
}
