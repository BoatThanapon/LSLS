using System.Linq;
using System.Web.Security;
using LSLS.Models;
using LSLS.Repository.Abstract;
using LSLS.ViewModels;

namespace LSLS.Repository
{
    public class FormsAuthProvider : IAuthProvider
    {
        private readonly ApplicationDbContext _context;

        public FormsAuthProvider(ApplicationDbContext context)
        {
            _context = context;
        }

        public Staff AuthenticateStaff(LoginStaffViewModel staff)
        {
            if (staff == null)
                return null;


            Staff staffInDb = _context.Staffs.FirstOrDefault(u => u.StaffEmployeeId.Equals(staff.StaffEmployeeId) &&
                                                                u.StaffUsername.Equals(staff.StaffUsername) &&
                                                                u.StaffPassword.Equals(staff.StaffPassword));
            if (staffInDb == null)
                return null;

            FormsAuthentication.SetAuthCookie(staffInDb.StaffUsername, false);
            return staffInDb;
        }
    }
}