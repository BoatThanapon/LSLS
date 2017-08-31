using System.Collections.Generic;
using LSLS.Models;

namespace LSLS.ViewModels
{
    public class FormJobAssignmentViewModel
    {
        public IEnumerable<TruckDriver> TruckDrivers { get; set; }
        public JobAssingment JobAssingment { get; set; }
    }
}