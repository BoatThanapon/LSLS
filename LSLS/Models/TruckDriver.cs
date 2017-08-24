using System.ComponentModel.DataAnnotations;

namespace LSLS.Models
{
    public class TruckDriver
    {
        [Key]
        public int TruckDriverId { get; set; }

        [Required]
        [Display(Name = "Username")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Username length must be between 4 to 15 characters")]
        public string TruckDriverUsername { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Password length must be between 4 to 15 characters")]
        [DataType(DataType.Password)]
        public string TruckDriverPassword { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Password length must be between 4 to 15 characters")]
        [Compare("TruckDriverPassword", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [DataType(DataType.Password)]
        public string TruckDriverConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Full name")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Fullname length must be between 4 to 30 characters")]
        public string TruckDriverFullname { get; set; }


        [Required]
        [Display(Name = "Gender")]
        public string TruckDriverGender { get; set; }

        [Required]
        [Display(Name = "CitizenID")]
        [RegularExpression(@"^(\d{13})$", ErrorMessage = "CitizenID must be number of 0-9 in 13 digits")]
        public string TruckDriverCitizenId { get; set; }

        [Required]
        [Display(Name = "Driving LicenseID")]
        [RegularExpression(@"^(\d{8})$", ErrorMessage = "Driving LicenseID must be number of 0-9 in 8 digits")]
        public string TruckDriverDriverLicenseId { get; set; }

        [Required]
        [Display(Name = "Address")]
        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string TruckDriverAddress { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public string TruckDriverBirthdate { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string TruckDriverEmail { get; set; }

        [Required]
        [Display(Name = "Telephone Number")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Telephone Number must be number of 0-9 in 10 digits")]
        // ReSharper disable once InconsistentNaming
        public string TruckDriverTelephoneNo { get; set; }

        [Required]
        [Display(Name = "TruckID")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "TruckId length must be between 4 to 30 characters")]
        public string TruckId { get; set; }
    }
}