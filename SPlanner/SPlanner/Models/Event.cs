using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SPlanner.Interfaces;

namespace SPlanner.Models
{
    public class Event : IEvent
    {
        public int EventID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Thema")]
        public string Thema { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Descriptions")]
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public int UserID { get; set; }
        
        public virtual Category Category { get; set; }
        public virtual User User { get; set; }
    }
}