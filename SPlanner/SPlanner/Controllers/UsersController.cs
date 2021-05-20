using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using SPlanner.DAL;
using SPlanner.Models;
using SPlanner.Security;

namespace SPlanner.Controllers
{
    public class UsersController : Controller
    {
        private SPlannerContext db = new SPlannerContext();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Rola);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details()
        {
            int id = int.Parse(Session["UserID"].ToString());
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Register()
        {
            //ViewBag.RolaID = new SelectList(db.Rolas, "RolaID", "Name");
            return View();
        }

        // POST: Users/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "UserID,FirstName,LastName,EmailAddress,Password,Remember,College")] User user)
        {
            var userr = db.Users.Where(x => x.EmailAddress == user.EmailAddress).FirstOrDefault();
            try
            {
                if (userr == null)
                {
                    if (PasswordSecurity.CheckPassword(user.Password))
                    {
                        if (ModelState.IsValid)
                        {
                            user.Password = PasswordSecurity.HashPassword(user.Password);
                            user.RolaID = 2;
                            db.Users.Add(user);
                            db.SaveChanges();

                            return RedirectToAction("Login", user);

                        }
                    }
                    else
                    {
                        TempData["Error"] = "<script>alert('The password must have minimum 7 letter, one special char, one number, one upper and lower case letter!');</script>";
                        //Content("<script language='javascript' type='text/javascript'>alert('Your Password had to: minimum 7 letter, one special char, one number, one upper and lower case letter!');</script>");
                    }
                }
                else
                {
                    ViewBag.Error = "Email exist in databse";
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e;
            }
            return View("Register", new User());
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User u)
        {

            try
            {
                u.Password = PasswordSecurity.HashPassword(u.Password);
                var user = db.Users.Where(x => x.EmailAddress == u.EmailAddress && x.Password == u.Password).FirstOrDefault();
                if (user != null)
                {
                    //return RedirectToAction("../Home/Calendar");

                    Session["UserID"] = user.UserID.ToString();
                    Session["RolaFK"] = user.RolaID.ToString();
                    Session["FirstName"] = user.FirstName.ToString();
                    Session["LastName"] = user.LastName.ToString();
                    Session["College"] = user.College.ToString();

                    var userRola = int.Parse(Session["RolaFK"].ToString());
                    if (Session["UserID"] != null && userRola == 1)
                    {
                        return RedirectToAction("AdminDashBoard");
                    }
                    else if (Session["UserID"] != null && userRola == 2)
                    {
                        return RedirectToAction("UserDashBoard");
                    }
                    else
                    {
                        return RedirectToAction("Login");
                    }

                }
                else
                {
                    ViewBag.Error = "Wrong login data";
                    //var alert = Content("<script language='javascript' type='text/javascript'>alert('Wrong login data!');</script>");
                    //Content("<script language='javascript' type='text/javascript'>alert('Wrong login data!');</script>");
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = "Write your data corectly";
            }

            return View("Login", new User());

        }
        public ActionResult UserDashBoard()
        {
            return View();

        }
        public ActionResult AdminDashBoard()
        {
            return View();
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Remove("UserID");
            HttpContext.Session.Remove("RolaFK");
            return RedirectToAction("../Home/Index");
        }

        // GET: Users/Edit/5
        public ActionResult Edit()
        {
            int id = int.Parse(Session["UserID"].ToString());
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FirstName,LastName,EmailAddress,Password,College")] User user)
        {
            if (ModelState.IsValid)
            {
                user.UserID = int.Parse(Session["UserID"].ToString());
                user.RolaID = int.Parse(Session["RolaFK"].ToString());
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var rola = int.Parse(Session["RolaFK"].ToString());
            if (rola == 2)
            {
                return RedirectToAction("../Home/Index");
            }
            return View(user); //RedirectToAction("../Home/Index");
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
