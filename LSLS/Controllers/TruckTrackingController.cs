using System.Web.Mvc;
using LSLS.Repository.Abstract;

namespace LSLS.Controllers
{
    public class TruckTrackingController : Controller
    {
        private readonly ITruckLocationRepository _truckLocationRepository;

        public TruckTrackingController(ITruckLocationRepository truckLocationRepository)
        {
            _truckLocationRepository = truckLocationRepository;
        }

        // GET: TruckTracking       
        public ActionResult TruckTracking()
        {
            return View("TruckTracking", _truckLocationRepository.GetAllTruckLocations());
        }

        // GET: TruckTracking/GetAllTruckLocations
        [HttpGet]
        public JsonResult GetAllTruckLocations()
        {
            var allTruckLocation = _truckLocationRepository.GetAllTruckLocations();
            return Json(allTruckLocation, JsonRequestBehavior.AllowGet);
        }

        // Get: TruckTracking/SearchTruckId
        [HttpPost]
        public ActionResult SearchTruckId(string truckId)
        {
            var resultSearchTruckId = _truckLocationRepository.GetTruckLocationByTruckId(truckId);

            if (resultSearchTruckId != null)
                return View("SearchTruckId", resultSearchTruckId);

            ModelState.AddModelError("", "");

            return View("TruckTracking", _truckLocationRepository.GetAllTruckLocations());
        }

        /*
        [HttpGet]
        [ActionName("SearchTruckId")]
        public JsonResult JSearchTruckId(string truckId)
        {
            var resultSearchTruckId = _truckLocationRepository.GetTruckLocationByTruckId(truckId);

            return Json(resultSearchTruckId, JsonRequestBehavior.AllowGet);
        }
        */
    }
}