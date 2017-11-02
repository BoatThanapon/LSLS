using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using System.Xml.Linq;
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
        static string baseUri =
            "http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false";

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
            string requestUri = string.Format(baseUri, truckLocation.Latitude, truckLocation.Longitude);

            using (var wc = new WebClient())
            {
                string result = wc.DownloadString(requestUri);
                var xmlElm = XElement.Parse(result);
                var status = (from elm in xmlElm.Descendants()
                    where
                        elm.Name == "status"
                    select elm).FirstOrDefault();
                if (status.Value.ToLower() == "ok")
                {
                    var res = (from elm in xmlElm.Descendants()
                        where
                            elm.Name == "formatted_address"
                        select elm).FirstOrDefault();

                    findTruckLocationByTruckDriverId.TruckCurrentAddress = res.Value.ToString();
                    if (res != null)
                        requestUri = res.Value;
                }
            }

            findTruckLocationByTruckDriverId.Latitude = truckLocation.Latitude;
            findTruckLocationByTruckDriverId.Longitude = truckLocation.Longitude;

            //



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