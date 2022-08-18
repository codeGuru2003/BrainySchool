using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthRecordsPro.Models
{
    public class ClassFaculty : Audit
    {
        [Key]
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int FacultyId { get; set; }
#nullable enable    
        [ForeignKey("ClassId")]
        public Class? Class { get; set; }

        [ForeignKey("FacultyId")]
        public Faculty? Faculty { get; set; }
    }
}
