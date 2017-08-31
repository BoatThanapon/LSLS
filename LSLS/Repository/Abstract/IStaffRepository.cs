using System.Collections.Generic;
using LSLS.Models;

namespace LSLS.Repository.Abstract
{
    public interface IStaffRepository
    {
        IEnumerable<Staff> GetAllStaffs();
        Staff GetStaffById(int? staffId);
        bool AddStaff(Staff staff);
        bool UpdateStaff(Staff staff);
        bool DeleteStaff(int? staffId);
    }
}