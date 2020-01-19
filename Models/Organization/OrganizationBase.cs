namespace RiseRestApi.Models
{
    public class OrganizationBase
    {
        public int AddressId { get; set; }
        public int AdminPersonId { get; set; }
        public string OrganizationName { get; set; }
        public string BlobImageName { get; set; }
        public string MainPhone { get; set; }
        public string Website { get; set; }
        public bool IsRemoved { get; set; }
    }
}
