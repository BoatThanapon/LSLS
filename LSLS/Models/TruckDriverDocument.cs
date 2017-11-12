using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LSLS.Models
{
    public class TruckDriverDocument
    {
        [Key]
        public int TruckDriverDocId { get; set; }
        public TruckDriver TruckDriver { get; set; }
        public virtual int TruckDriverId { get; set; }

        public virtual ICollection<FileDetail> FileDetails { get; set; }

    }
}