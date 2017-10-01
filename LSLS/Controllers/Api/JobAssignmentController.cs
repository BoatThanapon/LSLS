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
    [RoutePrefix("api/JobAssignment")]

    public class JobAssignmentController : ApiController
    {
        private readonly IJobAssignmentRepository _jobAssignmentRepository = new JobAssignmentRepository(new ApplicationDbContext());

        // GET: api/JobAssignment/ListJobAssignment
        [HttpGet]
        [ResponseType(typeof(JobAssignment))]
        [Route("GetListJobAssignment")]
        public IHttpActionResult ListJobAssignmentByTruckDriverId(int truckDriverId)
        {
            var listJobAssignmentsByTruckDriverId = _jobAssignmentRepository.GetListJobByTruckDriverId(truckDriverId);
            if (listJobAssignmentsByTruckDriverId == null)
            {
                return Ok();
            }
            return Ok(listJobAssignmentsByTruckDriverId);
        }

        /*
        // GET: api/JobAssignment/GetJobInfo
        [HttpGet]
        [ResponseType(typeof(JobAssignment))]
        [Route("GetJobInfo")]
        public IHttpActionResult GetJobAssingmentInfoByJobAssignmentId(int jobAssignmentId)
        {
            var findJobAssignmentsByJobAssignmentId = _jobAssignmentRepository.GetJobAssingmentInfoByJobAssignmentId(jobAssignmentId);
            if (findJobAssignmentsByJobAssignmentId == null)
            {
                return NotFound();
            }

            return Ok(findJobAssignmentsByJobAssignmentId);
        }

        */
    }
}