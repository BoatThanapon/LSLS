using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LSLS.Models;
using LSLS.ViewModels;

namespace LSLS.Repository
{
    public class TruckLocationRepository : ITruckLocationRepository
    {
        private ApplicationDbContext _context;

        public TruckLocationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TruckLocationViewModel> GetAllTruckLocation()
        {
            List<TruckLocationViewModel> TruckLocationVMlist = new List<TruckLocationViewModel>(); // to hold list of Customer and order details
            var truckLocationlist = (
                from truckLocation in _context.TruckLocations
                join truckDriver in _context.TruckDrivers on 
                truckLocation.TruckDriverId equals truckDriver.TruckDriverId
            select new {
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
                TruckLocationViewModel truckLocationViewModel = new TruckLocationViewModel(); // ViewModel
                truckLocationViewModel.TruckId = item.TruckId;
                truckLocationViewModel.TruckDriverFullname = item.TruckDriverFullname;
                truckLocationViewModel.Latitude = item.Latitude;
                truckLocationViewModel.Longitude = item.Longitude;
                truckLocationViewModel.TruckCurrentTime = item.TruckCurrentTime;
                truckLocationViewModel.TruckCurrentAddress = item.TruckCurrentAddress;

                TruckLocationVMlist.Add(truckLocationViewModel);
            }

            return TruckLocationVMlist.ToList();
        }

        public TruckLocationViewModel GetTruckLocationByTruckId(TruckLocationViewModel truckLocation)
        {
            throw new NotImplementedException();
        }
    }
}