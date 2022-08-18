using System.ComponentModel.DataAnnotations;

namespace HealthRecordsPro.Models
{
    public class Images : Audit
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public string ImageName { get; set; }
    }
}
