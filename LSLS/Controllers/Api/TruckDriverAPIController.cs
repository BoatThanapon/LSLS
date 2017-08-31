using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Antlr.Runtime.Tree;
using LSLS.Models;
using LSLS.Repository.Abstract;
using LSLS.Service;

namespace LSLS.Controllers.Api
{
    public class TruckDriverApiController : ApiController
    {
        private readonly ITruckDriverService _truckDriverService;
        private readonly ITruckDriverRepository _truckDriverRepository;
        private readonly ApplicationDbContext _context;

        public TruckDriverApiController(ITruckDriverService truckDriverService, ITruckDriverRepository truckDriverRepository, ApplicationDbContext context)
        {
            _truckDriverService = truckDriverService;
            _truckDriverRepository = truckDriverRepository;
            _context = context;
        }

        // GET /api/TruckDriverApi/CheckTruckDriverLogin
        [HttpGet]
        public IHttpActionResult CheckTruckDriverLogin(TruckDriver truckDriver)
        {
            if (truckDriver == null)
                return null;

            var checkUser = _truckDriverService.CheckLoginTruckDriver(truckDriver);
            if (checkUser == null)
            {
                return null;
            }

            return Ok(checkUser);
        }

        // GET /TruckDriverApi/GetTruckDriver/truckDriverId
        [HttpGet]
        public IHttpActionResult GetTruckDriver(int? truckDriverId)
        {
            if (truckDriverId == null)
                return null;

            var checkUser = _truckDriverRepository.GetTruckDriverById(truckDriverId);
            if (checkUser == null)
            {
                return NotFound();
            }

            return Ok(checkUser);
        }

        // GET /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.TruckDrivers.SingleOrDefault(c => c.TruckDriverId == id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

    }
}
