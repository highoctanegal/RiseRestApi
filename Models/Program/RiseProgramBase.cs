using System;

namespace RiseRestApi.Models
{
    public class RiseProgramBase
    {
        public string Code { get; set; }
        public string ProgramName { get; set; }
        public int? AdminPersonId { get; set; }
        public int? OrganizationId { get; set; }
        public bool IsRemoved { get; set; }
    }
}
