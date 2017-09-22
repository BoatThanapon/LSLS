using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using LSLS.Models;

namespace LSLS.Controllers.Api
{
    public class AccountController : ApiController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: api/Account
        [AllowAnonymous]
        public IHttpActionResult CheckLogin(string username, string password)
        {
            var truckDriverInDb = _db.TruckDrivers
                .FirstOrDefault(t => t.TruckDriverUsername.Equals(username) && t.TruckDriverPassword.Equals(password));

            if (truckDriverInDb == null)
            {
                return Ok(false);
            }

            return Ok(truckDriverInDb);
        }
       
    }
}
