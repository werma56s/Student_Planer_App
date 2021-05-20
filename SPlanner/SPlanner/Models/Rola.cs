using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SPlanner.Interfaces;

namespace SPlanner.Models
{
    public class Rola : ICategory
    {
        public int RolaID { get; set; }

        [StringLength(50)]
        [Display(Name = "Type of Rola")]
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}