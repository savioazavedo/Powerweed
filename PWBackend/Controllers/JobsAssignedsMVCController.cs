using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PWBackend;
using Syncfusion.EJ.Export;
using Syncfusion.XlsIO;
using Syncfusion.JavaScript.Models;
using System.Data.Entity.Core.Objects;
//using Syncfusion.EJ.Export;
//using Syncfusion.JavaScript.Models;
//using System.Collections;
//using System.Reflection;
//using Syncfusion.XlsIO;
//using System.Web.sc;



namespace PWBackend.Controllers
{
    public class JobsAssignedsMVCController : Controller
    {
        private visionDatabaseEntities db = new visionDatabaseEntities();

        // GET: JobsAssignedsMVC/Index
        public ActionResult Index()
        {
            return View(db.JobsAssigneds.OrderByDescending(o => o.AssignSTARTTIME).ToList());
        }
        
        //
        public ActionResult GridView()
        {
            //return View(db.JobsAssigneds.ToList());

            db.Configuration.AutoDetectChangesEnabled = false; //added line
            db.Configuration.LazyLoadingEnabled = false; //added line
            db.Configuration.ProxyCreationEnabled = false; //added line

            //var DataSource = db.JobsAssigneds.ToList();

            var DataSource = (from job in db.JobsAssigneds
                              join emp in db.EmployeeJobs
                              on job.AssignID equals emp.AssignID
                              select new
                              {
                                  AssignID = job.AssignID,
                                  AssignJOBNUM = job.AssignJOBNUM,
                                  AssignCLIENT = job.AssignCLIENT,
                                  AssignWORK = job.AssignWORK,
                                  AssignAREA = job.AssignAREA,
                                  AssignINSTRUCTIONS = job.AssignINSTRUCTIONS,
                                  AssignTRUCK = job.AssignTRUCK,
                                  TextSENT = job.TextSENT,
                                  AssignSTARTTIME = job.AssignSTARTTIME,
                                  AssignENDTIME = job.AssignENDTIME,
                                  Hours = EntityFunctions.DiffHours(job.AssignSTARTTIME, job.AssignENDTIME),
                                  EmpNAME = emp.EmpNAME
                              }).ToList();

            ViewBag.datasource = DataSource;

            var DataSource2 = (from d in db.EmployeeJobs
                              select new {AssignID = d.AssignID,Name = d.EmpNAME }).ToList();

            ViewBag.datasource2 = DataSource2;


            return View();
        }


        public void ExportToExcel(string GridModel)
        {
            ExcelExport exp = new ExcelExport();
            var DataSource = (from job in db.JobsAssigneds
                                               join emp in db.EmployeeJobs
                                               on job.AssignID equals emp.AssignID
                                               select new
                                               {
                                                   AssignID = job.AssignID,
                                                   AssignJOBNUM = job.AssignJOBNUM,
                                                   AssignCLIENT = job.AssignCLIENT,
                                                   AssignWORK = job.AssignWORK,
                                                   AssignAREA = job.AssignAREA,
                                                   AssignINSTRUCTIONS = job.AssignINSTRUCTIONS,
                                                   AssignTRUCK = job.AssignTRUCK,
                                                   TextSENT = job.TextSENT,
                                                   AssignSTARTTIME = job.AssignSTARTTIME,
                                                   AssignENDTIME = job.AssignENDTIME,
                                                   Hours = EntityFunctions.DiffHours(job.AssignSTARTTIME, job.AssignENDTIME),
                                                   EmpNAME = emp.EmpNAME
                                               }).ToList();

            GridProperties obj = (GridProperties)Syncfusion.JavaScript.Utils.DeserializeToModel(typeof(GridProperties), GridModel);

            exp.Export(obj, DataSource, "Export.xlsx", ExcelVersion.Excel2010, false, false, "flat-saffron");
        }

        //private GridProperties ConvertGridObject(string gridProperty)
        //{
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    IEnumerable div = (IEnumerable)serializer.Deserialize(gridProperty, typeof(IEnumerable));
        //    GridProperties gridProp = new GridProperties();
        //    foreach (KeyValuePair<string, object> ds in div)
        //    {
        //        var property = gridProp.GetType().GetProperty(ds.Key, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
        //        if (property != null)
        //        {
        //            Type type = property.PropertyType;
        //            string serialize = serializer.Serialize(ds.Value);
        //            object value = serializer.Deserialize(serialize, type);
        //            property.SetValue(gridProp, value, null);
        //        }
        //    }
        //    return gridProp;
        //}

        // GET: JobsAssignedsMVC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobsAssigned jobsAssigned = db.JobsAssigneds.Find(id);
            if (jobsAssigned == null)
            {
                return HttpNotFound();
            }
            return View(jobsAssigned);
        }

        //GET: Jobs assigned today
        public ActionResult todaysDetail(DateTime? dt)
        {
            if (dt == null)
            {
                dt = DateTime.Today;
            }

            //DateTime endDate = dt.AddDays(1);

            var query = from t in db.JobsAssigneds
                        where t.AssignSTARTTIME >= dt.Value && t.AssignSTARTTIME <= dt.Value.AddDays(1)
                        select t;

            if (query == null)
            {
                return HttpNotFound();
            }
            return View(query);
        }

        // GET: JobsAssignedsMVC/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JobsAssignedsMVC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssignID,AssignJOBNUM,AssignCLIENT,AssignWORK,AssignAREA,AssignSTARTTIME,AssignENDTIME,AssignINSTRUCTIONS,AssignTRUCK,TextSENT")] JobsAssigned jobsAssigned)
        {
            if (ModelState.IsValid)
            {
                db.JobsAssigneds.Add(jobsAssigned);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jobsAssigned);
        }

        // GET: JobsAssignedsMVC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobsAssigned jobsAssigned = db.JobsAssigneds.Find(id);
            if (jobsAssigned == null)
            {
                return HttpNotFound();
            }
            return View(jobsAssigned);
        }

        // POST: JobsAssignedsMVC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssignID,AssignJOBNUM,AssignCLIENT,AssignWORK,AssignAREA,AssignSTARTTIME,AssignENDTIME,AssignINSTRUCTIONS,AssignTRUCK,TextSENT")] JobsAssigned jobsAssigned)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobsAssigned).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobsAssigned);
        }

        // GET: JobsAssignedsMVC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobsAssigned jobsAssigned = db.JobsAssigneds.Find(id);
            if (jobsAssigned == null)
            {
                return HttpNotFound();
            }
            return View(jobsAssigned);
        }


        public ActionResult DeleteWorker(int? id)
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


        public ActionResult AddWorker(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //EmployeeJob employeeJob = db.EmployeeJobs;
            //if (employeeJob == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(employeeJob);

            EmployeeJob employeeJob = new EmployeeJob();
            employeeJob.AssignID = id;

            ViewBag.AssignID = id;

            var empname = from e in db.Employees
                          select  e.EmpNAME ;

            ViewBag.Workerlist = empname;

            return View(employeeJob);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddWorker([Bind(Include = "EmployeeJOBSID,AssignID,EmpNAME")] EmployeeJob employeeJob)
        {
            if (ModelState.IsValid)
            {
                //employeeJob.AssignID = ViewBag.AssignID;
                db.EmployeeJobs.Add(employeeJob);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = employeeJob.AssignID });
            }

            //ViewBag.AssignID = new SelectList(db.JobsAssigneds, "AssignID", "AssignJOBNUM", employeeJob.AssignID);
            return View(employeeJob);
        }



        // POST: EmployeeJobsMVC/DeleteWorker/5
        [HttpPost, ActionName("DeleteWorker")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedTest(int id)
        {
            EmployeeJob employeeJob = db.EmployeeJobs.Find(id);
            int? assignid = employeeJob.AssignID;
            db.EmployeeJobs.Remove(employeeJob);
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = assignid }); ;
        }



        // POST: JobsAssignedsMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobsAssigned jobsAssigned = db.JobsAssigneds.Find(id);
            var employeeJob = db.EmployeeJobs.Where(x=>x.AssignID == id);

            db.EmployeeJobs.RemoveRange(employeeJob);
            db.JobsAssigneds.Remove(jobsAssigned);

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
