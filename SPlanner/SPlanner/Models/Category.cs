using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SPlanner.Interfaces;

namespace SPlanner.Models
{
    public class Category : ICategory
    {
        public int CategoryID { get; set; }

        [StringLength(100)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}