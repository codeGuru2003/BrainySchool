using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthRecordsPro.Models
{
    public class Student : Audit
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="First Name")]
        public string Firstname { get; set; }
        [Display(Name ="Middle Name")]
        public string Middlename { get; set; }
        [Required]
        [Display(Name ="Last Name")]
        public string Lastname { get; set; }
        [Required,Display(Name ="Date of Birth")]
        public DateTime DateofBirth { get; set; }
        public int NationalityTypeId { get; set; }
        public int StudentTypeId { get; set; }

        [Required, Display(Name ="Phone Number")]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ApplicationUserId { get; set; }
        public byte[] Image { get; set; }
        public string ImageName { get; set; }
        public bool IsActive { get; set; }  
        public int GenderTypeId { get; set; }
#nullable enable

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? StudentUser { get; set; }

        [ForeignKey("StudentTypeId")]
        public StudentType? StudentType { get; set; }

        [ForeignKey("NationalityTypeId")]
        public NationalityType? NationalityType { get; set; }

        [ForeignKey("GenderTypeId")]
        public GenderType? GenderType { get; set; }
    }
}
