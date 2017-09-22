using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public IHttpActionResult PutTruckLocation(TruckLocation truckLocation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var truckLocationIndDb = _db.TruckLocations
                .FirstOrDefault(t => t.TruckLocationId == truckLocation.TruckLocationId);

            _db.Entry(truckLocationIndDb).State = EntityState.Modified;

            _db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}