using System.Collections.Generic;
using LSLS.Models;

namespace LSLS.Repository.Abstract
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