using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LSLS.Models;

namespace LSLS.ViewModels
{
    public class FormJobAssignmentViewModel
    {
        public IEnumerable<TransportationInf> TransportationInf { get; set; }
        public JobAssignment JobAssignment { get; set; }
        public IEnumerable<TruckDriver> TruckDrivers { get; set; }
    }
}