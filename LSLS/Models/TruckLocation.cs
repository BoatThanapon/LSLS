using System;
using System.ComponentModel.DataAnnotations;

namespace LSLS.Models
{
    public class TruckLocation
    {
        [Key]
        public int TruckLocationId { get; set; }
        public TruckDriver TruckDriver { get; set; }
        public virtual int TruckDriverId { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public DateTime? TruckCurrentTime { get; set; }
        public string TruckCurrentAddress { get; set; }

    }
}