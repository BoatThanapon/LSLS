using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using LSLS.Models;

namespace LSLS.Controllers.Api
{
    public class JobAssignmentController : ApiController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: api/JobAssignment
        public IQueryable<JobAssignment> GetJobAssignments()
        {
            return _db.JobAssignments;
        }

        // GET: api/JobAssignment/5
        [ResponseType(typeof(JobAssignment))]
        public IHttpActionResult GetJobAssignment(int truckdriverId)
        {
            List<JobAssignment> listJobAssignmentsByTruckDriverId = _db.JobAssignments.Where(j => j.TruckDriverId == truckdriverId).ToList();

            return Ok(listJobAssignmentsByTruckDriverId);
        }

        // PUT: api/JobAssignment/5
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateJobAssignment(int truckDriverId ,JobAssignment jobAssignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var jobAssignmentInDb = _db.JobAssignments.SingleOrDefault(c => c.TruckDriverId == truckDriverId);

            _db.Entry(jobAssignmentInDb).State = EntityState.Modified;

            var shipping = _db.TransportationInfs.Find(jobAssignment.ShippingId);

            if (shipping != null)
                shipping.StatusOfTransportation = jobAssignment.JobAssignmentStatus;

            _db.SaveChanges();

            return Ok();
        }



        private bool JobAssignmentExists(int id)
        {
            return _db.JobAssignments.Count(e => e.JobAssignmentId == id) > 0;
        }

    }
}