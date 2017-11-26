using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PWBackend;

namespace PWBackend.Controllers
{
    public class EmployeeJobsController : ApiController
    {
        private visionDatabaseEntities db = new visionDatabaseEntities();

        // GET: api/EmployeeJobs
        public IQueryable<EmployeeJob> GetEmployeeJobs()
        {
            return db.EmployeeJobs;
        }

        // GET: api/EmployeeJobs/5
        [ResponseType(typeof(EmployeeJob))]
        public IHttpActionResult GetEmployeeJob(int id)
        {
            EmployeeJob employeeJob = db.EmployeeJobs.Find(id);
            if (employeeJob == null)
            {
                return NotFound();
            }

            return Ok(employeeJob);
        }

        // PUT: api/EmployeeJobs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployeeJob(int id, EmployeeJob employeeJob)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeeJob.EmployeeJOBSID)
            {
                return BadRequest();
            }

            db.Entry(employeeJob).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeJobExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EmployeeJobs
        [ResponseType(typeof(EmployeeJob))]
        public IHttpActionResult PostEmployeeJob(EmployeeJob employeeJob)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmployeeJobs.Add(employeeJob);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EmployeeJobExists(employeeJob.EmployeeJOBSID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = employeeJob.EmployeeJOBSID }, employeeJob);
        }

        // DELETE: api/EmployeeJobs/5
        [ResponseType(typeof(EmployeeJob))]
        public IHttpActionResult DeleteEmployeeJob(int id)
        {
            EmployeeJob employeeJob = db.EmployeeJobs.Find(id);
            if (employeeJob == null)
            {
                return NotFound();
            }

            db.EmployeeJobs.Remove(employeeJob);
            db.SaveChanges();

            return Ok(employeeJob);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeJobExists(int id)
        {
            return db.EmployeeJobs.Count(e => e.EmployeeJOBSID == id) > 0;
        }
    }
}