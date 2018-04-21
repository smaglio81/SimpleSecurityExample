using System.Linq;
using System.Threading.Tasks;
using Ninject;
using Ucsb.Sa.Enterprise.Utility;

namespace Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity.Data
{
    public class SimpleSecurityInfoRepository : BaseInfoRepository<SimpleSecurityDbContext>
    {

        public SimpleSecurityInfoRepository(IKernel kernel) : base(kernel)
        {}

	    /// <summary>
	    /// Retrieve the access permissions for a given <paramref name="ucsbNetId" />.
	    /// </summary>
	    /// <param name="ucsbNetId">
	    /// The users ucsbNetId.
	    /// </param>
	    /// <returns>
	    /// A populated AccessInfo object. If no access object is found, then an empty object with
	    /// Allowed set to false is returned.
	    /// </returns>
	    public async Task<AccessInfo> GetAccessAsync(string ucsbNetId)
	    {
		    var context = GetDbContext();

		    var info = context.AccessInfos.FirstOrDefault(i => i.UcsbNetId == ucsbNetId);

		    if (info == null)
		    {
			    info = new AccessInfo()
			    {
				    UcsbNetId = ucsbNetId,
				    Allowed = false
			    };
			}

		    return info;
	    }

	    /// <summary>
	    /// Inserts an audit log entry for attempts to access the service.
	    /// </summary>
	    /// <param name="ucsbNetId">
	    /// The users ucsbNetId.
	    /// </param>
	    /// <returns>
	    /// A populated AccessInfo object. If no access object is found, then an empty object with
	    /// Allowed set to false is returned.
	    /// </returns>
	    public async Task<AccessLogEntry> LogAccessAttemptAsync(AccessLogEntry entry)
	    {
		    var context = GetDbContext();

		    context.AccessLogEntries.Add(entry);
		    await context.SaveChangesAsync();

		    return entry;
	    }

	}
}
