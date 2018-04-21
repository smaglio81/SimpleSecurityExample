using System.Web.Http;
using Microsoft.Web.Http;
using Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity;

namespace SimpleSecurity.Web.V1.Controllers
{
	[ApiVersion("1.0")]
    public class ValuesController : ApiController
    {

		[LdapAuthentication]
		[Authorize]
		public ValuesResult Get()
	    {
			var result = new ValuesResult();
		    return result;
	    }

    }

	public class ValuesResult
	{
		public string Name;
		public int Age;
	}
}
