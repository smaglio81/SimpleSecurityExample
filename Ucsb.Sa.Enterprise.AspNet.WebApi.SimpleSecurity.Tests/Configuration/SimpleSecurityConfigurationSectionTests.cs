using Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity.Configuration;
using Xunit;

namespace Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity.Tests.Configuration
{
    public class SimpleSecurityConfigurationSectionTests
    {

        [Fact]
        public void read_connectionStringName()
        {
            var config = SimpleSecurityConfigurationSection.Configuration;

            Assert.Equal("SimpleSecurity", config.ConnectionStringName);
        }


        [Fact]
        public void read_database()
        {
            var config = SimpleSecurityConfigurationSection.Configuration;

            var defaultValue =new SimpleSecurityDatabaseConfigurationElement();

            Assert.Equal(defaultValue.AccessTable, config.Database.AccessTable);
            Assert.Equal(defaultValue.AccessTableLog, config.Database.AccessTableLog);

			//Assert.Equal("Test", config.Database.AccessTable);
			//Assert.Equal("LogTest", config.Database.AccessTableLog);
		}
	}
}
