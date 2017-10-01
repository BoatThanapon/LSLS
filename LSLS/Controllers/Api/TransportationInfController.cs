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
using LSLS.Repository;
using LSLS.Repository.Abstract;

namespace LSLS.Controllers.Api
{
    [RoutePrefix("api/TransportationInf")]
    public class TransportationInfController : ApiController
    {
        private readonly ITransportationInfRepository _transportationInfRepository = new TransportationInfRepository(new ApplicationDbContext());

        // GET: api/TransportationInf/GetTransportationInf
        [HttpGet]
        [ResponseType(typeof(TransportationInf))]
        [Route("GetTransportationInf")]
        public IHttpActionResult GetTransportationInfByShippingId(int shippingId)
        {
            var findTransportationInf = _transportationInfRepository.GetTransportationInfById(shippingId);
            if (findTransportationInf == null)
            {
                return Ok();
            }

            return Ok(findTransportationInf);
        }

        // PUT: api/TransportationInf/UpdateTransportationInf
        [HttpPut]
        [ResponseType(typeof(void))]
        [Route("UpdateTransportationInf")]
        public IHttpActionResult UpdateTransportationInf(int shippingId, TransportationInf transportationInf)
        {
            if (transportationInf == null || transportationInf.ShippingId != shippingId)
            {
                return Ok(false);
            }

            var findTransportationInf = _transportationInfRepository.GetTransportationInfById(shippingId);
            if (findTransportationInf == null)
            {
                return NotFound();
            }

            findTransportationInf.DateOfTransportation = transportationInf.DateOfTransportation;
            findTransportationInf.OrderDate = transportationInf.OrderDate;
            findTransportationInf.ProductName = transportationInf.ProductName;
            findTransportationInf.StartingPoint = transportationInf.StartingPoint;
            findTransportationInf.Destination = transportationInf.Destination;
            findTransportationInf.Employer = transportationInf.Employer;
            findTransportationInf.ReceiverName = transportationInf.ReceiverName;
            findTransportationInf.JobIsActive = transportationInf.JobIsActive;
            findTransportationInf.StatusOfTransportation = transportationInf.StatusOfTransportation;
            findTransportationInf.ReceiveDateTime = DateTime.Now.ToLocalTime();

            var updateTransportationInf = _transportationInfRepository.UpdateTransportationInf(findTransportationInf);
            if (updateTransportationInf.Equals(false))
            {
                return Ok(false);
            }

            return Ok(true);
        }

    }
}