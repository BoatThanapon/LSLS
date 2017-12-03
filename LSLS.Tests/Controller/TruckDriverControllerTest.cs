using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LSLS.Controllers;
using LSLS.Models;
using LSLS.Repository.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LSLS.Tests.Controller
{
    [TestClass]
    public class TruckDriverControllerTest
    {
        // ListAllStaffs -------------------------------------------------------

        [TestMethod]
        public void Test_ListAllTruckDrivers_Return_List_And_ViewName_Correct()
        {
            // Arrange
            var truckDriverList = MockListTruckDrivers();
            var truckDriverRepository = new Mock<ITruckDriverRepository>();
            truckDriverRepository.Setup(e => e.GetAllTruckDrivers()).Returns(truckDriverList.AsQueryable());
            var controller = new TruckDriverController(truckDriverRepository.Object);

            // Act 
            var result = controller.ListAllTruckDrivers() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.ViewName, "ListAllTruckDrivers");
        }

        //FormCreateStaff-------------------------------------------------------------------------------

        [TestMethod]
        public void Test_FormCreateTruckDriver_Return_ViewName_Correct()
        {
            // Arrange
            var truckDriverRepository = new Mock<ITruckDriverRepository>();
            var controller = new TruckDriverController(truckDriverRepository.Object);

            // Act 
            var result = controller.FormCreateTruckDriver() as ViewResult;
            var viewName = result.ViewName;

            // Assert
            Assert.AreEqual(viewName, "FormCreateTruckDriver");
        }

        //DetailsTruckDriver-------------------------------------------------------------------------------

        [TestMethod]
        public void Test_DetailsTruckDriver_Return_HttpBadRequest_When_TruckDrivevrId_Equal_Null()
        {
            // Arrange
            var truckDriver = new TruckDriver();
            var truckDriverRepository = new Mock<ITruckDriverRepository>();
            truckDriverRepository.Setup(e => e.GetTruckDriverById(null)).Returns(truckDriver);
            var controller = new TruckDriverController(truckDriverRepository.Object);

            // Act
            var result = controller.DetailsTruckDriver(null) as ActionResult;
            var truckDriverIdNull = new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // Assert
            Assert.AreEqual(result.ToString(), truckDriverIdNull.ToString());
        }

        [TestMethod]
        public void Test_DetailsTruckDriver_Return_HttpNotFound_When_TruckDriverRepo_Equal_Null()
        {
            // Arrange
            var truckDriver = MockTruckDriver();
            var truckDriverRepository = new Mock<ITruckDriverRepository>();
            truckDriverRepository.Setup(e => e.GetTruckDriverById(truckDriver.TruckDriverId)).Returns(truckDriver);
            var controller = new TruckDriverController(truckDriverRepository.Object);

            // Act
            var result = controller.DetailsTruckDriver(5) as ActionResult;
            var truckDriverRepoNull = new HttpNotFoundResult();

            // Assert
            Assert.AreEqual(result.ToString(), truckDriverRepoNull.ToString());
        }

        [TestMethod]
        public void Test_DetailsTruckDriver_Return_TruckDriver_And_ViewName_Correct()
        {
            // Arrange
            var truckDriver = MockTruckDriver();
            var truckDriverRepository = new Mock<ITruckDriverRepository>();
            truckDriverRepository.Setup(e => e.GetTruckDriverById(truckDriver.TruckDriverId)).Returns(truckDriver);
            var controller = new TruckDriverController(truckDriverRepository.Object);

            // Act
            var result = controller.DetailsTruckDriver(1) as ViewResult;
            var viewName = result.ViewName;
            var model = result.Model as TruckDriver;

            // Assert
            Assert.AreEqual(viewName, "DetailsTruckDriver");
            Assert.IsNotNull(model);
        }


        //FormEditTruckDriver-------------------------------------------------------------------------------

        [TestMethod]
        public void Test_FormEditTruckDriver_Return_HttpBadRequest_When_TruckDrivevrId_Equal_Null()
        {
            // Arrange
            var truckDriver = new TruckDriver();
            var truckDriverRepository = new Mock<ITruckDriverRepository>();
            truckDriverRepository.Setup(e => e.GetTruckDriverById(null)).Returns(truckDriver);
            var controller = new TruckDriverController(truckDriverRepository.Object);

            // Act
            var result = controller.FormEditTruckDriver(null) as ActionResult;
            var truckDriverIdNull = new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // Assert
            Assert.AreEqual(result.ToString(), truckDriverIdNull.ToString());
        }

        [TestMethod]
        public void Test_FormEditTruckDriver_Return_HttpNotFound_When_TruckDriverRepo_Equal_Null()
        {
            // Arrange
            var truckDriver = MockTruckDriver();
            var truckDriverRepository = new Mock<ITruckDriverRepository>();
            truckDriverRepository.Setup(e => e.GetTruckDriverById(truckDriver.TruckDriverId)).Returns(truckDriver);
            var controller = new TruckDriverController(truckDriverRepository.Object);

            // Act
            var result = controller.FormEditTruckDriver(5) as ActionResult;
            var truckDriverRepoNull = new HttpNotFoundResult();

            // Assert
            Assert.AreEqual(result.ToString(), truckDriverRepoNull.ToString());
        }

        [TestMethod]
        public void Test_FormEditTruckDriver_Return_TruckDriver_And_ViewName_Correct()
        {
            // Arrange
            var truckDriver = MockTruckDriver();
            var truckDriverRepository = new Mock<ITruckDriverRepository>();
            truckDriverRepository.Setup(e => e.GetTruckDriverById(truckDriver.TruckDriverId)).Returns(truckDriver);
            var controller = new TruckDriverController(truckDriverRepository.Object);

            // Act
            var result = controller.FormEditTruckDriver(1) as ViewResult;
            var viewName = result.ViewName;
            var model = result.Model as TruckDriver;

            // Assert
            Assert.AreEqual(viewName, "FormEditTruckDriver");
            Assert.IsNotNull(model);
        }


        //SaveTruckDriver-------------------------------------------------------------------------------

        [TestMethod]
        public void Test_SaveTruckDriver_Return_TruckDriverModel_When_ModelState_Is_Not_Valid()
        {
            // Arrange
            var truckDriver = MockTruckDriver();
            truckDriver.TruckDriverFullname = null;

            var truckDriverRepository = new Mock<ITruckDriverRepository>();
            var controller = new TruckDriverController(truckDriverRepository.Object);
            controller.ModelState.AddModelError("fakeError", "");

            // Act
            var result = controller.SaveTruckDriver(truckDriver) as ViewResult;
            var modelView = result.Model as TruckDriver;

            // Assert
            Assert.IsNotNull(modelView);
        }

        [TestMethod]
        public void Test_SaveTruckDriver_Return_ListAllTruckDrivers_When_StaffId_Equal_Zero()
        {
            // Arrange
            var truckDriver = MockTruckDriver();
            truckDriver.TruckDriverId = 0;

            var truckDriverRepository = new Mock<ITruckDriverRepository>();
            truckDriverRepository.Setup(e => e.AddTruckDriver(truckDriver)).Returns(true);
            var controller = new TruckDriverController(truckDriverRepository.Object);

            // Act
            var result = controller.SaveTruckDriver(truckDriver) as RedirectToRouteResult;
            var redirectViewName = result.RouteValues["Action"];

            // Assert
            Assert.AreEqual(redirectViewName, "ListAllTruckDrivers");
        }

        [TestMethod]
        public void Test_SaveTruckDriver_Return_ListAllTruckDrivers_When_StaffId_Not_Equal_Zero()
        {
            // Arrange
            var truckDriver = MockTruckDriver();
            truckDriver.TruckDriverId = 1;

            var truckDriverRepository = new Mock<ITruckDriverRepository>();
            truckDriverRepository.Setup(e => e.UpdateTruckDriver(truckDriver)).Returns(true);
            var controller = new TruckDriverController(truckDriverRepository.Object);

            // Act
            var result = controller.SaveTruckDriver(truckDriver) as RedirectToRouteResult;
            var redirectViewName = result.RouteValues["Action"];

            // Assert
            Assert.AreEqual(redirectViewName, "ListAllTruckDrivers");
        }


        //DeleteTruckDriver-------------------------------------------------------------------------------
        [TestMethod]
        public void Test_DeleteTruckDriver_Return_HttpBadRequest_When_TruckDriverId_Equal_Null()
        {
            // Arrange
            var truckDriver = new TruckDriver();
            var truckDriverRepository = new Mock<ITruckDriverRepository>();
            truckDriverRepository.Setup(e => e.GetTruckDriverById(null)).Returns(truckDriver);
            var controller = new TruckDriverController(truckDriverRepository.Object);

            // Act
            var result = controller.DeleteTruckDriver(null) as ActionResult;
            var truckDriverIdNull = new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // Assert Staff Id Null
            Assert.AreEqual(result.ToString(), truckDriverIdNull.ToString());
        }

        [TestMethod]
        public void Test_DeleteTruckDriver_Return_HttpNotFound_When_TruckDriverRepo_Equal_Null()
        {
            // Arrange
            var truckDriver = MockTruckDriver();
            var truckDriverRepository = new Mock<ITruckDriverRepository>();
            truckDriverRepository.Setup(e => e.GetTruckDriverById(truckDriver.TruckDriverId)).Returns(truckDriver);
            var controller = new TruckDriverController(truckDriverRepository.Object);

            // Act
            var result = controller.DeleteTruckDriver(5) as ActionResult;
            var truckDriverRepoNull = new HttpNotFoundResult();

            // Assert
            Assert.AreEqual(result.ToString(), truckDriverRepoNull.ToString());
        }

        [TestMethod]
        public void Test_DeleteTruckDriver_Return_TruckDriver_And_ViewName_Correct()
        {
            // Arrange
            var truckDriver = MockTruckDriver();
            var truckDriverRepository = new Mock<ITruckDriverRepository>();
            truckDriverRepository.Setup(e => e.GetTruckDriverById(1)).Returns(truckDriver);
            var controller = new TruckDriverController(truckDriverRepository.Object);

            // Act
            var result = controller.DeleteTruckDriver(1) as ViewResult;
            var viewName = result.ViewName;

            // Assert Staff Id Null
            Assert.AreEqual(viewName, "DeleteTruckDriver");
        }


        //DeleteStaffConfirmed-------------------------------------------------------------
        [TestMethod]
        public void Test_DeleteTruckDriverConfirmed_Return_ListAllTruckDriversViewName_Correct_When_TruckDriverRepo_Return_true()
        {
            // Arrange
            var truckDriver = MockTruckDriver();
            var truckDriverRepository = new Mock<ITruckDriverRepository>();
            truckDriverRepository.Setup(e => e.DeleteTruckDriver(truckDriver.TruckDriverId)).Returns(true);
            var controller = new TruckDriverController(truckDriverRepository.Object);

            // Act
            var result = controller.DeleteTruckDriverConfirmed(truckDriver.TruckDriverId) as RedirectToRouteResult;
            var redirectViewName = result.RouteValues["Action"];

            // Assert
            Assert.AreEqual(redirectViewName, "ListAllTruckDrivers");
        }

        [TestMethod]
        public void Test_DeleteTruckDriverConfirmed_Return_DeleteTruckDriverViewName_Correct_When_TruckDriverRepo_Return_false()
        {
            // Arrange
            var truckDriver = MockTruckDriver();
            var truckDriverRepository = new Mock<ITruckDriverRepository>();
            truckDriverRepository.Setup(e => e.DeleteTruckDriver(truckDriver.TruckDriverId)).Returns(false);
            var controller = new TruckDriverController(truckDriverRepository.Object);

            // Act
            var result = controller.DeleteTruckDriverConfirmed(truckDriver.TruckDriverId) as ViewResult;
            var redirectViewName = result.ViewName;

            // Assert
            Assert.AreEqual(redirectViewName, "DeleteTruckDriver");
        }




        // List TruckDriver
        private IEnumerable<TruckDriver> MockListTruckDrivers()
        {
            var truckdriverList = new TruckDriver[]
            {
                new TruckDriver
                {
                    TruckDriverId = 1,
                    TruckDriverUsername = "DriverA",
                    TruckDriverPassword = "1234",
                    TruckDriverConfirmPassword = "1234",
                    TruckDriverFullname = "DriverA",
                    TruckDriverCitizenId = "1579900617731",
                    TruckDriverDriverLicenseId = "12345678",
                    TruckDriverGender = "Male",
                    TruckDriverAddress = "264 Chiang Saen Chiang Rai",
                    TruckDriverBirthdate = new DateTime(2010, 3, 11).ToString(CultureInfo.CurrentCulture),
                    TruckDriverEmail = "DriverA@gmail.com",
                    TruckDriverTelephoneNo = "0970747125",
                    TruckId = "CR-12345"

                },
                new TruckDriver
                {
                    TruckDriverId = 2,
                    TruckDriverUsername = "DriverB",
                    TruckDriverPassword = "Driver123",
                    TruckDriverConfirmPassword = "Driver123",
                    TruckDriverFullname = "DriverB",
                    TruckDriverCitizenId = "1234567894123",
                    TruckDriverDriverLicenseId = "56287456",
                    TruckDriverGender = "Male",
                    TruckDriverAddress = "92 Chiang Saen Chiang Rai",
                    TruckDriverBirthdate = new DateTime(2010, 3, 11).ToString(CultureInfo.CurrentCulture),
                    TruckDriverEmail = "DriverB@gmail.com",
                    TruckDriverTelephoneNo = "0810273809",
                    TruckId = "CM-1457"

                }
            };

            return truckdriverList;
        }

        private TruckDriver MockTruckDriver()
        {
            TruckDriver truckDriver = new TruckDriver
            {
                TruckDriverId = 1,
                TruckDriverUsername = "DriverA",
                TruckDriverPassword = "1234",
                TruckDriverConfirmPassword = "1234",
                TruckDriverFullname = "DriverA",
                TruckDriverCitizenId = "1579900617731",
                TruckDriverDriverLicenseId = "12345678",
                TruckDriverGender = "Male",
                TruckDriverAddress = "264 Chiang Saen Chiang Rai",
                TruckDriverBirthdate = new DateTime(2010, 3, 11).ToString(CultureInfo.CurrentCulture),
                TruckDriverEmail = "DriverA@gmail.com",
                TruckDriverTelephoneNo = "0970747125",
                TruckId = "CR-12345"

            };

            return truckDriver;
        }

    }
}
