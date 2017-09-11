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
    public class TransportationInfController : Controller
    {
        private readonly ITransportationInfRepository _transportationInfRepository;
        private readonly IJobAssignmentRepository _jobAssignmentRepository;


        public TransportationInfController(ITransportationInfRepository transportationInfRepository, IJobAssignmentRepository jobAssignmentRepository)
        {
            _transportationInfRepository = transportationInfRepository;
            _jobAssignmentRepository = jobAssignmentRepository;
        }

        // GET: TransportationInfs/ListAllTransportationInfs
        public ActionResult ListAllTransportationInfs()
        {
            var listTranInf = _transportationInfRepository.GetAllTransportationInfs();
            return View("ListAllTransportationInfs", listTranInf);
        }

        //GET: TransportationInfs/DetailsTransportationInf/shippingId
        [HttpGet]
        public ActionResult DetailsTransportationInf(int? shippingId)
        {
            if (shippingId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var detailsTrans = _transportationInfRepository.GetTransportationInfById(shippingId);
            if (detailsTrans == null)
                return HttpNotFound();

            return View("DetailsTransportationInf", detailsTrans);
        }

        //GET: TransportationInfs/FormCreateTransportationInf
        [HttpGet]
        public ActionResult FormCreateTransportationInf()
        {
            return View("FormCreateTransportationInf");
        }

        //POST: TransportationInfs/FormCreateTransportationInf
        [HttpPost]
        [ActionName("FormCreateTransportationInf")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTransportationInf(TransportationInf transportationInf)
        {
            if (!ModelState.IsValid)
                return View(transportationInf);

            bool transportationInfAdd = _transportationInfRepository.AddTransportationInf(transportationInf);
            if (transportationInfAdd.Equals(true))
                return RedirectToAction("ListAllTransportationInfs");

            return View(transportationInf);
        }

        //GET TransportationInfs/FormEditTransportationInf/shippingId
        [HttpGet]
        public ActionResult FormEditTransportationInf(int? shippingId)
        {
            if (shippingId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            TransportationInf transportationInfInDb = _transportationInfRepository.GetTransportationInfById(shippingId);
            if (transportationInfInDb == null)
                return HttpNotFound();

            return View("FormEditTransportationInf", transportationInfInDb);
        }

        //POST: TransportationInfs/FormEditTransportationInf
        [HttpPost]
        [ActionName("FormEditTransportationInf")]
        [ValidateAntiForgeryToken]
        public ActionResult EditTransportationInf(TransportationInf transportationInf)
        {
            if (!ModelState.IsValid)
                return View(transportationInf);

            bool transportationInfEdit = _transportationInfRepository.UpdateTransportationInf(transportationInf);
            if (transportationInfEdit.Equals(true))
                return RedirectToAction("ListAllTransportationInfs");

            return View(transportationInf);
        }

        // GET: TransportationInfs/DeleteTransportationInf/shippingId
        [HttpGet]
        public ActionResult DeleteTransportationInf(int? shippingId)
        {
            if (shippingId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            TransportationInf transportationInfInDb = _transportationInfRepository.GetTransportationInfById(shippingId);
            if (transportationInfInDb == null)
                return HttpNotFound();

            return View("DeleteTransportationInf", transportationInfInDb);
        }

        //POST: TransportationInfs/DeleteTransportationInf/shippingId
        [HttpPost]
        [ActionName("DeleteTransportationInf")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTransportationInfConfirmed(int? shippingId)
        {
            if (shippingId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            bool transportationInfInDb = _transportationInfRepository.DeleteTransportationInf(shippingId);
            if (transportationInfInDb.Equals(true))
                return RedirectToAction("ListAllTransportationInfs");

            return View("DeleteTransportationInf");
        }


        // GET: JobAssignment/FormCreateJobAssignment
        [HttpGet]
        public ActionResult FromCreateJobAssignment(int? shippingId)
        {
            FormJobAssignmentViewModel fromCreate = _transportationInfRepository.FromJobAssingment(shippingId);
            if (fromCreate == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View("FromCreateJobAssignment", fromCreate);
        }

        // POST: JobAssignment/FromCreateJobAssignment
        [HttpPost]
        [ActionName("FromCreateJobAssignment")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateJobAssignment(JobAssingment jobAssingment)
        {
            if (!ModelState.IsValid)
                return View(jobAssingment);

            var createJob = _jobAssignmentRepository.AddJobAssignment(jobAssingment);
            if (createJob.Equals(true))
            {
                return RedirectToAction("ListAllTransportationInfs");
            }

            return View(jobAssingment);
        }

    }
}