using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LSLS.Models;
using LSLS.Repository.Abstract;

namespace LSLS.Repository
{
    public class TransportationInfRepository : ITransportationInfRepository
    {
        private readonly ApplicationDbContext _context;

        public TransportationInfRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TransportationInf> GetAllTransportationInfs()
        {
            return _context.TransportationInfs.ToList();
        }

        public TransportationInf GetTransportationInfById(int? shippingId)
        {
            if(shippingId == null)
                return null;

            TransportationInf transportationInfInDb = _context.TransportationInfs.Find(shippingId);
            
            return transportationInfInDb;
        }

        public bool AddTransportationInf(TransportationInf transportationInf)
        {
            if (transportationInf == null)
                return false;

            _context.TransportationInfs.Add(transportationInf);
            _context.SaveChanges();

            return true;
        }

        public bool UpdateTransportationInf(TransportationInf transportationInf)
        {
            if (transportationInf == null)
                return false;

            TransportationInf transportationInfInDb =
                _context.TransportationInfs.FirstOrDefault(t => t.ShippingId == transportationInf.ShippingId);

            if (transportationInfInDb != null)
            {
                transportationInfInDb.Employer = transportationInf.Employer;
                transportationInfInDb.OrderDate = transportationInf.OrderDate;
                transportationInfInDb.DateOfTransportation = transportationInf.DateOfTransportation;
                transportationInfInDb.ProductName = transportationInf.ProductName;
                transportationInfInDb.StartingPoint = transportationInf.StartingPoint;
                transportationInfInDb.Destination = transportationInf.Destination;
                transportationInfInDb.RecieverName = transportationInf.RecieverName;
                transportationInfInDb.StatusOfTransportation = transportationInf.StatusOfTransportation;
                transportationInfInDb.ShipingDocImageUrl = transportationInf.ShipingDocImageUrl;
            }

            _context.SaveChanges();

            return true;
        }

        public bool DeleteTransportationInf(int? shippingId)
        {
            if (shippingId == null)
                return false;

            // ReSharper disable once SuggestVarOrType_SimpleTypes
            TransportationInf transportationInfInDb  = _context.TransportationInfs.FirstOrDefault(t => t.ShippingId == shippingId);
            if (transportationInfInDb == null)
                return false;

            _context.TransportationInfs.Remove(transportationInfInDb);
            _context.SaveChanges();

            return true;
        }
    }
}