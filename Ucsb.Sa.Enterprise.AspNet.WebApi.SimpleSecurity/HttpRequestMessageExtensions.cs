using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity
{
    public static class HttpRequestMessageExtensions
    {

        public static AccessLogEntry CreateAccessLogEntry(this HttpRequestMessage message)
        {
            var entry = new AccessLogEntry();

	        entry = PopulateAccessLogEntry(message, entry);

            return entry;
        }

	    public static AccessLogEntry PopulateAccessLogEntry(this HttpRequestMessage message, AccessLogEntry entry)
	    {
		    entry.DateTime = DateTime.Now;
		    entry.ComputerName = Environment.MachineName;

			if (message.Properties.ContainsKey("MS_HttpContext"))
		    {
			    HttpRequestBase request = ((HttpContextWrapper)message.Properties["MS_HttpContext"]).Request;

			    entry.IpAddress = request.UserHostAddress;
			    var XForwardedFor = request.Headers["X-Forwarded-For"];
			    if (string.IsNullOrWhiteSpace(XForwardedFor) == false)
			    {
				    var ips = XForwardedFor.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
				    entry.XForwardedFor = XForwardedFor;

				    if (ips.Length > 0)
				    {
					    var ipWithPort = ips[0].Trim();
					    entry.IpAddress = ipWithPort.Split(':')[0]; // drops the port
				    }
				}

			    entry.Uri = request.Url.AbsoluteUri;
			}

		    return entry;
	    }

	}
}
