using System;

namespace Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity
{
    public class AccessLogEntry
    {

	    public AccessLogEntry()
	    {
			DateTime = DateTime.Now;
	    }

        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string UcsbNetId { get; set; }
        public string UcsbCampusId { get; set; }
        public bool Success { get; set; }
        public string IpAddress { get; set; }
        public string XForwardedFor { get; set; }
        public string ComputerName { get; set; }
		public string Uri { get; set; }
        public string Reason { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorStackTrace { get; set; }
    }
}
