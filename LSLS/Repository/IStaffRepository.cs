using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSLS.Models;

namespace LSLS.Repository
{
    public interface IStaffRepository
    {
        IEnumerable<Staff> GetAllStaffs();
        Staff GetStaffById(int? staffId);
        bool AddStaff(Staff staff);
        bool UpdateStaff(Staff staff);
        bool DeleteStaff(int? staffId);
        Staff CheckLoginStaff();

    }
}
