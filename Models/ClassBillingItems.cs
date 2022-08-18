using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthRecordsPro.Models
{
    public class ClassBillingItems : Audit
    {
        [Key]
        public int Id { get; set; }
        public int ClassId { get; set; }
        [Required]
        public string Name { get; set; }
        public double AmountInUSD { get; set; }
        public double AmountInLRD { get; set; }
        public string Description { get; set; }

#nullable enable
        [ForeignKey("ClassId")]
        public Class? Class { get; set; }
    }
}
