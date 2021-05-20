using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SPlanner.Interfaces;

namespace SPlanner.Models
{
    public class Subject : ICategory
    {
        public int SubjectID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Grade> Grades { get; set; }
    }
}