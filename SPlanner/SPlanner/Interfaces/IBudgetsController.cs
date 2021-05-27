using SPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SPlanner.Interfaces
{
    interface IBudgetsController
    {
        ActionResult Index(string sort, string searchString);
        void ExportToExcel();
        ActionResult ImportToData(FormCollection formCollection);
        ActionResult Details(int? id);
        ActionResult Create();
        ActionResult Create([Bind(Include = "BudgetID,NameExp,DataOfBudget,PlanedExp,ActualExp")] Budget budget);
        ActionResult Edit(int? id);
        ActionResult Edit([Bind(Include = "BudgetID,NameExp,DataOfBudget,PlanedExp,ActualExp")] Budget budget);
        ActionResult Delete(int? id);
        ActionResult DeleteConfirmed(int id);

    }
}
