using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LSLS.Models;

namespace LSLS.ViewModels
{
    public class TruckDriverDocViewModel
    {
        public FileDetail FileDetail { get; set; }
        public IEnumerable<TruckDriverDocument> TruckDriverDocuments { get; set; }
    }
}