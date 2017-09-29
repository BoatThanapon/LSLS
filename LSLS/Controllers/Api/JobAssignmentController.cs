using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using LSLS.Models;
using LSLS.Repository;
using LSLS.Repository.Abstract;

namespace LSLS.Controllers.Api
{
    //Completed
    public class JobAssignmentController : ApiController
    {
        private readonly IJobAssignmentRepository _jobAssignmentRepository = new JobAssignmentRepository(new ApplicationDbContext());

        // GET: api/JobAssignment/5
        [HttpGet]
        [ResponseType(typeof(JobAssignment))]
        public IHttpActionResult ListJobAssignmentByTruckDriverId(int truckDriverId)
        {
            var listJobAssignmentsByTruckDriverId = _jobAssignmentRepository.GetListJobByTruckDriverId(truckDriverId);
            
            return Ok(listJobAssignmentsByTruckDriverId);
        }

        // GET: api/JobAssignment/5
        [HttpGet]
        [ResponseType(typeof(JobAssignment))]
        public IHttpActionResult GetJobAssignmentByJobAssignmentId(int jobAssignmentId)
        {
            var findJobAssignmentsByJobAssignmentId = _jobAssignmentRepository.GetJobAssignmentById(jobAssignmentId);
            if (findJobAssignmentsByJobAssignmentId == null)
            {
                return NotFound();
            }

            return Ok(findJobAssignmentsByJobAssignmentId);
        }


    }
}