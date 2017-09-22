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
        public IHttpActionResult GetJobAssignment(int id)
        {
            JobAssignment jobAssignment = _db.JobAssignments.Find(id);
            if (jobAssignment == null)
            {
                return NotFound();
            }

            return Ok(jobAssignment);
        }

        // PUT: api/JobAssignment/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJobAssignment(int id, JobAssignment jobAssignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jobAssignment.JobAssignmentId)
            {
                return BadRequest();
            }

            _db.Entry(jobAssignment).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobAssignmentExists(id))
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

        private bool JobAssignmentExists(int id)
        {
            return _db.JobAssignments.Count(e => e.JobAssignmentId == id) > 0;
        }
    }
}