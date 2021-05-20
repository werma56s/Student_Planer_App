using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SPlanner.Interfaces;

namespace SPlanner.Models
{
    public class Budget : IBudget
    {
        public int BudgetID { get; set; }

        public string NameExp { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Budget Date")]
        public DateTime DataOfBudget { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        //[RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Maximum Two Decimal Points.")]
        [Range(0, 9999999999999999.99)] //aren't accepting any negative numbers - Otherwise, replace 0 with -9999999999999999.99
        public decimal PlanedExp { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        //[RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Maximum Two Decimal Points.")]
        [Range(0, 9999999999999999.99)]
        public decimal ActualExp { get; set; }

        //[RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Maximum Two Decimal Points.")]
        public decimal Difference => PlanedExp - ActualExp;
        public int UserID { get; set; }
        public virtual User User { get; set; }
        
    }
}