using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthRecordsPro.Models
{
    public class StudentGrade
    {
        [Key]
        public int Id { get; set; }
        public int StudentClassId { get; set; }
        public int AcademicSemesterPeriodId { get; set; }
        public double Grade { get; set; }
#nullable enable
        public string? GradeLetter { get; set; }
        
        [ForeignKey("StudentClassId")]
        public virtual StudentClass? StudentClass { get; set; }

        [ForeignKey("AcademicSemesterPeriodId")]
        public virtual AcademicSemesterPeriod? AcademicSemesterPeriod { get; set; }

    }
}
