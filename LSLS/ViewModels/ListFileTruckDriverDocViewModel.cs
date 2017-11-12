using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LSLS.Models;

namespace LSLS.ViewModels
{
    public class ListFileTruckDriverDocViewModel
    {
        public TruckDriverDocument TruckDriverDocument { get; set; }
        public IEnumerable<FileDetail> FileDetails { get; set; }
    }
}