using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LSLS.ViewModels
{
    public class LoginStaffViewModel
    {
        [Required(ErrorMessage = "Please input EmployeeId")]
        [Display(Name = "EmployeeID")]
        public string StaffEmployeeId { get; set; }

        [Required(ErrorMessage = "Please input Username")]
        [Display(Name = "Username")]
        public string StaffUsername { get; set; }

        [Required(ErrorMessage = "Please input Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string StaffPassword { get; set; }

    }
}