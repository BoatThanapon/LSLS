using System.Collections.Generic;
using LSLS.ViewModels;

namespace LSLS.Repository.Abstract
{
    public interface ITruckLocationRepository
    {
        IEnumerable<TruckLocationViewModel> GetAllTruckLocations();
        TruckLocationViewModel GetTruckLocationByTruckId(string truckId);
    }
}