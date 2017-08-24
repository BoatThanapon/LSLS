using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSLS.Models;

namespace LSLS.Repository
{
    public interface ITruckDriverRepository
    {
        IEnumerable<TruckDriver> GetAllTruckDrivers();
        TruckDriver GetTruckDriverById(int? truckdriverId);
        bool AddTruckDriver(TruckDriver truckDriver);
        bool UpdateTruckDriver(TruckDriver truckDriver);
        bool DeleteTruckDriver(int? truckdriverId);

    }
}
