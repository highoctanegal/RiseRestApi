using System.ComponentModel.DataAnnotations;

namespace RiseRestApi.Models
{
    public class UsState : IModel
    {
        [Key]
        public int UsStateId { get; set; }
        public string UsStateName { get; set; }
        public string UsStateAbbrev { get; set; }
        public int Id => UsStateId;
    }
}
