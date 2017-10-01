using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LSLS.Models;
using LSLS.Repository;
using LSLS.Repository.Abstract;
using Newtonsoft.Json;

namespace LSLS.Controllers.Api
{
    //Completed
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly IAuthProvider _authProvider = new AuthProvider(new ApplicationDbContext());

        // GET: api/Account/CheckTruckDriverLogin
        [HttpGet]
        [AllowAnonymous]
        [Route("CheckTruckDriverLogin")]
        public IHttpActionResult CheckTruckDriverLogin(string username, string password)
        {
            var checkTruckDriverLogin = _authProvider.AuthenticateTruckDriver(username, password);
            if (checkTruckDriverLogin == null)
            {
                return Ok(new {Status = 0});
            }

            return Ok(new { Status = 1, checkTruckDriverLogin.TruckDriverId});          
        }

    }
}
