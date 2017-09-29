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
    public class TransportationInfController : ApiController
    {
        private readonly ITransportationInfRepository _transportationInfRepository = new TransportationInfRepository(new ApplicationDbContext());

        // GET: api/TransportationInf/5
        [ResponseType(typeof(TransportationInf))]
        public IHttpActionResult GetTransportationInf(int shippingId)
        {
            var findTransportationInf = _transportationInfRepository.GetTransportationInfById(shippingId);
            if (findTransportationInf == null)
            {
                return NotFound();
            }

            return Ok(findTransportationInf);
        }

        // PUT: api/TransportationInf/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTransportationInf(int shippingId, TransportationInf transportationInf)
        {
            if (transportationInf == null || transportationInf.ShippingId != shippingId)
            {
                return BadRequest();
            }

            var findTransportationInf = _transportationInfRepository.GetTransportationInfById(shippingId);
            if (findTransportationInf == null)
            {
                return NotFound();
            }
           
            findTransportationInf = transportationInf;
            findTransportationInf.RecieveDateTime = DateTime.Now;

            var updateTransportationInf = _transportationInfRepository.UpdateTransportationInf(findTransportationInf);
            if (updateTransportationInf.Equals(false))
            {
                return Ok(false);
            }

            return Ok(true);
        }

    }
}