using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DFA_Talan2.Models
{
    public class Employee
    {
        
        public int empid { get; set; }
        [DisplayName("Employee Name")]
        [Required(ErrorMessage = "This is Mandatory")]
        public string empname { get; set; }
        [DisplayName("Employee Mail")]
        [Required(ErrorMessage = "This is Mandatory")]
        public string empmail { get; set; }
        [DisplayName("Employee Salary")]
        [Required(ErrorMessage = "This is Mandatory")]
        public string empsalary { get; set; }
    }
}