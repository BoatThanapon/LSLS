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
    public class TransportationInfController : ApiController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: api/TransportationInf
        public IQueryable<TransportationInf> GetTransportationInfs()
        {
            return _db.TransportationInfs;
        }

        // GET: api/TransportationInf/5
        [ResponseType(typeof(TransportationInf))]
        public IHttpActionResult GetTransportationInf(int id)
        {
            TransportationInf transportationInf = _db.TransportationInfs.Find(id);
            if (transportationInf == null)
            {
                return NotFound();
            }

            return Ok(transportationInf);
        }

        // PUT: api/TransportationInf/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTransportationInf(int id, TransportationInf transportationInf)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transportationInf.ShippingId)
            {
                return BadRequest();
            }

            _db.Entry(transportationInf).State = EntityState.Modified;

            var jobAssignment = _db.JobAssignments.Find(transportationInf.ShippingId);

            if (jobAssignment != null)
            {
                jobAssignment.JobAssignmentStatus = transportationInf.StatusOfTransportation;
                _db.Entry(jobAssignment).State = EntityState.Modified;
            }
                                     
            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransportationInfExists(id))
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

        private bool TransportationInfExists(int id)
        {
            return _db.TransportationInfs.Count(e => e.ShippingId == id) > 0;
        }
    }
}