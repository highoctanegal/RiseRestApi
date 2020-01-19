using System.ComponentModel.DataAnnotations;

namespace RiseRestApi.Models
{
    public class Address : IModel
    {
        [Key]
        public int AddressId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public bool IsRemoved { get; set; }


        public Person Person { get; set; }
        public Organization Organization { get; set; }

        public int Id => AddressId;

    }
}
