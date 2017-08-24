using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LSLS.Repository;

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
            return View("TruckTracking", _truckLocationRepository.GetAllTruckLocation());
        }
    }
}