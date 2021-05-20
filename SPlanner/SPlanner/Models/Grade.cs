using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SPlanner.Interfaces;

namespace SPlanner.Models
{
    public class Grade : IGrade
    {
        public int GradeID { get; set; }

        [Display(Name = "Grade")]
        [Range(0, 9999999999999999.99)]
        public decimal Gradee { get; set; }
        public int UserID { get; set; }
        public int SubjectID { get; set; }
       
        public virtual User User { get; set; }
        public virtual Subject Subject { get; set; }
    }
}