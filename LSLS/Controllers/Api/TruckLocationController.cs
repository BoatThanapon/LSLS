using System;
using System.Data.Entity;
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
    [RoutePrefix("api/TruckLocation")]
    public class TruckLocationController : ApiController
    {
        private readonly ITruckLocationRepository _truckLocationRepository = new TruckLocationRepository(new ApplicationDbContext());

        // PUT: api/TruckLocation/UpdateTruckLocation
        [HttpPut]
        [ResponseType(typeof(void))]
        [Route("UpdateTruckLocation")]
        public IHttpActionResult UpdateTruckLocation(int truckDriverId,TruckLocation truckLocation)
        {
            if (truckLocation == null || truckLocation.TruckDriverId != truckDriverId)
            {
                return BadRequest();
            }
            var findTruckLocationByTruckDriverId =
                _truckLocationRepository.GetTruckLocationByTruckDriverId(truckDriverId);

            if (findTruckLocationByTruckDriverId == null)
            {
                return NotFound();
            }

            findTruckLocationByTruckDriverId.Latitude = truckLocation.Latitude;
            findTruckLocationByTruckDriverId.Longitude = truckLocation.Longitude;
            findTruckLocationByTruckDriverId.TruckCurrentAddress = truckLocation.TruckCurrentAddress;
            findTruckLocationByTruckDriverId.TruckCurrentTime = DateTime.Now;
            
            var updateTruckLocation = _truckLocationRepository.UpdateTruckLocation(findTruckLocationByTruckDriverId);
            if (updateTruckLocation.Equals(false))
            {
                return Ok(false);
            }

            return Ok(true);
        }
        
    }
    
}