using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PWBackend;

namespace PWBackend.Controllers
{
    public class EmployeeJobsMVCController : Controller
    {
        private visionDatabaseEntities db = new visionDatabaseEntities();

        // GET: EmployeeJobsMVC
        public ActionResult Index()
        {
            var employeeJobs = db.EmployeeJobs.Include(e => e.JobsAssigned);
            return View(employeeJobs.ToList());
        }

        // GET: EmployeeJobsMVC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeJob employeeJob = db.EmployeeJobs.Find(id);
            if (employeeJob == null)
            {
                return HttpNotFound();
            }
            return View(employeeJob);
        }

        // GET: EmployeeJobsMVC/Create
        public ActionResult Create()
        {
            ViewBag.AssignID = new SelectList(db.JobsAssigneds, "AssignID", "AssignJOBNUM");
            return View();
        }

        // POST: EmployeeJobsMVC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeJOBSID,AssignID,EmpNAME")] EmployeeJob employeeJob)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeJobs.Add(employeeJob);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssignID = new SelectList(db.JobsAssigneds, "AssignID", "AssignJOBNUM", employeeJob.AssignID);
            return View(employeeJob);
        }

        // GET: EmployeeJobsMVC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeJob employeeJob = db.EmployeeJobs.Find(id);
            if (employeeJob == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssignID = new SelectList(db.JobsAssigneds, "AssignID", "AssignJOBNUM", employeeJob.AssignID);
            return View(employeeJob);
        }

        // POST: EmployeeJobsMVC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeJOBSID,AssignID,EmpNAME")] EmployeeJob employeeJob)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeJob).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssignID = new SelectList(db.JobsAssigneds, "AssignID", "AssignJOBNUM", employeeJob.AssignID);
            return View(employeeJob);
        }

        // GET: EmployeeJobsMVC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeJob employeeJob = db.EmployeeJobs.Find(id);
            if (employeeJob == null)
            {
                return HttpNotFound();
            }
            return View(employeeJob);
        }


       

        // POST: EmployeeJobsMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeJob employeeJob = db.EmployeeJobs.Find(id);
            db.EmployeeJobs.Remove(employeeJob);
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
