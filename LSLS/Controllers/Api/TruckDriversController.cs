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
using LSLS.Models;

namespace LSLS.Controllers.Api
{
    public class TruckDriversController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/TruckDrivers
        public IQueryable<TruckDriver> GetTruckDrivers()
        {
            return db.TruckDrivers;
        }

        // GET: api/TruckDrivers/5
        [ResponseType(typeof(TruckDriver))]
        public IHttpActionResult GetTruckDriver(int id)
        {
            TruckDriver truckDriver = db.TruckDrivers.Find(id);
            if (truckDriver == null)
            {
                return NotFound();
            }

            return Ok(truckDriver);
        }

        // PUT: api/TruckDrivers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTruckDriver(int id, TruckDriver truckDriver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != truckDriver.TruckDriverId)
            {
                return BadRequest();
            }

            db.Entry(truckDriver).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TruckDriverExists(id))
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

        // POST: api/TruckDrivers
        [ResponseType(typeof(TruckDriver))]
        public IHttpActionResult PostTruckDriver(TruckDriver truckDriver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TruckDrivers.Add(truckDriver);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = truckDriver.TruckDriverId }, truckDriver);
        }

        // DELETE: api/TruckDrivers/5
        [ResponseType(typeof(TruckDriver))]
        public IHttpActionResult DeleteTruckDriver(int id)
        {
            TruckDriver truckDriver = db.TruckDrivers.Find(id);
            if (truckDriver == null)
            {
                return NotFound();
            }

            db.TruckDrivers.Remove(truckDriver);
            db.SaveChanges();

            return Ok(truckDriver);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TruckDriverExists(int id)
        {
            return db.TruckDrivers.Count(e => e.TruckDriverId == id) > 0;
        }
    }
}