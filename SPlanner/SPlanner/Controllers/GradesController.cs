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

namespace SPlanner.Controllers
{
    public class GradesController : Controller
    {
        private SPlannerContext db = new SPlannerContext();

        // GET: Grades
        public ActionResult Index(int? id)
        {
            var grades = db.Grades.Include(g => g.Subject).Include(g => g.User);
            try
            {
                var userid = int.Parse(Session["UserID"].ToString());
                grades = grades.Where(e => e.UserID == userid);
                return View(grades.ToList());
            }
            catch(Exception e)
            {
                TempData["Error"] = "<script>alert('Refresh website and login agan');</script>";
            }

            /*grades = from e in db.Grades
                        where e.UserID == userid
                        select e;*/
            return RedirectToAction("Login", "Users");
        }
        // GET: Grades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = db.Grades.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        // GET: Grades/Create
        public ActionResult Create()
        {
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name");
            //ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName");
            return View();
        }

        // POST: Grades/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GradeID,Gradee,SubjectID")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                grade.UserID = int.Parse(Session["UserID"].ToString());
                db.Grades.Add(grade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name", grade.SubjectID);
            //ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", grade.UserID);
            return View(grade);
        }

        // GET: Grades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = db.Grades.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name", grade.SubjectID);
            //ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", grade.UserID);
            return View(grade);
        }

        // POST: Grades/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GradeID,Gradee,SubjectID")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                grade.UserID = int.Parse(Session["UserID"].ToString());
                db.Entry(grade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "Name", grade.SubjectID);
            //ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", grade.UserID);
            return View(grade);
        }

        // GET: Grades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = db.Grades.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        // POST: Grades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Grade grade = db.Grades.Find(id);
            db.Grades.Remove(grade);
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
