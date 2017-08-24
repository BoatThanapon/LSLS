using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LSLS.ViewModels
{
    public class TruckLocationViewModel
    {
        //TruckDriver
        public string TruckId { get; set; } 
        public string TruckDriverFullname { get; set; }

        //TruckLocation
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public DateTime? TruckCurrentTime { get; set; }
        public string TruckCurrentAddress { get; set; }
    }
}