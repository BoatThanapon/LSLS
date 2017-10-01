using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using LSLS.Models;
using LSLS.Repository;
using LSLS.Repository.Abstract;
using Microsoft.AspNet.Identity;

namespace LSLS.Controllers.Api
{
    //Complete
    [RoutePrefix("api/TruckDriver")]
    public class TruckDriverController : ApiController
    {
        private readonly ITruckDriverRepository _truckDriverRepository = new TruckDriverRepository(new ApplicationDbContext());

        // GET: api/TruckDriver/GetTruckDriverInfo
        [HttpGet]       
        [ResponseType(typeof(TruckDriver))]
        [Route("GetTruckDriverInfo")]
        public IHttpActionResult GetTruckDriverInfo(int truckDriverId)
        {
            var findTruckDriverByTruckDriverId = _truckDriverRepository.GetTruckDriverById(truckDriverId);
            if (findTruckDriverByTruckDriverId == null)
            {
                return NotFound();
                
            }

            return Ok(findTruckDriverByTruckDriverId);           
        }        

    }
}
