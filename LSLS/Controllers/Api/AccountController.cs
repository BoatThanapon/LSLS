using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LSLS.Models;
using Newtonsoft.Json;

namespace LSLS.Controllers.Api
{
    public class AccountController : ApiController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: api/Account/CheckLogin
        [HttpGet]
        public IHttpActionResult CheckLogin(string username, string password)
        {
            var truckDriverInDb = _db.TruckDrivers
                .FirstOrDefault(t => t.TruckDriverUsername.Equals(username) && t.TruckDriverPassword.Equals(password));

            if (truckDriverInDb == null)
            {
                return Ok(new {Status = 0});
            }

            return Ok(new { Status = 1, truckDriverInDb.TruckDriverId});          
        }
     
    }
}
