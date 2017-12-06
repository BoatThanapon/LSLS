using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LSLS.Controllers;
using LSLS.Repository.Abstract;
using LSLS.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LSLS.Tests.Controller
{
    [TestClass]
    public class TruckTrackingControllerTest
    {
        // TruckTracking -------------------------------------------------------

        [TestMethod]
        public void Test_TruckTracking_Return_List_And_ViewName_Correct()
        {
            // Arrange
            var truckLocationList = MockTruckLocationList();
            var truckLocationRepository = new Mock<ITruckLocationRepository>();
            truckLocationRepository.Setup(e => e.GetAllTruckLocations()).Returns(truckLocationList.AsQueryable());
            var controller = new TruckTrackingController(truckLocationRepository.Object);

            // Act 
            var result = controller.TruckTracking() as ViewResult;
            var model = result.Model;

            // Assert
            Assert.IsNotNull(model);
            Assert.AreEqual(result.ViewName, "TruckTracking");
        }

        //GetAllTruckLocations-----------------------------------------------
        [TestMethod]
        public void Test_GetAllTruckLocations_Return_Json_Correct()
        {
            // Arrange
            var truckLocationList = MockTruckLocationList();
            var jsonMock = new JsonResult {Data = truckLocationList};

            var truckLocationRepository = new Mock<ITruckLocationRepository>();
            truckLocationRepository.Setup(e => e.GetAllTruckLocations()).Returns(truckLocationList.AsQueryable());
            var controller = new TruckTrackingController(truckLocationRepository.Object);

            // Act 
            var result = controller.GetAllTruckLocations() as JsonResult;

            var data = result.Data;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(data.ToString(), jsonMock.Data.ToString());
        }

        //SearchTruckId-----------------------------------------------------
        [TestMethod] public void Test_SearchTruckId_Return_SearchTruckId_View_When_TruckId_Is_Exist()
        {
            // Arrange
            var truckLocation = MockTruckLocationViewModel();
            var truckLocationRepository = new Mock<ITruckLocationRepository>();
            truckLocationRepository.Setup(e => e.SearchTruckLocationByTruckId(truckLocation.TruckId)).Returns(truckLocation);
            var controller = new TruckTrackingController(truckLocationRepository.Object);

            // Act 
            var result = controller.SearchTruckId(truckLocation.TruckId) as ViewResult;
            var model = result.Model;

            // Assert
            Assert.AreEqual(result.ViewName, "SearchTruckId");
            Assert.AreEqual(model, truckLocation);

        }

        [TestMethod]
        public void Test_SearchTruckId_Return_SearchTruckId_View_When_TruckId_Is_Null()
        {
            // Arrange
            var truckLocation = MockTruckLocationViewModel();
            var truckLocationRepository = new Mock<ITruckLocationRepository>();
            truckLocationRepository.Setup(e => e.SearchTruckLocationByTruckId(truckLocation.TruckId)).Returns(truckLocation);
            var controller = new TruckTrackingController(truckLocationRepository.Object);

            // Act 
            var result = controller.SearchTruckId("CM-0004") as ViewResult;

            Assert.AreEqual(result.ViewName, "TruckTracking");

        }






        public IEnumerable<TruckLocationViewModel> MockTruckLocationList()
        {
            var truckLocationlist = new TruckLocationViewModel[]
            {
                new TruckLocationViewModel
                {
                    TruckId = "CR-12345",
                    TruckDriverFullname = "DriverA",
                    Latitude = Convert.ToSingle(12.02151),
                    Longitude = Convert.ToSingle(121.02151),
                    TruckCurrentAddress = "Chiang Mai",
                    TruckCurrentTime = new DateTime(2017,12,3,8,40,00)
                },
                new TruckLocationViewModel
                {
                    TruckId = "CM-1457",
                    TruckDriverFullname = "DriverB",
                    Latitude = Convert.ToSingle(13.02151),
                    Longitude = Convert.ToSingle(98.02151),
                    TruckCurrentAddress = "Bangkok",
                    TruckCurrentTime = new DateTime(2017,8,12,5,50,30)
                }
            };

            return truckLocationlist;
        }

        public TruckLocationViewModel MockTruckLocationViewModel()
        {
            var truckLocation = new TruckLocationViewModel
            {
                TruckId = "CR-12345",
                TruckDriverFullname = "DriverA",
                Latitude = Convert.ToSingle(12.02151),
                Longitude = Convert.ToSingle(121.02151),
                TruckCurrentAddress = "Chiang Mai",
                TruckCurrentTime = new DateTime(2017, 12, 3, 8, 40, 00)
            };

            return truckLocation;
        }
    }

}
