using System.Net;
using System.Web.Mvc;
using LSLS.Models;
using LSLS.Repository.Abstract;
using LSLS.ViewModels;

namespace LSLS.Controllers
{
    [Authorize]
    public class TransportationInfController : Controller
    {
        private readonly ITransportationInfRepository _transportationInfRepository;
        private readonly IJobAssignmentRepository _jobAssignmentRepository;
        private readonly ITruckDriverRepository _truckDriverRepository;


        public TransportationInfController(ITransportationInfRepository transportationInfRepository, IJobAssignmentRepository jobAssignmentRepository, ITruckDriverRepository truckDriverRepository)
        {
            _transportationInfRepository = transportationInfRepository;
            _jobAssignmentRepository = jobAssignmentRepository;
            _truckDriverRepository = truckDriverRepository;
        }

        // GET: TransportationInfs/ListAllTransportationInfs
        public ViewResult ListAllTransportationInfs()
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

        //เปลี่ยนเป็น Form
        //GET: TransportationInfs/FormCreateTransportationInf
        [HttpGet]
        public ViewResult FormCreateTransportationInf()
        {
            return View("FormCreateTransportationInf");
        }

        //POST: TransportationInfs/FormCreateTransportationInf
        [HttpPost]
        [ActionName("FormCreateTransportationInf")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTransportationInf(TransportationInf transportationInf)
        {
            if (ModelState.IsValid)
            {
                bool transportationInfAdd = _transportationInfRepository.AddTransportationInf(transportationInf);
                if (transportationInfAdd.Equals(true))
                    return RedirectToAction("ListAllTransportationInfs");
            }
            return View("FormCreateTransportationInf", transportationInf);
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
                return View("FormEditTransportationInf", transportationInf);

            bool transportationInfEdit = _transportationInfRepository.UpdateTransportationInf(transportationInf);
            if (transportationInfEdit.Equals(true))
                return RedirectToAction("ListAllTransportationInfs");

            return View("FormEditTransportationInf", transportationInf);
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
            bool transportationInfInDb = _transportationInfRepository.DeleteTransportationInf(shippingId);
            if (transportationInfInDb.Equals(true))
                return RedirectToAction("ListAllTransportationInfs");

            return View("DeleteTransportationInf");
        }

        //เปลี่ยนเป็น Form
        // GET: JobAssignment/FormCreateJobAssignment
        [HttpGet]
        public ActionResult FormCreateJobAssignment(int? shippingId)
        {
            if (shippingId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var findTransportationInf = _transportationInfRepository.GetTransportationInfById(shippingId);
            if (findTransportationInf == null)
            {
                return HttpNotFound();
            }

            var formJopViewModel = new FormJobAssignmentViewModel
            {
                JobAssignment = new JobAssignment
                {
                    ShippingId = findTransportationInf.ShippingId,
                    StartingPointJob = findTransportationInf.StartingPoint,
                    DestinationJob = findTransportationInf.Destination,
                    JobAssignmentDate = findTransportationInf.DateOfTransportation,
                },
                TruckDrivers = _truckDriverRepository.GetAllTruckDrivers(),
            };

            return View("FormCreateJobAssignment", formJopViewModel);
        }

        //เปลี่ยนเป็น Form
        // POST: JobAssignment/FormCreateJobAssignment
        [HttpPost]
        [ActionName("FormCreateJobAssignment")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateJobAssignment(FormJobAssignmentViewModel jobAssignmentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("FormCreateJobAssignment", jobAssignmentViewModel);

            }
            var createJob = _jobAssignmentRepository.AddJobAssignment(jobAssignmentViewModel);
            if (createJob.Equals(true))
            {
                return RedirectToAction("ListAllTransportationInfs");
            }
       
            return View("FormCreateJobAssignment", jobAssignmentViewModel);
        }
        
    }

}