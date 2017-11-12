using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LSLS.Models;

namespace LSLS.ViewModels
{
    public class SentToMailViewModel
    {
        public string To { get; set; }
        public string Subject { get; set; }

        [StringLength(200)]
        [DataType(DataType.MultilineText)]   
        public string Body { get; set; }

        public FileDetail FileDetail { get; set; }

    }
}