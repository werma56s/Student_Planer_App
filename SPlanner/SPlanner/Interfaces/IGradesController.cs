using SPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SPlanner.Interfaces
{
    interface IGradesController
    {
        ActionResult Index(int? id);
        ActionResult Details(int? id);
        ActionResult Create();
        ActionResult Create([Bind(Include = "GradeID,Gradee,SubjectID")] Grade grade);
        ActionResult Edit(int? id);
        ActionResult Edit([Bind(Include = "GradeID,Gradee,SubjectID")] Grade grade);
        ActionResult Delete(int? id);
        ActionResult DeleteConfirmed(int id);

    }
}
