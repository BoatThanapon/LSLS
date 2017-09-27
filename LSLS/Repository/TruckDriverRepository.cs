using System.Collections.Generic;
using System.Linq;
using LSLS.Models;
using LSLS.Repository.Abstract;

namespace LSLS.Repository
{
    public class TruckDriverRepository : ITruckDriverRepository
    {
        private readonly ApplicationDbContext _context;

        public TruckDriverRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TruckDriver> GetAllTruckDrivers()
        {
            return _context.TruckDrivers.ToList();
        }

        public TruckDriver GetTruckDriverById(int? truckdriverId)
        {
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (truckdriverId == null)
                return null;

            return _context.TruckDrivers.Find(truckdriverId);
        }

        public bool AddTruckDriver(TruckDriver truckDriver)
        {
            if (truckDriver == null)
                return false;
            _context.TruckDrivers.Add(truckDriver);

            TruckLocation truckLocation = new TruckLocation
            {
                TruckDriverId = truckDriver.TruckDriverId,
            };
            _context.TruckLocations.Add(truckLocation);

            _context.SaveChanges();

            return true;
        }

        public bool UpdateTruckDriver(TruckDriver truckDriver)
        {
            if (truckDriver == null)
                return false;

            // ReSharper disable once SuggestVarOrType_SimpleTypes
            var truckDriverInDb =
                _context.TruckDrivers.SingleOrDefault(s => s.TruckDriverId == truckDriver.TruckDriverId);
            if (truckDriverInDb != null)
            {
                truckDriverInDb.TruckDriverUsername = truckDriver.TruckDriverUsername;
                truckDriverInDb.TruckDriverPassword = truckDriver.TruckDriverPassword;
                truckDriverInDb.TruckDriverConfirmPassword = truckDriver.TruckDriverConfirmPassword;
                truckDriverInDb.TruckDriverFullname = truckDriver.TruckDriverFullname;
                truckDriverInDb.TruckDriverGender = truckDriver.TruckDriverGender;
                truckDriverInDb.TruckDriverCitizenId = truckDriver.TruckDriverCitizenId;
                truckDriverInDb.TruckDriverDriverLicenseId = truckDriver.TruckDriverDriverLicenseId;
                truckDriverInDb.TruckDriverBirthdate = truckDriver.TruckDriverBirthdate;
                truckDriverInDb.TruckDriverAddress = truckDriver.TruckDriverAddress;
                truckDriverInDb.TruckDriverEmail = truckDriver.TruckDriverEmail;
                truckDriverInDb.TruckDriverTelephoneNo = truckDriver.TruckDriverTelephoneNo;
                truckDriverInDb.TruckId = truckDriver.TruckId;
            }
            _context.SaveChanges();

            return true;
        }

        public bool DeleteTruckDriver(int? truckdriverId)
        {
            if (truckdriverId == null)
                return false;

            // ReSharper disable once SuggestVarOrType_SimpleTypes
            var truckDriver = _context.TruckDrivers.Find(truckdriverId);
            if (truckDriver == null)
                return false;

            _context.TruckDrivers.Remove(truckDriver);
            _context.SaveChanges();

            return true;
        }
    }
}