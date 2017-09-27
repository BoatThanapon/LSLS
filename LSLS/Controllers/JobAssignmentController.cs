using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using LSLS.Models;
using LSLS.Repository.Abstract;
using LSLS.ViewModels;

namespace LSLS.Controllers
{
    [Authorize]
    public class JobAssignmentController : Controller
    {
        private readonly IJobAssignmentRepository _jobAssignmentRepository;
        private readonly ITruckDriverRepository _truckDriverRepository;

        public JobAssignmentController(IJobAssignmentRepository jobAssignmentRepository, ITruckDriverRepository truckDriverRepository)
        {
            _jobAssignmentRepository = jobAssignmentRepository;
            _truckDriverRepository = truckDriverRepository;
        }

        // GET: JobAssignment
        [HttpGet]
        public ActionResult ListAllJobAssignments()
        {
            IEnumerable<JobAssignment> listJobAssingments = _jobAssignmentRepository.GetAllJobAssignments();

            return View("ListAllJobAssignments", listJobAssingments);
        }

        // GET: JobAssignment/FormEditJobAssignment/jobAssignmentId
        [HttpGet]
        public ActionResult FromEditJobAssignment(int? jobAssignmentId)
        {
            if (jobAssignmentId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var findJob = _jobAssignmentRepository.GetJobAssignmentById(jobAssignmentId);
            if (findJob == null)
            {
                return HttpNotFound();
            }

            var jobViewModel = new FormJobAssignmentViewModel
            {
                JobAssignment = findJob,
                TruckDrivers = _truckDriverRepository.GetAllTruckDrivers(),
            };

            return View("FromEditJobAssignment", jobViewModel);
        }

        // POST: JobAssignment/FromEditJobAssignment
        [HttpPost]
        [ActionName("FromEditJobAssignment")]
        [ValidateAntiForgeryToken]
        public ActionResult EditJobAssignment(FormJobAssignmentViewModel jobAssignmentViewModel)
        {
            if (!ModelState.IsValid)
                return View(jobAssignmentViewModel);

            var editJob = _jobAssignmentRepository.UpdateJobAssignment(jobAssignmentViewModel);
            if (editJob.Equals(true))
            {
                return RedirectToAction("ListAllJobAssignments");
            }

            return View(jobAssignmentViewModel);
        }


        // GET: JobAssignment/DetailsJobAssignment/jobAssignmentId
        [HttpGet]
        public ActionResult DetailsJobAssignment(int? jobAssignmentId)
        {
            if (jobAssignmentId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            var findJob = _jobAssignmentRepository.GetJobAssignmentById(jobAssignmentId);
            if (findJob == null)
            {
                return HttpNotFound();
            }

            var jobViewModel = new FormJobAssignmentViewModel
            {
                JobAssignment = findJob,
                TruckDrivers = _truckDriverRepository.GetAllTruckDrivers(),
            };

            return View("DetailsJobAssignment", jobViewModel);
        }

        // GET: JobAssignment/DeleteJobAssignment/jobAssignmentId
        [HttpGet]
        public ActionResult DeleteJobAssignment(int? jobAssignmentId)
        {
            if (jobAssignmentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var findJob = _jobAssignmentRepository.GetJobAssignmentById(jobAssignmentId);
            if (findJob == null)
            {
                return HttpNotFound();
            }

            return View("DeleteJobAssignment", findJob);
        }

        // POST: JobAssignment/DeleteAssignment
        [HttpPost]
        [ActionName("DeleteJobAssignment")]
        public ActionResult DeleteJobAssignmentConfirmed(int? jobAssignmentId)
        {
            if (jobAssignmentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var deleteJob = _jobAssignmentRepository.DeleteJobAssignment(jobAssignmentId);
            if (deleteJob.Equals(true))
            {
                return RedirectToAction("ListAllJobAssignments");
            }

            return View();
        }       
    }
}