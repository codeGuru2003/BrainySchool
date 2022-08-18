using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthRecordsPro.Models
{
    public class Audit
    {
        public string RecordedBy { get; set; }
        public DateTime DateRecorded { get; set; } = DateTime.Now;
#nullable enable
        [ForeignKey("RecordedBy")]
        public ApplicationUser? User { get; set; }


    }
}
