using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LSLS.Models
{
    public class FileOnline
    {
        [Key]
        public string FileId { get; set; }
        public string FileName { get; set; }
        public long? FileSize { get; set; }
        public long? FileVersion { get; set; }
        public DateTime? FileCreatedTime { get; set; }
    }
}