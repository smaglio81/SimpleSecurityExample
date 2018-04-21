using System.Threading.Tasks;
using Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity.Data;

namespace Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity
{
    public class SimpleSecurityService
    {

        public SimpleSecurityInfoRepository SecurityInfoRepository;

        public SimpleSecurityService(SimpleSecurityInfoRepository securityInfoRepository)
        {
            SecurityInfoRepository = securityInfoRepository;
        }

        public async Task<AccessInfo> GetAccessInfoAsync(string ucsbNetId)
        {
            var accessInfo = await SecurityInfoRepository.GetAccessAsync(ucsbNetId);

            return accessInfo;
        }

        public async Task<AccessLogEntry> LogAccessAttemptAsync(AccessLogEntry entry)
        {
	        entry = await SecurityInfoRepository.LogAccessAttemptAsync(entry);

	        return entry;
		}
    }
}
