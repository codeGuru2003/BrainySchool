using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthRecordsPro.Models
{
    public class StudentClass : Audit
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }  
        public int ClassId { get; set; }
        public int AcademicSemesterId { get; set; }
#nullable enable
        [ForeignKey("StudentId")]
        public Student? Student { get; set; }

        [ForeignKey("ClassId")]
        public Class? Class { get; set; }

        [ForeignKey("AcademicSchoolYearId")]
        public AcademicSchoolYear? AcademicSchoolYear { get; set; }
    }
}
