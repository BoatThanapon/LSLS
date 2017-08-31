using System;
using System.ComponentModel.DataAnnotations;

namespace LSLS.Models
{
    public class TransportationInf
    {
        [Key]
        public int ShippingId { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Employer length must be between 4 to 30 characters")]
        [Display(Name = "Employer")]
        public string Employer { get; set; }

        [Required]
        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        public DateTime? OrderDate { get; set; }

        [Required]
        [Display(Name = "Date of Transportation")]
        [DataType(DataType.Date)]
        public DateTime? DateOfTransportation { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Product Name length must be between 4 to 30 characters")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Starting Point")]
        public string StartingPoint { get; set; }

        [Required]
        [Display(Name = "Destination")]
        public string Destination { get; set; }

        [Required]
        [Display(Name = "Reciever Name")]
        public string RecieverName { get; set; }

        [Required]
        [Display(Name = "Status of Transportation")]
        public bool StatusOfTransportation { get; set; }


        [DataType(DataType.ImageUrl)]
        public string ShipingDocImageUrl { get; set; }


    }
}