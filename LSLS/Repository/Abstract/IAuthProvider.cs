using LSLS.Models;
using LSLS.ViewModels;

namespace LSLS.Repository.Abstract
{
    public interface IAuthProvider
    {
        Staff AuthenticateStaff(LoginStaffViewModel staff);
        TruckDriver AuthenticateTruckDriver(string username, string password);
    }
}