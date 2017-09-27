using System.Net;
using System.Web.Mvc;
using LSLS.Models;
using LSLS.Repository.Abstract;

// ReSharper disable SuggestVarOrType_SimpleTypes
// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace LSLS.Controllers
{
    [Authorize]
    public class TruckDriverController : Controller
    {
        private readonly ITruckDriverRepository _truckDriverRepository;

        public TruckDriverController(ITruckDriverRepository truckDriverRepository)
        {
            _truckDriverRepository = truckDriverRepository;
        }

        // GET: TruckDriver/ListAllTruckDrivers
        [HttpGet]
        public ActionResult ListAllTruckDrivers()
        {
            return View("ListAllTruckDrivers", _truckDriverRepository.GetAllTruckDrivers());
        }

        // GET: TruckDriver/FormCreateTruckDriver
        [HttpGet]
        public ActionResult FormCreateTruckDriver()
        {
            return View("FormCreateTruckDriver");
        }

        // GET: TruckDriver/DetailsTruckDriver/truckdriverId
        [HttpGet]
        public ActionResult DetailsTruckDriver(int? truckdriverId)
        {
            if (truckdriverId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var truckDriver = _truckDriverRepository.GetTruckDriverById(truckdriverId);
            if (truckDriver == null)
                return HttpNotFound();

            return View("DetailsTruckDriver", truckDriver);
        }

        // GET: TruckDriver/FromEditTruckDriver/truckdriverId
        [HttpGet]
        public ActionResult FormEditTruckDriver(int? truckdriverId)
        {
            if (truckdriverId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var truckDriver = _truckDriverRepository.GetTruckDriverById(truckdriverId);
            if (truckDriver == null)
                return HttpNotFound();

            return View("FormEditTruckDriver", truckDriver);
        }

        // POST: TruckDriver/SaveTruckDriver
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveTruckDriver(TruckDriver truckDriver)
        {
            if (!ModelState.IsValid)
                return View(truckDriver);

            if (truckDriver.TruckDriverId == 0)
            {
                var truckdriverAdd = _truckDriverRepository.AddTruckDriver(truckDriver);
                if (truckdriverAdd.Equals(true))
                    return RedirectToAction("ListAllTruckDrivers");
            }
            else
            {
                var truckdriverEdit = _truckDriverRepository.UpdateTruckDriver(truckDriver);
                if (truckdriverEdit.Equals(true))
                    return RedirectToAction("ListAllTruckDrivers");
            }

            return View(truckDriver);
        }

        // GET:TruckDriver/DeleteTruckDriver/truckdriverId
        [HttpGet]
        public ActionResult DeleteTruckDriver(int? truckdriverId)
        {
            if (truckdriverId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var truckDriver = _truckDriverRepository.GetTruckDriverById(truckdriverId);
            if (truckDriver == null)
                return HttpNotFound();

            return View("DeleteTruckDriver", truckDriver);
        }

        //POST:TruckDriver/DeleteTruckDriver/truckdriverId
        [HttpPost]
        [ActionName("DeleteTruckDriver")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTruckDriverConfirmed(int? truckdriverId)
        {
            if (truckdriverId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var truckDriver = _truckDriverRepository.DeleteTruckDriver(truckdriverId);
            if (truckDriver.Equals(true))
                return RedirectToAction("ListAllTruckDrivers");

            return View("DeleteTruckDriver");
        }
    }
}