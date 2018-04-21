using System;
using Ninject.Activation;
using Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity.Data;

namespace Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity.Ninject
{
    public class SimpleSecurityDbContextProvider : Provider<SimpleSecurityDbContext>
    {
        protected override SimpleSecurityDbContext CreateInstance(IContext context)
        {
            var config = SimpleSecurityConfigManager.GetConfig();
            var dbContext = new SimpleSecurityDbContext(config);
            return dbContext;
        }

        public Type Type
        {
            get { return typeof(SimpleSecurityDbContext); }
        }
    }
}
