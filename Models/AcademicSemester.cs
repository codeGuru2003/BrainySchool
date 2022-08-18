using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthRecordsPro.Models
{
    public class AcademicSemester : Audit
    {
        [Key]
        public int Id { get; set; }
        public int AcademicSchoolYearId { get; set; }
        public int AcademicSemesterTypeId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
#nullable enable
        [ForeignKey("AcademicSemesterTypeId")]
        public AcademicSemesterType? AcademicSemesterType { get; set; }

        [ForeignKey("AcademicSchoolYearId")]
        public AcademicSchoolYear? AcademicSchoolYear { get; set; } 
    }
}
