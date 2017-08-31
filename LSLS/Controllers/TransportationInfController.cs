using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LSLS.Models;
using LSLS.Repository.Abstract;

namespace LSLS.Controllers
{
    public class TransportationInfController : Controller
    {
        private readonly ITransportationInfRepository _transportationInfRepository;

        public TransportationInfController(ITransportationInfRepository transportationInfRepository)
        {
            _transportationInfRepository = transportationInfRepository;
        }

        // GET: TransportationInf
        [HttpGet]
        public ActionResult ListAllTransportationInfs()
        {
            
            return View("ListAllTransportationInfs", _transportationInfRepository.GetAllTransportationInfs());
        }

        //GET: TransportationInf/DetailsTransportationInf/shippingId
        [HttpGet]
        public ActionResult DetailsTransportationInf(int? shippingId)
        {
            if (shippingId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var detailsTrans = _transportationInfRepository.GetTransportationInfById(shippingId);
            if (detailsTrans == null)
                return HttpNotFound();

            return View("DetailsTransportationInf", detailsTrans);
        }

        //GET TransportationInf/FormCreateTransportationInf
        [HttpGet]
        public ActionResult FormCreateTransportationInf()
        {
            return View("FormCreateTransportationInf");
        }

        //GET TransportationInf/FormEditTransportationInf/shippingId
        [HttpGet]
        public ActionResult FormEditTransportationInf(int? shippingId)
        {
            if (shippingId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            TransportationInf transportationInfInDb = _transportationInfRepository.GetTransportationInfById(shippingId);
            if (transportationInfInDb == null)
                return HttpNotFound();

            return View("FormEditTransportationInf", transportationInfInDb);
        }

        // POST: TransportationInf/SaveTransportationInf
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveTransportationInf(TransportationInf transportationInf)
        {
            if (!ModelState.IsValid)
                return View(transportationInf);

            if (transportationInf.ShippingId == 0)
            {
                bool transportationInfAdd = _transportationInfRepository.AddTransportationInf(transportationInf);
                if (transportationInfAdd.Equals(true))
                    return RedirectToAction("ListAllTransportationInfs");
            }
            else
            {
                bool transportationInfEdit = _transportationInfRepository.UpdateTransportationInf(transportationInf);
                if (transportationInfEdit.Equals(true))
                    return RedirectToAction("ListAllTransportationInfs");
            }

            return View(transportationInf);
        }

        // GET:TransportationInf/DeleteTransportationInf/shippingId
        [HttpGet]
        public ActionResult DeleteTransportationInf(int? shippingId)
        {
            if (shippingId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            TransportationInf transportationInfInDb = _transportationInfRepository.GetTransportationInfById(shippingId);
            if (transportationInfInDb == null)
                return HttpNotFound();

            return View("DeleteTransportationInf", transportationInfInDb);
        }

        //POST:TransportationInf/DeleteTransportationInf/shippingId
        [HttpPost]
        [ActionName("DeleteTransportationInf")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTransportationInfConfirmed(int? shippingId)
        {
            if (shippingId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            bool transportationInfInDb = _transportationInfRepository.DeleteTransportationInf(shippingId);
            if (transportationInfInDb.Equals(true))
                return RedirectToAction("ListAllTransportationInfs");

            return View("DeleteTransportationInf");
        }

    }
}