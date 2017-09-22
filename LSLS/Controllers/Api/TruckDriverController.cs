using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using LSLS.Models;
using LSLS.Repository.Abstract;

namespace LSLS.Controllers.Api
{
    public class TruckDriverController : ApiController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: api/TruckDriver
        public IQueryable<TruckDriver> GetAllTruckDrivers()
        {
            return _db.TruckDrivers;
        }

        // GET: api/TruckDriver/5
        [ResponseType(typeof(TruckDriver))]
        public IHttpActionResult GetTruckDriver(int id)
        {
            TruckDriver truckDriver = _db.TruckDrivers.Find(id);
            if (truckDriver == null)
            {
                return NotFound();
            }

            return Ok(truckDriver);
        }

    }
}
