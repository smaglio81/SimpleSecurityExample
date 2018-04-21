namespace Ucsb.Sa.Enterprise.AspNet.WebApi.SimpleSecurity
{
    public class AccessInfo
    {

        public int Id { get; set; }
        public string UcsbNetId { get; set; }
        public string UcsbCampusId { get; set; }
        public bool Allowed { get; set; }

    }

}
