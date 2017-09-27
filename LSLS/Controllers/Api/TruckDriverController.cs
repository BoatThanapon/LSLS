using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using LSLS.Models;

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
        public IHttpActionResult GetTruckDriver(int truckDriverId)
        {
            TruckDriver truckDriver = _db.TruckDrivers.Find(truckDriverId);
            if (truckDriver == null)
            {
                return NotFound();
                
            }

            return Ok(truckDriver);
            
        }        

    }
}
