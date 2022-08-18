using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthRecordsPro.Models
{
    public class AcademicSchoolYear : Audit
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int AcademicSchoolTypeId { get; set; }
        public bool IsActive { get; set; }
#nullable enable
        [ForeignKey("AcademicSchoolTypeId")]
        public AcademicSchoolYearType? AcademicSchoolYearType { get; set; }
    }
}
