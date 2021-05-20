using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPlanner.Interfaces
{
    interface IEvent
    {
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        string Thema { get; set; }
        string Description { get; set; }
    }
}
