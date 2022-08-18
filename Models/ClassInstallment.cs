using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthRecordsPro.Models
{
    public class ClassInstallment : Audit
    {
        [Key]
        public int Id { get; set; } 
        public int ClassId { get; set; }
        public int InstallmentTypeId { get; set; }
        public double AmountinUSD { get; set; }
        public double AmountinLRD { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

#nullable enable
        [ForeignKey("ClassId")]
        public Class? Class { get; set; }

        [ForeignKey("InstallmentTypeId")]
        public InstallmentType? InstallmentType { get; set; }
    }
}
