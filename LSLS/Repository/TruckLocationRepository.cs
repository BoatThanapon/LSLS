using System.Collections.Generic;
using System.Linq;
using LSLS.Models;
using LSLS.Repository.Abstract;
using LSLS.ViewModels;

namespace LSLS.Repository
{
    public class TruckLocationRepository : ITruckLocationRepository
    {
        private readonly ApplicationDbContext _context;

        public TruckLocationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TruckLocationViewModel> GetAllTruckLocations()
        {
            List<TruckLocationViewModel> truckLocationVMlist = new List<TruckLocationViewModel>(); // to hold list of Customer and order details
            var truckLocationlist = (
                from truckLocation in _context.TruckLocations
                join truckDriver in _context.TruckDrivers on
                    truckLocation.TruckDriverId equals truckDriver.TruckDriverId
                select new
                {
                    truckDriver.TruckId,
                    truckDriver.TruckDriverFullname,
                    truckLocation.Latitude,
                    truckLocation.Longitude,
                    truckLocation.TruckCurrentTime,
                    truckLocation.TruckCurrentAddress
                }).ToList();

            //query getting data from database from joining two tables and storing data in customerlist
            foreach (var item in truckLocationlist)
            {
                var truckLocationViewModel = new TruckLocationViewModel(); // ViewModel
                truckLocationViewModel.TruckId = item.TruckId;
                truckLocationViewModel.TruckDriverFullname = item.TruckDriverFullname;
                truckLocationViewModel.Latitude = item.Latitude;
                truckLocationViewModel.Longitude = item.Longitude;
                truckLocationViewModel.TruckCurrentTime = item.TruckCurrentTime;
                truckLocationViewModel.TruckCurrentAddress = item.TruckCurrentAddress;

                truckLocationVMlist.Add(truckLocationViewModel);
            }

            return truckLocationVMlist.ToList();
        }

        public TruckLocationViewModel GetTruckLocationByTruckId(string truckId)
        {
            var truckLocation = GetAllTruckLocations();

            if (truckId != null)
            {
                var findTruckID = truckLocation.FirstOrDefault(u => u.TruckId.Equals(truckId));

                return findTruckID;
            }

            return null;
        }
    }
}