using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Text;

namespace Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity.Data
{
    public class AccessInfoMap : EntityTypeConfiguration<AccessInfo>
	{
		public AccessInfoMap()
		{
			var config = SimpleSecurityConfigManager.GetConfig();

			ToTable(config.AccessTable);
			HasKey(t => t.Id);
		}
	}
}
