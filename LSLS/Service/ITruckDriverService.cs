using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSLS.Models;

namespace LSLS.Service
{
    public interface ITruckDriverService
    {
        TruckDriver CheckLoginTruckDriver(TruckDriver truckDriver);
        bool UpdateTruckLocation(TruckLocation truckLocation);
        
    }
}
