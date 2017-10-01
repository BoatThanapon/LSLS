using System.ComponentModel.DataAnnotations;

namespace LSLS.Models
{
    public class JobAssignment
    {
        [Key]
        public int JobAssignmentId { get; set; }

        //TransportationInf
        public TransportationInf TransportationInf { get; set; }
        [Display(Name = "Shipping No.")]
        public virtual int ShippingId { get; set; }


        public TruckDriver TruckDriver { get; set; }
        [Display(Name = "TruckID")]
        public virtual int TruckDriverId { get; set; }

        [Required]
        [Display(Name = "Date of Transportation")]
        [DataType(DataType.Date)]
        public string JobAssignmentDate { get; set; }

        [Required]
        [Display(Name = "From")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "StartingPointJob length must be between 4 to 30 characters")]
        public string StartingPointJob { get; set; }

        [Required]
        public float LatitudeStartJob { get; set; }

        [Required]
        public float LongitudeStartJob { get; set; }

        [Required]
        [Display(Name = "To")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "DestinationJob length must be between 4 to 30 characters")]
        public string DestinationJob { get; set; }

        [Required]
        public float LatitudeDesJob { get; set; }

        [Required]
        public float LongitudeDesJob { get; set; }

        [Display(Name = "Status")]
        public bool JobAssignmentStatus { get; set; }

    }
}