using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Ninject.Modules;
using Ninject.Web.Common;
using Ninject.Web.WebApi.FilterBindingSyntax;
using Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity.Data;

namespace Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity.Ninject
{
    public class SimpleSecurityNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<SimpleSecurityDbContext>().ToProvider<SimpleSecurityDbContextProvider>().InRequestScope();

            this.BindHttpFilter<LdapAuthenticationAttribute>(FilterScope.Controller)	//  on controllers
                .WhenControllerHas<LdapAuthenticationAttribute>();
	        this.BindHttpFilter<LdapAuthenticationAttribute>(FilterScope.Action) //  on actions
		        .WhenActionMethodHas<LdapAuthenticationAttribute>();
        }
    }
}
