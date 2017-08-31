using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSLS.Models;

namespace LSLS.Repository.Abstract
{
    public interface ITransportationInfRepository
    {
        IEnumerable<TransportationInf> GetAllTransportationInfs();
        TransportationInf GetTransportationInfById(int? shippingId);
        bool AddTransportationInf(TransportationInf transportationInf);
        bool UpdateTransportationInf(TransportationInf transportationInf);
        bool DeleteTransportationInf(int? shippingId);

    }
}
