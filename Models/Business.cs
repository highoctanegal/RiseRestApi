using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RiseRestApi.Models
{
    public partial class Business
    {
        public Business()
        {
            Person = new HashSet<Person>();
        }

        [Key]
        public int BusinessId { get; set; }
        public string BusinessName { get; set; }

        public virtual ICollection<Person> Person { get; set; }
    }
}
