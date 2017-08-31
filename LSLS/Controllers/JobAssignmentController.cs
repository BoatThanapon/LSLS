using System.Net;
using System.Web.Mvc;
using LSLS.Models;
using LSLS.Repository.Abstract;
using LSLS.ViewModels;

namespace LSLS.Controllers
{
    public class JobAssignmentController : Controller
    {
        private readonly IJobAssignmentRepository _jobAssignmentRepository;

        public JobAssignmentController(IJobAssignmentRepository jobAssignmentRepository)
        {
            _jobAssignmentRepository = jobAssignmentRepository;
        }

        // GET: JobAssignment
        [HttpGet]
        public ActionResult ListAllJobAssignments()
        {
            return View("ListAllJobAssignments", _jobAssignmentRepository.GetAllJobAssingments());
        }

        // GET: JobAssignment/FormCreateJobAssignment
        [HttpGet]
        public ActionResult FromCreateJobAssignment(int? jobAssignmentId)
        {
            FormJobAssignmentViewModel fromCreate = _jobAssignmentRepository.FromJobAssingment(jobAssignmentId);
            if (fromCreate == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View("FromCreateJobAssignment", fromCreate);
        }
        
        // Get: JobAssignment/FormEditJobAssignment/jobAssignmentId
        [HttpGet]
        public ActionResult FromEditJobAssignment(int? jobAssignmentId)
        {
            if (jobAssignmentId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            FormJobAssignmentViewModel fromCreate = _jobAssignmentRepository.FromJobAssingment(jobAssignmentId);

            return View("FromCreateJobAssignment", fromCreate);
        }

        // Get: JobAssignment/DetailsJobAssignment/jobAssignmentId
        [HttpGet]
        public ActionResult DetailsJobAssignment(int? jobAssignmentId)
        {
            if (jobAssignmentId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            FormJobAssignmentViewModel fromDetails = _jobAssignmentRepository.FromJobAssingment(jobAssignmentId);

            return View("DetailsJobAssignment", fromDetails);

        }


        // POST: JobAssignment/SaveJobAssignment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveJobAssignment(JobAssingment jobAssingment)
        {
            if (!ModelState.IsValid)
                return View(jobAssingment);

            if (jobAssingment.JobAssignmentId == 0)
            {
                var jobAssignmentAdd = _jobAssignmentRepository.AddJobAssignment(jobAssingment);
                if (jobAssignmentAdd.Equals(true))
                    return RedirectToAction("ListAllJobAssignments");
            }
            else
            {
                var jobAssignmentEdit = _jobAssignmentRepository.UpdateJobAssignment(jobAssingment);
                if (jobAssignmentEdit.Equals(true))
                    return RedirectToAction("ListAllJobAssignments");
            }

            return View(jobAssingment);
        }

        // GET: JobAsignmet/DeleteJobAssignment/jobAssignmentId
        [HttpGet]
        public ActionResult DeleteJobAssignment(int? jobAssignmentId)
        {
            if (jobAssignmentId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var jobAssingment = _jobAssignmentRepository.GetJobAssingmentById(jobAssignmentId);
            if (jobAssingment == null)
                return HttpNotFound();

            return View("DeleteJobAssignment", jobAssingment);
        }

        // POST: Staffs/DeleteJobAssignment/jobAssignmentId
        [HttpPost]
        [ActionName("DeleteJobAssignment")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteJobAssignmentConfirmed(int? jobAssignmentId)
        {
            // ReSharper disable once SuggestVarOrType_BuiltInTypes
            bool jobAssignment = _jobAssignmentRepository.DeleteJobAssignment(jobAssignmentId);
            if (jobAssignment.Equals(true))
                return RedirectToAction("ListAllJobAssignments");

            return View("DeleteJobAssignment");
        }

    }
}