using System;
using System.Collections.Generic;
using System.Linq;
using LSLS.Models;

namespace LSLS.Repository
{
    public class StaffRepository : IStaffRepository
    {
        private readonly ApplicationDbContext _context;

        public StaffRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Staff> GetAllStaffs()
        {
            return _context.Staffs.ToList();
        }

        public Staff GetStaffById(int? staffId)
        {
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (staffId == null)
            {
                return null;
            }

            return _context.Staffs.Find(staffId);
        }

        public bool AddStaff(Staff staff)
        {
            if (staff == null)
            {
                return false;
            }

            _context.Staffs.Add(staff);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateStaff(Staff staff)
        {
            if (staff == null)
            {
                return false;
            }

            Staff staffInDb = _context.Staffs.FirstOrDefault(s => s.StaffId == staff.StaffId);
            if (staffInDb != null)
            {
                staffInDb.StaffEmployeeId = staff.StaffEmployeeId;
                staffInDb.StaffUsername = staff.StaffUsername;
                staffInDb.StaffPassword = staff.StaffPassword;
                staffInDb.StaffConfirmPassword = staffInDb.StaffConfirmPassword;
                staffInDb.StaffFullname = staff.StaffFullname;
                staffInDb.StaffGender = staff.StaffGender;
                staffInDb.StaffCitizenId = staff.StaffCitizenId;
                staffInDb.StaffBirthdate = staff.StaffBirthdate;
                staffInDb.StaffAddress = staff.StaffAddress;
                staffInDb.StaffEmail = staff.StaffEmail;
                staffInDb.StaffTelephoneNo = staff.StaffTelephoneNo;
            }
            _context.SaveChanges();

            return true;
        }

        public bool DeleteStaff(int? staffId)
        {
            if (staffId == null)
            {
                return false;
            }

            // ReSharper disable once SuggestVarOrType_SimpleTypes
            Staff staff = _context.Staffs.Find(staffId);
            if (staff == null)
            {
                return false;
            }

            _context.Staffs.Remove(staff);
            _context.SaveChanges();

            return true;
        }

        public Staff CheckLoginStaff()
        {
            throw new NotImplementedException();
        }
    }
}