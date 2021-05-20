using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SPlanner.Interfaces;

namespace SPlanner.Models
{
    public class User : IUser
    {

        public int UserID { get; set; }

        [Required] //wymagane pole 
        [StringLength(50)]//,ErrorMessage="Max 50 chars"]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Email")]
        //[EmailAddress]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format. Example email: example@exp.pl")]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(270, MinimumLength = 7, ErrorMessage = "Password must have min length of 7")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Max Length of 100")]
        [Display(Name = "College")]
        public string College { get; set; }
        public int RolaID { get; set; }

        public virtual Rola Rola { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
        public virtual ICollection<Budget> Budget { get; set; }
    }
}