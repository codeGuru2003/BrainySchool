using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthRecordsPro.Models
{
    public class Class : Audit
    {
        [Key]
        public int Id { get; set; }
        public int ClassTypeId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

#nullable enable
        [ForeignKey("ClassTypeId")]
        public ClassType? ClassType { get; set; }
    }
}
