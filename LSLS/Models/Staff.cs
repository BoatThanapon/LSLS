using System.ComponentModel.DataAnnotations;

namespace LSLS.Models
{
    public class Staff
    {
        [Key]
        public int StaffId { get; set; }

        [Required]
        [Display(Name = "EmployeeID")]
        [RegularExpression(@"^([E])+(\d{4})", ErrorMessage ="EmployeeID must be begin with E and follow by number 0-9 in 4 digits.")]
        public string StaffEmployeeId { get; set; }

        [Required]
        [Display(Name = "Username")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Username length must be between 4 to 15 characters")]
        public string StaffUsername { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Password length must be between 4 to 15 characters")]
        [DataType(DataType.Password)]
        public string StaffPassword { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [StringLength(15, MinimumLength = 4,
            ErrorMessage = "Confirm Password length must be between 4 to 15 characters")]
        [Compare("StaffPassword", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [DataType(DataType.Password)]
        public string StaffConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Fullname")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Fullname length must be between 4 to 30 characters")]
        public string StaffFullname { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string StaffGender { get; set; }

        [Required]
        [Display(Name = "CitizenID")]
        [RegularExpression(@"^(\d{13})$", ErrorMessage = "CitizenID must be number of 0-9 in 13 digits")]
        public string StaffCitizenId { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public string StaffBirthdate { get; set; }

        [Required]
        [Display(Name = "Address")]
        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string StaffAddress { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string StaffEmail { get; set; }

        [Required]
        [Display(Name = "Telephone Number")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Telephone Number must be number of 0-9 in 10 digits")]
        public string StaffTelephoneNo { get; set; }
    }
}