using System;
using System.ComponentModel.DataAnnotations;

namespace LSLS.Models
{
    public class TransportationInf
    {
        [Key]
        public int ShippingId { get; set; }

        [Required]
        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        public string OrderDate { get; set; }

        [Required]
        [Display(Name = "Date of Transportation")]
        [DataType(DataType.Date)]
        public string DateOfTransportation { get; set; }

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
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Employer length must be between 4 to 30 characters")]
        [Display(Name = "Employer")]
        public string Employer { get; set; }

        [Required]
        [Display(Name = "Receiver Name")]
        public string ReceiverName { get; set; }

        [Required]
        [Display(Name = "Status of Transportation")]
        public bool StatusOfTransportation { get; set; }

        [Required]
        public bool JobIsActive { get; set; }

        public DateTime? ReceiveDateTime { get; set; }

        public string ShippingDocImage { get; set; }

    }
}