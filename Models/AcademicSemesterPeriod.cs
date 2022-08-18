using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthRecordsPro.Models
{
    public class AcademicSemesterPeriod : Audit
    {
        [Key]
        public int Id { get; set; }
        public int PeriodTypeId { get; set; }
        public int AcademicSemesterId { get; set; }

#nullable enable
        [ForeignKey("PeriodTypeId")]
        public PeriodType? PeriodType { get; set; }

        [ForeignKey("AcademicSemesterId")]
        public AcademicSemester? AcademicSemester { get; set; }

    }
}
