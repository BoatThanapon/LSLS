using System.Collections.Generic;
using System.Linq;
using LSLS.Models;
using LSLS.Repository.Abstract;

namespace LSLS.Repository
{
    public class ReportTransportationRepository : IReportTransportationRepository
    {
        private readonly ApplicationDbContext _context;

        public ReportTransportationRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<TransportationInf> ListTransportationInfIsCompletedInMonth(string month, string year)
        {          
            var transportationInMonth =
                _context.TransportationInfs.Where(t => t.ReceiveDateTime.Value.Month.ToString() == month 
                                                    && t.ReceiveDateTime.Value.Year.ToString() == year
                                                    && t.StatusOfTransportation.Equals(true));

            
            return transportationInMonth;
        }

        public IEnumerable<TransportationInf> ListTransportationInfIsNotCompletedInMonth(string month, string year)
        {
            var transportationInMonth =
                _context.TransportationInfs.Where(t => t.ReceiveDateTime.Value.Month.ToString() == month
                                                       && t.ReceiveDateTime.Value.Year.ToString() == year
                                                       && t.StatusOfTransportation.Equals(false));


            return transportationInMonth;
        }
    }
}