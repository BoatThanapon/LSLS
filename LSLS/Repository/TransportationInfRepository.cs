using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using LSLS.Models;
using LSLS.Repository.Abstract;
using LSLS.ViewModels;

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
            if (shippingId == null)
            {
                return null;
            }

            TransportationInf transportationInfInDb = _context.TransportationInfs.Find(shippingId);

            return transportationInfInDb;
        }

        public bool AddTransportationInf(TransportationInf transportationInf)
        {
            if (transportationInf == null)
            {
                return false;
            }
            _context.TransportationInfs.Add(transportationInf);
            _context.SaveChanges();

            return true;
        }

        public bool UpdateTransportationInf(TransportationInf transportationInf)
        {
            if (transportationInf == null)
            {
                return false;
            }

            _context.Entry(transportationInf).State = EntityState.Modified;

            _context.SaveChanges();

            return true;
        }

        public bool DeleteTransportationInf(int? shippingId)
        {
            if (shippingId == null)
                return false;

            // ReSharper disable once SuggestVarOrType_SimpleTypes
            TransportationInf transportationInfInDb = _context.TransportationInfs.Find(shippingId);
            if (transportationInfInDb == null)
                return false;

            _context.TransportationInfs.Remove(transportationInfInDb);
            _context.SaveChanges();

            return true;
        }

        public FormJobAssignmentViewModel FromJobAssingment(int? shippingId)
        {
            TransportationInf findTransportationInf = _context.TransportationInfs.Find(shippingId);
            if (findTransportationInf != null)
            {
                FormJobAssignmentViewModel formCreateJobAssignment = new FormJobAssignmentViewModel
                {
                    JobAssignment = new JobAssignment
                    {
                        ShippingId = findTransportationInf.ShippingId,
                        StartingPointJob = findTransportationInf.StartingPoint,
                        DestinationJob = findTransportationInf.Destination,
                        JobAssignmentDate = findTransportationInf.DateOfTransportation,
                        
                    },
                        TruckDrivers = _context.TruckDrivers.ToList(),
                        TransportationInf = _context.TransportationInfs.ToList(),
                    };

                    return formCreateJobAssignment;
            }
            return null;
        }
            
    }
}
