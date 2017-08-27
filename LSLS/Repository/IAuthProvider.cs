using LSLS.Models;
using LSLS.ViewModels;

namespace LSLS.Repository
{
    public interface IAuthProvider
    {
        Staff AuthenticateStaff(LoginStaffViewModel staff);
    }
}
