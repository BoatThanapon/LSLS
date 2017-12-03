using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LSLS.Models
{
    public class PaymentDocument
    {
        [Key]
        public Guid PaymentDocId { get; set; }
        public string PaymentFileName { get; set; }
        public string Extension { get; set; }
        public DateTime? PaymentLastModified { get; set; }
        public virtual ICollection<PaymentDocument> PaymentDocuments { get; set; }

    }
}