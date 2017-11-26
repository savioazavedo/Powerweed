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
    public class FleetsAPIController : ApiController
    {
        private visionDatabaseEntities db = new visionDatabaseEntities();

        // GET: api/Fleets
        public IQueryable<Fleet> GetFleets()
        {
            return db.Fleets;
        }

        // GET: api/Fleets/5
        [ResponseType(typeof(Fleet))]
        public IHttpActionResult GetFleet(int id)
        {
            Fleet fleet = db.Fleets.Find(id);
            if (fleet == null)
            {
                return NotFound();
            }

            return Ok(fleet);
        }

        // PUT: api/Fleets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFleet(int id, Fleet fleet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fleet.FLEETID)
            {
                return BadRequest();
            }

            db.Entry(fleet).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FleetExists(id))
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

        // POST: api/Fleets
        [ResponseType(typeof(Fleet))]
        public IHttpActionResult PostFleet(Fleet fleet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Fleets.Add(fleet);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = fleet.FLEETID }, fleet);
        }

        // DELETE: api/Fleets/5
        [ResponseType(typeof(Fleet))]
        public IHttpActionResult DeleteFleet(int id)
        {
            Fleet fleet = db.Fleets.Find(id);
            if (fleet == null)
            {
                return NotFound();
            }

            db.Fleets.Remove(fleet);
            db.SaveChanges();

            return Ok(fleet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FleetExists(int id)
        {
            return db.Fleets.Count(e => e.FLEETID == id) > 0;
        }
    }
}