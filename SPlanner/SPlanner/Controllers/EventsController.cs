using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SPlanner.DAL;
using SPlanner.Models;
using PagedList;

namespace SPlanner.Controllers
{
    public class EventsController : Controller
    {
        private SPlannerContext db = new SPlannerContext();

        // GET: Events
        public ActionResult Index(string sort, string searchString, string currentFilter, int? page)
        {

            var events = db.Events.Include(x => x.Category).Include(x => x.User);

            ViewBag.CurrentSort = sort; //dodanie pagniacji danych
            ViewBag.ThemaSort = String.IsNullOrEmpty(sort) ? "thema_desc" : " ";
            ViewBag.StartDateSort = sort == "dataStart" ? "dataStart_desc" : "dataStart";
            ViewBag.EndDateSort = sort == "dataEnd" ? "dataEnd_desc" : "dataEnd";
            //
            try
            {
                int userid = int.Parse(Session["UserID"].ToString());
                events = events.Where(e => e.UserID == userid);

                //
                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }
                ViewBag.CurrentFilter = searchString;
                //wyszukiwanie
                if (!String.IsNullOrEmpty(searchString))
                {
                    events = events.Where(e => e.Thema.Contains(searchString) || e.Description.Contains(searchString));
                }
                //sortowanie 
                switch (sort)
                {
                    case "thema_desc":
                        events = events.OrderByDescending(s => s.Thema);
                        break;
                    case "dataStart":
                        events = events.OrderBy(s => s.StartDate);
                        break;
                    case "dataStart_desc":
                        events = events.OrderByDescending(s => s.StartDate);
                        break;
                    case "dataEnd":
                        events = events.OrderBy(s => s.EndDate);
                        break;
                    case "dataEnd_desc":
                        events = events.OrderByDescending(s => s.EndDate);
                        break;
                    default:
                        events = events.OrderBy(s => s.Thema);
                        break;
                }

                int pageSizle = 3;
                int pageNumber = (page ?? 1);

                return View(events.ToPagedList(pageNumber, pageSizle)); //events.ToList()
            }
            catch (Exception e)
            {
                TempData["Error"] = "<script>alert('Refresh website and login agan');</script>";
            }

            return RedirectToAction("Login", "Users"); //View("~/Views/Users/Login.cshtml");
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            //ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName");
            return View();
        }

        // POST: Events/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventID,StartDate,EndDate,Thema,Description,CategoryID")] Event @event)
        {
            if (ModelState.IsValid)
            {
                @event.UserID = int.Parse(Session["UserID"].ToString());
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", @event.CategoryID);
            return View(@event);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", @event.CategoryID);

            return View(@event);
        }

        // POST: Events/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventID,StartDate,EndDate,Thema,Description,CategoryID,UserID")] Event @event)
        {
            if (ModelState.IsValid)
            {
                @event.UserID = int.Parse(Session["UserID"].ToString());
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", @event.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", @event.UserID);
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
