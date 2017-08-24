﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LSLS.Models;
using LSLS.Repository;


namespace LSLS.Controllers
{
    public class StaffController : Controller
    {
        private readonly IStaffRepository _staffRepository;

        public StaffController(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        // GET: Staff/ListAllStaffs
        [HttpGet]
        public ActionResult ListAllStaffs()
        {
            return View("ListAllStaffs", _staffRepository.GetAllStaffs());
        }

        // GET: Staff/FormCreateStaff
        [HttpGet]
        public ActionResult FormCreateStaff()
        {
            return View("FormCreateStaff");
        }
       
        // GET: Staff/DetailsStaff/staffId
        [HttpGet]
        public ActionResult DetailsStaff(int? staffId)
        {
            if (staffId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // ReSharper disable once SuggestVarOrType_SimpleTypes
            Staff staff = _staffRepository.GetStaffById(staffId);
            if (staff == null)
            {
                return HttpNotFound();
            }

            return View("DetailsStaff",staff);
        }

        // GET: Staff/FormEditStaff/staffId
        [HttpGet]
        public ActionResult FormEditStaff(int? staffId)
        {
            if (staffId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Staff staff = _staffRepository.GetStaffById(staffId);
            if (staff == null)
            {
                return HttpNotFound();
            }

            return View("FormEditStaff", staff);
        }

        // POST: Staff/SaveStaff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveStaff(Staff staff)
        {
            if (!ModelState.IsValid)
                return View(staff);

            if (staff.StaffId == 0)
            {
                bool staffAdd = _staffRepository.AddStaff(staff);
                if (staffAdd.Equals(true))
                {
                    return RedirectToAction("ListAllStaffs");
                }
            }
            else
            {
                bool staffEdit = _staffRepository.UpdateStaff(staff);
                if (staffEdit.Equals(true))
                {
                    return RedirectToAction("ListAllStaffs");
                }

            }

            return View(staff);
        }

        
        // GET: Staff/DeleteStaff/staffId
        [HttpGet]
        public ActionResult DeleteStaff(int? staffId)
        {
            if (staffId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Staff staff = _staffRepository.GetStaffById(staffId);
            if (staff == null)
            {
                return HttpNotFound();
            }

            return View("DeleteStaff", staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("DeleteStaff")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteStaffConfirmed(int? staffId)
        {
            // ReSharper disable once SuggestVarOrType_BuiltInTypes
            bool deleteStaff = _staffRepository.DeleteStaff(staffId);
            if (deleteStaff.Equals(true))
            {
                return RedirectToAction("ListAllStaffs");
            }

            return View("DeleteStaff");
        }


    }
}