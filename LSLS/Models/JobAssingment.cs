using System;
using System.ComponentModel.DataAnnotations;

namespace LSLS.Models
{
    public class JobAssingment
    {
        [Key]
        public int JobAssignmentId { get; set; }

        //TransportationInf
        public TransportationInf TransportationInf { get; set; }
        [Display(Name = "Shipping No.")]
        public virtual int ShippingId { get; set; }


        //TruckDriver
        public TruckDriver TruckDriver { get; set; }
        public virtual int TruckDriverId { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime JobAssignmentDateTime { get; set; }

        [Display(Name = "From")]
        public string StartingPointJob { get; set; }

        public float LatitudeStartJob { get; set; }
        public float LongitudeStartJob { get; set; }

        [Display(Name = "To")]
        public string DestinationJob { get; set; }

        public float LatitudeDesJob { get; set; }
        public float LongitudeDesJob { get; set; }


        [Display(Name = "Status")]
        public bool JobAssignmentStatus { get; set; }

    }
}