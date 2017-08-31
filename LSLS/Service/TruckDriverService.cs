using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LSLS.Models;

namespace LSLS.Service
{
    public class TruckDriverService : ITruckDriverService
    {
        private readonly ApplicationDbContext _context;

        public TruckDriverService(ApplicationDbContext context)
        {
            _context = context;
        }

        public TruckDriver CheckLoginTruckDriver(TruckDriver truckDriver)
        {
            if (truckDriver == null)
                return null;

            TruckDriver checkTruckDriver = _context.TruckDrivers.FirstOrDefault(t =>
                t.TruckDriverUsername.Equals(truckDriver.TruckDriverUsername)
                && t.TruckDriverPassword.Equals(truckDriver.TruckDriverPassword));

            if(checkTruckDriver == null)
                return null;

            return checkTruckDriver;
        }

        public bool UpdateTruckLocation(TruckLocation truckLocation)
        {
            if (truckLocation == null)
                return false;

            var updateTruckLocation = _context.TruckLocations.Find(truckLocation.TruckDriverId);
            if (updateTruckLocation != null)
            {
                updateTruckLocation.Latitude = truckLocation.Latitude;
                updateTruckLocation.Longitude = truckLocation.Longitude;
                updateTruckLocation.TruckCurrentAddress = truckLocation.TruckCurrentAddress;
                updateTruckLocation.TruckCurrentTime = truckLocation.TruckCurrentTime;
            }

            _context.SaveChanges();

            return true;
        }
    }
}