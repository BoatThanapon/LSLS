using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSLS.ViewModels;

namespace LSLS.Repository
{
    public interface ITruckLocationRepository
    {
        IEnumerable<TruckLocationViewModel> GetAllTruckLocations();
        TruckLocationViewModel GetTruckLocationByTruckId(string truckId);
    }
}
