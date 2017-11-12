using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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
        /*
        public TransportationInfController(ITransportationInfRepository repository)
        {
            _transportationInfRepository = repository;
        }
        */
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
        public IHttpActionResult UpdateTransportationInf(int? shippingId, TransportationInf transportationInf)
        {
            if (transportationInf == null || transportationInf.ShippingId != shippingId)
            {
                return NotFound();
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
            findTransportationInf.ShippingDocImage = transportationInf.ShippingDocImage;
            findTransportationInf.ShippingNote = transportationInf.ShippingNote;


            // ReSharper disable once UseNullPropagation
            if (findTransportationInf.ShippingDocImage != null)
            {
                if (findTransportationInf.ShippingDocImage.Length > 0)
                {
                    var path = HttpContext.Current.Server.MapPath("~/App_Data/ShippingDoc/"); //Path

                    //Check if directory exist
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path); //Create directory if it doesn't exist
                    }

                    var imageName = findTransportationInf.ReceiveDateTime.ToString() + findTransportationInf.ShippingId + findTransportationInf.Employer + ".jpg";

                    //set the image path
                    var imgPath = Path.Combine(path, imageName);

                    File.WriteAllBytes(imgPath, findTransportationInf.ShippingDocImage);
                }

            }

            var updateTransportationInf = _transportationInfRepository.UpdateTransportationInf(findTransportationInf);
            if (updateTransportationInf.Equals(false))
            {
                return Ok(false);
            }

            return Ok(true);
        }

    }
}