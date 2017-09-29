using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            transportationInf.StatusOfTransportation = false;
                            transportationInf.JobIsActive = false;

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
 
            if (transportationInf.JobIsActive.Equals(true))
            {               
                var findJob = _context.JobAssignments.FirstOrDefault(j => j.ShippingId == transportationInf.ShippingId);
                if (findJob != null)
                {
                    findJob.JobAssignmentDate = transportationInf.DateOfTransportation;
                    findJob.StartingPointJob = transportationInf.StartingPoint;
                    findJob.DestinationJob = transportationInf.Destination;
                    findJob.JobAssignmentStatus = transportationInf.StatusOfTransportation;

                    transportationInf.JobIsActive = true;
                    _context.Entry(transportationInf).State = EntityState.Modified;

                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                transportationInf.JobIsActive = false;
                _context.Entry(transportationInf).State = EntityState.Modified;
               
                _context.SaveChanges();

                return true;
            }

            return false;
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
           
    }
}
