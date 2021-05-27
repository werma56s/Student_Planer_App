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
using OfficeOpenXml;
using System.Drawing;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using System.Globalization;
using System.Data.Entity.Validation;
using SPlanner.Interfaces;

namespace SPlanner.Controllers
{
    public class BudgetsController : Controller, IBudgetsController
    {
        private SPlannerContext db = new SPlannerContext();

        // GET: Budgets
        public ActionResult Index(string sort, string searchString)
        {
            var budgets = db.Budgets.Include(b => b.User); //eager loading

            ViewBag.NameExpSort = String.IsNullOrEmpty(sort) ? "name_desc" : "";
            ViewBag.DataOfBudgetSort = sort == "dataBudget" ? "dataBudget_desc" : "dataBudget";
            try
            {
                int userid = int.Parse(Session["UserID"].ToString());
                budgets = budgets.Where(e => e.UserID == userid);

                //wyszukiwanie
                if (!String.IsNullOrEmpty(searchString))
                {
                    budgets = budgets.Where(e => e.NameExp.Contains(searchString));
                }
                //sortowanie 
                switch (sort)
                {
                    case "name_desc":
                        budgets = budgets.OrderByDescending(s => s.NameExp);
                        break;
                    case "dataBudget":
                        budgets = budgets.OrderBy(s => s.DataOfBudget);
                        break;
                    case "dataBudget_desc":
                        budgets = budgets.OrderByDescending(s => s.DataOfBudget);
                        break;
                    default:
                        budgets = budgets.OrderBy(s => s.NameExp);
                        break;
                }
                return View(budgets.ToList());
            }
            catch (Exception e)
            {
                TempData["Error"] = "<script>alert('Refresh website and login agan');</script>";
            }

            return RedirectToAction("Login", "Users"); //View("~/Views/Users/Login.cshtml");

        }
        //Export data to Excel
        public void ExportToExcel()
        {
            try
            {
                var userid = int.Parse(Session["UserID"].ToString());
                /*var budgets = from e in db.Budgets
                              where e.UserID == userid
                              select e;*/
                var budgets = db.Budgets.Where(e => e.UserID == userid);
                budgets.ToList();

                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                ExcelPackage excelPackage = new ExcelPackage();
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Budgetes");

                worksheet.Cells["A1"].Value = "Date";
                worksheet.Cells["B1"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

                worksheet.Cells["A4"].Value = "NameExp";
                worksheet.Cells["B4"].Value = "DataOfBudget";
                worksheet.Cells["C4"].Value = "PlanedExp";
                worksheet.Cells["D4"].Value = "ActualExp";
                worksheet.Cells["E4"].Value = "Difference";

                int rowStart = 5;
                foreach (var item in budgets)
                {
                    if (item.Difference < 0)
                    {
                        worksheet.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
                    }

                    worksheet.Cells[string.Format("A{0}", rowStart)].Value = item.NameExp;
                    worksheet.Cells[string.Format("B{0}", rowStart)].Value = item.DataOfBudget;
                    worksheet.Cells[string.Format("C{0}", rowStart)].Value = item.PlanedExp;
                    worksheet.Cells[string.Format("D{0}", rowStart)].Value = item.ActualExp;
                    worksheet.Cells[string.Format("E{0}", rowStart)].Value = item.Difference;
                    rowStart++;
                }
                worksheet.Cells["A:AZ"].AutoFitColumns();
                string excelName = "BudgetReport";
                using (var memoryStream = new MemoryStream()) 
                {
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment; filename=" + excelName + ".xlsx");
                    excelPackage.SaveAs(memoryStream);
                    memoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }

            }catch (Exception e)
            {
                TempData["Error"] = "<script>alert('Export Valid');</script>";
            }

        }
        //Import data form Excel to database
        [HttpPost]
        public ActionResult ImportToData(FormCollection formCollection)
        {
            try
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if (file != null && file.ContentLength > 0 && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));

                    using (var epPackage = new ExcelPackage(file.InputStream))
                    {
                        ExcelPackage.LicenseContext = LicenseContext.Commercial;
                        ExcelWorksheet worksheet = epPackage.Workbook.Worksheets.First();

                        for (var row = 2; row <= worksheet.Dimension.End.Row; row++) //start to row 2 and igonore the header
                        {
                            //save
                            var budget = new Budget();
                            budget.NameExp = worksheet.Cells[row, 1].Value.ToString();

                            double dateNum = double.Parse(worksheet.Cells[row, 2].Value.ToString());
                            DateTime result = DateTime.FromOADate(dateNum);
                            budget.DataOfBudget = result; 
                            budget.PlanedExp = Convert.ToDecimal(worksheet.Cells[row, 3].Value);
                            budget.ActualExp = Convert.ToDecimal(worksheet.Cells[row, 4].Value);
                            budget.UserID = int.Parse(Session["UserID"].ToString());
                            //Create(budget);
                            db.Budgets.Add(budget);
                            db.SaveChanges();
                        }
                        return RedirectToAction("Index");
                    }
                }
              }
            catch (DbEntityValidationException ex) 
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                
                // Throw a new DbEntityValidationException with the improved exception message.
                TempData["Error"] = "<script>alert('Import Valid - Read about this');</script>";
            }catch(Exception e)
            {
                TempData["Error"] = "<script>alert('Import Valid - Read about this');</script>";
            }
            return View("ImportToData");
        }

        // GET: Budgets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budget budget = db.Budgets.Find(id);
            if (budget == null)
            {
                return HttpNotFound();
            }
            return View(budget);
        }

        // GET: Budgets/Create
        public ActionResult Create()
        {
            //ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName");
            return View();
        }

        // POST: Budgets/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BudgetID,NameExp,DataOfBudget,PlanedExp,ActualExp")] Budget budget)
        {

            if (ModelState.IsValid)
            {
                budget.UserID = int.Parse(Session["UserID"].ToString());
                db.Budgets.Add(budget);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", budget.UserID);
            return View("Create", new Budget());
        }

        // GET: Budgets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budget budget = db.Budgets.Find(id);
            if (budget == null)
            {
                return HttpNotFound();
            }
            //ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", budget.UserID);
            return View(budget);
        }

        // POST: Budgets/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BudgetID,NameExp,DataOfBudget,PlanedExp,ActualExp")] Budget budget)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    budget.UserID = int.Parse(Session["UserID"].ToString());
                    db.Entry(budget).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = $"Error: {e}";
            }

            //ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", budget.UserID);
            return View(budget);
        }

        // GET: Budgets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budget budget = db.Budgets.Find(id);
            if (budget == null)
            {
                return HttpNotFound();
            }
            return View(budget);
        }

        // POST: Budgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Budget budget = db.Budgets.Find(id);
            db.Budgets.Remove(budget);
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
