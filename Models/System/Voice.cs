using System.ComponentModel.DataAnnotations;

namespace RiseRestApi.Models
{
    public partial class Voice
    {
        [Key]
        public int VoiceId { get; set; }
        public string VoiceName { get; set; }
        public string Description { get; set; }
        public bool IsRemoved { get; set; }
    }
}
