using System.Collections.Generic;
using LSLS.Models;

namespace LSLS.ViewModels
{
    public class FormJobAssignmentViewModel
    {
        public JobAssignment JobAssignment { get; set; }
        public IEnumerable<TruckDriver> TruckDrivers { get; set; }
    }
}