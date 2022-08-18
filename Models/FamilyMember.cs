using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthRecordsPro.Models
{
    public class FamilyMember : Audit
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int GenderTypeId { get; set; }

        [DataType(DataType.Upload)]
        public byte[] Image { get; set; }
        public string ImageName { get; set; }

        [Display(Name = "Relationship Type")]
        public int FamilyMemberTypeId { get; set; }

        [Display(Name = "Marital Status")]
        public int? MaritalStatusTypeId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string Firstname { get; set; }
        [Display(Name = "Middle Name")]
        public string Middlename { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }

        [Required, Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
#nullable enable
        [ForeignKey("StudentId")]
        public Student? Student { get; set; }

        [ForeignKey("FamilyMemberTypeId")]
        public FamilyMemberType? FamilyMemberType { get; set; }

        [ForeignKey("MaritalStatusTypeId")]
        public MaritalStatusType? MaritalStatusType { get; set;}

        [ForeignKey("GenderTypeId")]
        public GenderType? GenderType { get; set; }
    }
}
