using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPlanner.Interfaces
{
    interface IBudget
    {
        string NameExp { get; set; }
        DateTime DataOfBudget { get; set; }
        decimal PlanedExp { get; set; }
        decimal ActualExp { get; set; }

    }
}
