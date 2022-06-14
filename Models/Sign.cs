using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DFA_Talan2.Models
{
    public class Sign
    {
        [Key]
        public int userId { get; set; }
        [DisplayName("Employee Name")]
        [Required(ErrorMessage = "This is Mandatory")]
        public string userName { get; set; }
        [DisplayName("Employee Name")]
        [Required(ErrorMessage = "This is Mandatory")]
        public string userMail { get; set; }
        [DisplayName("Employee Name")]
        [Required(ErrorMessage = "This is Mandatory")]
        public string userGender { get; set; }
        [DisplayName("Employee Name")]
        [Required(ErrorMessage = "This is Mandatory")]
        public string userPassword { get; set; }
    }
}