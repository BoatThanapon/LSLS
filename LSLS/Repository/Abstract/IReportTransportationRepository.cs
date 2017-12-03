using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSLS.Models;

namespace LSLS.Repository.Abstract
{
    public interface IReportTransportationRepository
    {
        IEnumerable<TransportationInf> ListTransportationInfIsCompletedInMonth(string month, string year);
        IEnumerable<TransportationInf> ListTransportationInfIsNotCompletedInMonth(string month, string year);

    }
}
