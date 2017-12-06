using System;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LSLS.Controllers;
using LSLS.Models;
using LSLS.Repository;
using LSLS.Repository.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Assert = NUnit.Framework.Assert;

namespace LSLS.Tests.Controller
{
    /// <summary>
    /// Summary description for StaffRepositoryTest
    /// </summary>
    [TestClass]
    public class StaffControllerTest
    {

        // ListAllStaffs -------------------------------------------------------

        [TestMethod]
        public void Test_ListAllStaffs_Return_List_And_ViewName_Correct()
        {
            // Arrange
            var staffList = MockListStaff();
            var staffRepository = new Mock<IStaffRepository>();
            staffRepository.Setup(e => e.GetAllStaffs()).Returns(staffList.AsQueryable());
            var controller = new StaffController(staffRepository.Object);

            // Act 
            var result = controller.ListAllStaffs() as ViewResult;
            var model = result.Model;

            // Assert
            Assert.AreEqual(result.ViewName, "ListAllStaffs");
            Assert.AreEqual(model, staffList);

        }

        //FormCreateStaff-------------------------------------------------------------------------------

        [TestMethod]
        public void Test_FormCreateStaff_Return_ViewName_Correct()
        {
            // Arrange
            var staffRepository = new Mock<IStaffRepository>();
            var controller = new StaffController(staffRepository.Object);

            // Act 
            var result = controller.FormCreateStaff() as ViewResult;
            var viewName = result.ViewName;

            // Assert
            Assert.AreEqual(viewName, "FormCreateStaff");
        }

        //DetailsStaff-------------------------------------------------------------------------------

        [TestMethod]
        public void Test_DetailsStaff_Return_HttpBadRequest_When_StaffId_Equal_Null()
        {
            // Arrange
            var staff = new Staff();
            var staffRepository = new Mock<IStaffRepository>();
            staffRepository.Setup(e => e.GetStaffById(null)).Returns(staff);
            var controller = new StaffController(staffRepository.Object);

            // Act
            var result = controller.DetailsStaff(null) as ActionResult;
            var staffIdNull = new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // Assert
            Assert.AreEqual(result.ToString(), staffIdNull.ToString());
        }

        [TestMethod]
        public void Test_DetailsStaff_Return_HttpNotFound_When_StaffRepo_Equal_Null()
        {
            // Arrange
            var staff = MockStaff();
            var staffRepository = new Mock<IStaffRepository>();
            staffRepository.Setup(e => e.GetStaffById(staff.StaffId)).Returns(staff);
            var controller = new StaffController(staffRepository.Object);

            // Act
            var result = controller.DetailsStaff(5) as ActionResult;
            var staffRepoNull = new HttpNotFoundResult();

            // Assert
            Assert.AreEqual(result.ToString(), staffRepoNull.ToString());
        }

        [TestMethod]
        public void Test_DetailsStaff_Return_Staff_And_ViewName_Correct()
        {
            // Arrange
            var staff = MockStaff();
            var staffRepository = new Mock<IStaffRepository>();
            staffRepository.Setup(e => e.GetStaffById(1)).Returns(staff);
            var controller = new StaffController(staffRepository.Object);

            // Act
            var result = controller.DetailsStaff(1) as ViewResult;
            var viewName = result.ViewName;
            var model = result.Model;

            // Assert
            Assert.AreEqual(viewName, "DetailsStaff");
            Assert.AreEqual(model, staff);
        }


        //FromEditStaff-------------------------------------------------------------------------------

        [TestMethod]
        public void Test_FromEditStaff_Return_HttpBadRequest_When_StaffId_Equal_Null()
        {
            // Arrange
            var staff = new Staff();
            var staffRepository = new Mock<IStaffRepository>();
            staffRepository.Setup(e => e.GetStaffById(null)).Returns(staff);
            var controller = new StaffController(staffRepository.Object);

            // Act
            var result = controller.FormEditStaff(null) as ActionResult;
            var staffIdNull = new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // Assert
            Assert.AreEqual(result.ToString(), staffIdNull.ToString());
        }

        [TestMethod]
        public void Test_FromEditStaff_Return_HttpNotFound_When_StaffRepo_Equal_Null()
        {
            // Arrange
            var staff = MockStaff();
            var staffRepository = new Mock<IStaffRepository>();
            staffRepository.Setup(e => e.GetStaffById(staff.StaffId)).Returns(staff);
            var controller = new StaffController(staffRepository.Object);

            // Act
            var result = controller.FormEditStaff(5) as ActionResult;
            var staffRepoNull = new HttpNotFoundResult();

            // Assert
            Assert.AreEqual(result.ToString(), staffRepoNull.ToString());
        }

        [TestMethod]
        public void Test_FromEditStaff_Return_Staff_And_ViewName_Correct()
        {
            // Arrange
            var staff = MockStaff();
            var staffRepository = new Mock<IStaffRepository>();
            staffRepository.Setup(e => e.GetStaffById(1)).Returns(staff);
            var controller = new StaffController(staffRepository.Object);

            // Act
            var result = controller.FormEditStaff(1) as ViewResult;
            var viewName = result.ViewName;

            // Assert
            Assert.AreEqual(viewName, "FormEditStaff");
        }

        //SaveStaff-------------------------------------------------------------------------------

        [TestMethod]
        public void Test_SaveStaff_Return_FormCreateStaff_When_ModelState_Is_Not_Valid()
        {
            // Arrange
            var staff = MockStaff();
            staff.StaffCitizenId = null;

            var staffRepository = new Mock<IStaffRepository>();
            var controller = new StaffController(staffRepository.Object);
            controller.ModelState.AddModelError("fakeError", "");

            // Act
            var result = controller.SaveStaff(staff) as ViewResult;
            var viewName = result.ViewName.ToString();

            // Assert
            Assert.AreEqual(viewName, "FormCreateStaff");
        }

        [TestMethod]
        public void Test_SaveStaff_Return_ListAllStaffs_When_StaffId_Equal_Zero()
        {
            // Arrange
            var staff = MockStaff();
            staff.StaffId = 0;

            var staffRepository = new Mock<IStaffRepository>();
            staffRepository.Setup(e => e.AddStaff(staff)).Returns(true);
            var controller = new StaffController(staffRepository.Object);

            // Act
            var result = controller.SaveStaff(staff) as RedirectToRouteResult;
            var redirectViewName = result.RouteValues["Action"];

            // Assert
            Assert.AreEqual(redirectViewName, "ListAllStaffs");
        }

        [TestMethod]
        public void Test_SaveStaff_Return_ListAllStaffs_When_StaffId_Not_Equal_Zero()
        {
            // Arrange
            var staff = MockStaff();
            staff.StaffId = 1;

            var staffRepository = new Mock<IStaffRepository>();
            staffRepository.Setup(e => e.UpdateStaff(staff)).Returns(true);
            var controller = new StaffController(staffRepository.Object);

            // Act
            var result = controller.SaveStaff(staff) as RedirectToRouteResult;
            var redirectViewName = result.RouteValues["Action"];

            // Assert
            Assert.AreEqual(redirectViewName, "ListAllStaffs");
        }


        //DeleteStaff-------------------------------------------------------------------------------
        [TestMethod]
        public void Test_DeleteStaff_Return_HttpBadRequest_When_StaffId_Equal_Null()
        {
            // Arrange
            var staff = new Staff();
            var staffRepository = new Mock<IStaffRepository>();
            staffRepository.Setup(e => e.GetStaffById(null)).Returns(staff);
            var controller = new StaffController(staffRepository.Object);

            // Act
            var result = controller.DeleteStaff(null) as ActionResult;
            var staffIdNull = new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // Assert
            Assert.AreEqual(result.ToString(), staffIdNull.ToString());
        }

        [TestMethod]
        public void Test_DeleteStaff_Return_HttpNotFound_When_StaffRepo_Equal_Null()
        {
            // Arrange
            var staff = MockStaff();
            var staffRepository = new Mock<IStaffRepository>();
            staffRepository.Setup(e => e.GetStaffById(staff.StaffId)).Returns(staff);
            var controller = new StaffController(staffRepository.Object);

            // Act
            var result = controller.DeleteStaff(5) as ActionResult;
            var staffRepoNull = new HttpNotFoundResult();

            // Assert
            Assert.AreEqual(result.ToString(), staffRepoNull.ToString());
        }

        [TestMethod]
        public void Test_DeleteStaff_Return_Staff_And_ViewName_Correct()
        {
            // Arrange
            var staff = MockStaff();
            var staffRepository = new Mock<IStaffRepository>();
            staffRepository.Setup(e => e.GetStaffById(1)).Returns(staff);
            var controller = new StaffController(staffRepository.Object);

            // Act
            var result = controller.DeleteStaff(1) as ViewResult;
            var viewName = result.ViewName;

            // Assert
            Assert.AreEqual(viewName, "DeleteStaff");
        }


        //DeleteStaffConfirmed-------------------------------------------------------------
        [TestMethod]
        public void Test_DeleteStaffConfirmed_Return_Staff_And_ViewName_Correct_When_StaffRepo_Return_true()
        {
            // Arrange
            var staff = MockStaff();
            var staffRepository = new Mock<IStaffRepository>();
            staffRepository.Setup(e => e.DeleteStaff(staff.StaffId)).Returns(true);
            var controller = new StaffController(staffRepository.Object);

            // Act
            var result = controller.DeleteStaffConfirmed(staff.StaffId) as RedirectToRouteResult;
            var redirectViewName = result.RouteValues["Action"];

            // Assert
            Assert.AreEqual(redirectViewName, "ListAllStaffs");
        }

        [TestMethod]
        public void Test_DeleteStaffConfirmed_Return_Staff_And_ViewName_Correct_When_StaffRepo_Return_false()
        {
            // Arrange
            var staff = MockStaff();
            var staffRepository = new Mock<IStaffRepository>();
            staffRepository.Setup(e => e.DeleteStaff(staff.StaffId)).Returns(false);
            var controller = new StaffController(staffRepository.Object);

            // Act
            var result = controller.DeleteStaffConfirmed(staff.StaffId) as ViewResult;
            var redirectViewName = result.ViewName;

            // Assert
            Assert.AreEqual(redirectViewName, "DeleteStaff");
        }




        // List Staff
        public IEnumerable<Staff> MockListStaff()
        {
            var staffList = new Staff[]
            {
                new Staff
                {
                    StaffId = 1,
                    StaffEmployeeId = "E0001",
                    StaffUsername = "AdminA",
                    StaffPassword = "1234",
                    StaffConfirmPassword = "1234",
                    StaffFullname = "Thanapon",
                    StaffCitizenId = "15799900617731",
                    StaffGender = "Male",
                    StaffAddress = "264 Chiang Saen Chiang Rai",
                    StaffBirthdate = new DateTime(2010, 3, 11).ToString(CultureInfo.InvariantCulture),
                    StaffEmail = "AdminA@gmail.com",
                    StaffTelephoneNo = "0970747125"
                },
                new Staff
                {
                    StaffId = 2,
                    StaffEmployeeId = "E0002",
                    StaffUsername = "AdminB",
                    StaffPassword = "56789",
                    StaffConfirmPassword = "56789",
                    StaffFullname = "Rawisara",
                    StaffCitizenId = "1234567897894",
                    StaffGender = "Female",
                    StaffAddress = "948 Sansai Chiang Mai",
                    StaffBirthdate = new DateTime(2010, 5, 7).ToString(CultureInfo.InvariantCulture),
                    StaffEmail = "AdminB@gmail.com",
                    StaffTelephoneNo = "0123456789"
                }
            };

            return staffList;
        }

        public Staff MockStaff()
        {
            Staff staff = new Staff
            {
                StaffId = 1,
                StaffEmployeeId = "E0001",
                StaffUsername = "AdminA",
                StaffPassword = "1234",
                StaffConfirmPassword = "1234",
                StaffFullname = "Thanapon",
                StaffCitizenId = "15799900617731",
                StaffGender = "Male",
                StaffAddress = "264 Chiang Saen Chiang Rai",
                StaffBirthdate = new DateTime(2010, 3, 11).ToString(CultureInfo.InvariantCulture),
                StaffEmail = "AdminA@gmail.com",
                StaffTelephoneNo = "0970747125"
            };

            return staff;
        }
    }
}
