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
    public class JobsAssignedsController : ApiController
    {
        private visionDatabaseEntities db = new visionDatabaseEntities();

        // GET: api/JobsAssigneds
        public IQueryable<JobsAssigned> GetJobsAssigneds()
        {
            DateTime dt = DateTime.Today;

            DateTime endDate = dt.AddDays(2);
            DateTime startDate = dt.AddDays(1);

            var query = from t in db.JobsAssigneds
                        where t.AssignSTARTTIME >= dt && t.AssignSTARTTIME <= endDate
                        select t;

            return query;
        }

        // GET: api/JobsAssigneds/5
        [ResponseType(typeof(JobsAssigned))]
        public IHttpActionResult GetJobsAssigned(int id)
        {
            JobsAssigned jobsAssigned = db.JobsAssigneds.Find(id);
            if (jobsAssigned == null)
            {
                return NotFound();
            }
            return Ok(jobsAssigned);
        }

    // PUT: api/JobsAssigneds/5
    [ResponseType(typeof(void))]
        public IHttpActionResult PutJobsAssigned(int id, JobsAssigned jobsAssigned)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jobsAssigned.AssignID)
            {
                return BadRequest();
            }

            db.Entry(jobsAssigned).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobsAssignedExists(id))
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

        // POST: api/JobsAssigneds
        [ResponseType(typeof(JobsAssigned))]
        public IHttpActionResult PostJobsAssigned(JobsAssigned jobsAssigned)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JobsAssigneds.Add(jobsAssigned);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (JobsAssignedExists(jobsAssigned.AssignID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = jobsAssigned.AssignID }, jobsAssigned);
        }

        // DELETE: api/JobsAssigneds/5
        [ResponseType(typeof(JobsAssigned))]
        public IHttpActionResult DeleteJobsAssigned(int id)
        {
            JobsAssigned jobsAssigned = db.JobsAssigneds.Find(id);
            if (jobsAssigned == null)
            {
                return NotFound();
            }

            db.JobsAssigneds.Remove(jobsAssigned);
            db.SaveChanges();

            return Ok(jobsAssigned);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobsAssignedExists(int id)
        {
            return db.JobsAssigneds.Count(e => e.AssignID == id) > 0;
        }
    }
}