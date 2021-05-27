using SPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SPlanner.Interfaces
{
    interface IEventsController
    {
        ActionResult Index(string sort, string searchString, string currentFilter, int? page);
        ActionResult Details(int? id);
        ActionResult Create();
        ActionResult Create([Bind(Include = "EventID,StartDate,EndDate,Thema,Description,CategoryID")] Event @event);
        ActionResult Edit(int? id);
        ActionResult Edit([Bind(Include = "EventID,StartDate,EndDate,Thema,Description,CategoryID,UserID")] Event @event);
        ActionResult Delete(int? id);
        ActionResult DeleteConfirmed(int id);

    }
}
