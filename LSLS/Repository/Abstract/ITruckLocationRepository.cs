using System.Collections.Generic;
using LSLS.Models;
using LSLS.ViewModels;

namespace LSLS.Repository.Abstract
{
    public interface ITruckLocationRepository
    {
        IEnumerable<TruckLocationViewModel> GetAllTruckLocations();
        TruckLocationViewModel SearchTruckLocationByTruckId(string truckId);

        TruckLocation GetTruckLocationByTruckDriverId(int truckLocationId);
        bool UpdateTruckLocation(TruckLocation truckLocation);
    }
}