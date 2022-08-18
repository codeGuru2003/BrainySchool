using System.ComponentModel.DataAnnotations;

namespace HealthRecordsPro.Models
{
    public class AcademicSchoolYearType : Audit
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
