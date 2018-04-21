using Microsoft.Web.Http;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http;
using Ucsb.Sa.Enterprise.Soa;

namespace SimpleSecurity.Web.V1.Controllers
{
	[ApiVersion("1.0")]
	public class HealthcheckController : Ucsb.Sa.Enterprise.Soa.HealthcheckController
	{

		/// <summary>
		/// Performs a healthcheck
		/// </summary>
		/// <returns>A HealthcheckResponse object with information about the service.</returns>
		[AcceptVerbs("GET")]
		public override async Task<HealthcheckResponse> Healthcheck()
		{

			//	add application specific healthcheck code here


			return await base.Healthcheck();
		}

	}
}
