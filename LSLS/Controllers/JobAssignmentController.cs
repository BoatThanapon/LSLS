using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
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
            IEnumerable<JobAssingment> listJobAssingments = _jobAssignmentRepository.GetAllJobAssingments();

            return View("ListAllJobAssignments", listJobAssingments);
        }

        // GET: JobAssignment/FormEditJobAssignment/jobAssignmentId
        [HttpGet]
        public ActionResult FromEditJobAssignment(int? jobAssignmentId)
        {
            if (jobAssignmentId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            FormJobAssignmentViewModel fromEdit = _jobAssignmentRepository.FromJobAssingment(jobAssignmentId);

            return View("FromEditJobAssignment", fromEdit);
        }

        // POST: JobAssignment/FromEditJobAssignment
        [HttpPost]
        [ActionName("FromEditJobAssignment")]
        [ValidateAntiForgeryToken]
        public ActionResult EditJobAssignment(JobAssingment jobAssingment)
        {
            if (!ModelState.IsValid)
                return View(jobAssingment);

            var editJob = _jobAssignmentRepository.UpdateJobAssignment(jobAssingment);
            if (editJob.Equals(true))
            {
                return RedirectToAction("ListAllJobAssignments");
            }

            return View(jobAssingment);
        }


        // GET: JobAssignment/DetailsJobAssignment/jobAssignmentId
        [HttpGet]
        public ActionResult DetailsJobAssignment(int? jobAssignmentId)
        {
            if (jobAssignmentId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            FormJobAssignmentViewModel fromDetails = _jobAssignmentRepository.FromJobAssingment(jobAssignmentId);

            return View("DetailsJobAssignment", fromDetails);

        }

        // GET: JobAssignment/DeleteJobAssignment/jobAssignmentId
        [HttpGet]
        public ActionResult DeleteJobAssignment(int? jobAssignmentId)
        {
            if (jobAssignmentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var findJob = _jobAssignmentRepository.GetJobAssingmentById(jobAssignmentId);
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

            return View(deleteJob);
        }       
    }
}