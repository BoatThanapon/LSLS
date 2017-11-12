using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LSLS.Models
{
    public class FileDetail
    {
        [Key]
        public Guid FileId { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string FileCategory { get; set; }
        public DateTime? LastModified { get; set; }

        public virtual int TruckDriverDocId { get; set; }
        public virtual TruckDriverDocument TruckDriverDocument { get; set; }

    }


}