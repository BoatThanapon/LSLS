using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using LSLS.Models;

namespace LSLS.Controllers.Api
{
    public class TruckLocationController : ApiController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: api/TruckLocation
        public IQueryable<TruckLocation> GetAllTruckLocation()
        {
            return _db.TruckLocations;
        }

        // PUT: api/TruckLocation/5
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateTruckLocation(int truckDriverId,TruckLocation truckLocation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var truckLocationIndDb = _db.TruckLocations.FirstOrDefault(t => t.TruckDriverId == truckDriverId);

            if (truckLocationIndDb != null)
            {
                truckLocationIndDb.TruckCurrentTime = DateTime.Now;

                _db.Entry(truckLocationIndDb).State = EntityState.Modified;
            }

            _db.SaveChanges();

            return Ok();
        }
        
    }
    
}