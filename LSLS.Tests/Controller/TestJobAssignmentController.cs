using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using LSLS.Controllers;
using LSLS.Models;
using LSLS.Repository.Abstract;
using LSLS.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace LSLS.Tests.Controller
{
    public class TestJobAssignmentController
    {

        //UTC-34
        [Test]
        public void ListAllJobAssignmentsTest()
        {

            var jobAssignmentData = MockJobAssignmentData();
            var truckDriverData = MockTruckDriverData();

            // Arrange
            var controller = new JobAssignmentController(jobAssignmentData.Object, truckDriverData.Object);
            // Act
            var result = controller.ListAllJobAssignments() as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual("ListAllJobAssignments", result.ViewName);


            // Action             
            JobAssignment[] resultA = ((IEnumerable<JobAssignment>)controller.ListAllJobAssignments().ViewData.Model).ToArray();

            // Assert             
            Assert.AreEqual(resultA.Length, 2);
            Assert.AreEqual(1, resultA[0].JobAssignmentId);
            Assert.AreEqual(2, resultA[1].JobAssignmentId);

        }

        //UTC-35
        [Test]
        public void DetailsJobAssignment_ShouldReturnJobAssignment()
        {

            var jobAssignmentData = MockJobAssignmentData();
            var truckDriverData = MockTruckDriverData();

            // Arrange
            var controller = new JobAssignmentController(jobAssignmentData.Object, truckDriverData.Object);

            //Act id = 1
            var resultA = controller.DetailsJobAssignment(1);

            //Assert id = 1
            Assert.IsNotNull(resultA);

            //Act id = null & job = null
            var resultB = controller.DetailsJobAssignment(null) as ActionResult;

            //Assert id = null & job = null
            Assert.IsNotNull(resultB);
        }

        //UTC-36
        [Test]
        public void FormEditJobAssignmentTest()
        {
            var jobViewModel = MockViewModelData();
            var mockJob = new Mock<IJobAssignmentRepository>();
            mockJob.Setup(j => j.GetJobAssignmentById(jobViewModel.JobAssignment.JobAssignmentId)).Returns(new JobAssignment());

            var mockTruckDriver = new Mock<ITruckDriverRepository>();

            // Arrange
            var controller = new JobAssignmentController(mockJob.Object, mockTruckDriver.Object);

            var result = controller.FormEditJobAssignment(1) as ViewResult;

            Assert.AreEqual("FormEditJobAssignment", result.ViewName);
            Assert.IsNotNull(result);

        }

        //UTC-37
        [Test]
        public void EditJobAssignmentTest()
        {
            //Arrange
            var jobViewModel = MockViewModelData();

            var mockJob = new Mock<IJobAssignmentRepository>();
            mockJob.Setup(j => j.UpdateJobAssignment(jobViewModel)).Returns(true);
            var mockTruckDriver = new Mock<ITruckDriverRepository>();

            
           

            var controller = new JobAssignmentController(mockJob.Object, mockTruckDriver.Object);

            // Arrange - add an error to the model state     
            controller.ModelState.AddModelError("error", @"error");
            // Act - try to save the product     
            ActionResult result = controller.EditJobAssignment(jobViewModel);

            // Assert - check that the repository was not called     
            Assert.IsNotNull(result);

            // Assert - check the method result type     
            if (controller.EditJobAssignment(jobViewModel) is RedirectToRouteResult resultA)
                Assert.Equals(resultA.RouteValues["action"], "ListAllJobAssignments");
        }


        //UTC-38
        [Test]
        public void DeleteJobAssignmentTest()
        {
            //Arrange
            var jobViewModel = MockViewModelData();
            var mockJob = new Mock<IJobAssignmentRepository>();
                mockJob.Setup(j => j.GetJobAssignmentById(jobViewModel.JobAssignment.JobAssignmentId)).Returns(new JobAssignment());

            var mockTruckDriver = new Mock<ITruckDriverRepository>();

            var controller = new JobAssignmentController(mockJob.Object, mockTruckDriver.Object);

            var result = controller.DeleteJobAssignment(1) as ViewResult;

            //Assert
            Assert.IsNotNull(result);

            Assert.AreEqual(result.ViewName, "DeleteJobAssignment");

        }

        //UTC-39
        [Test]
        public void DeleteJobAssignmentConfirmedTest()
        {
            //Arrange
            var job = new JobAssignment()
            {
                JobAssignmentId = 1,
                ShippingId = 1,
                TruckDriverId = 1,
                JobAssignmentDate = new DateTime(2016, 7, 2).ToString(CultureInfo.CurrentCulture),
                StartingPointJob = "Bangkok",
                LatitudeStartJob = (float)12.1215,
                LongitudeStartJob = (float)98.148,
                DestinationJob = "Chiang Mai",
                LatitudeDesJob = (float)16.1245,
                LongitudeDesJob = (float)100.1454,
                JobAssignmentStatus = true
            };

            var mockJob = new Mock<IJobAssignmentRepository>();
            mockJob.Setup(j => j.DeleteJobAssignment(job.JobAssignmentId)).Returns(true);

            var mockTruckDriver = new Mock<ITruckDriverRepository>();

            var controller = new JobAssignmentController(mockJob.Object, mockTruckDriver.Object);

            var result = controller.DeleteJobAssignmentConfirmed(1) as ActionResult;

            //Assert
            Assert.IsNotNull(result);

            if (controller.DeleteJobAssignmentConfirmed(1) is RedirectToRouteResult resultB)
                Assert.AreEqual(resultB.RouteValues["action"], "ListAllJobAssignments");

        }

        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //

        [TestInitialize]
        public Mock<IJobAssignmentRepository> MockJobAssignmentData()
        {
            var mockJob = new Mock<IJobAssignmentRepository>();
            mockJob.Setup(m => m.GetAllJobAssignments()).Returns(new JobAssignment[] {
                new JobAssignment()
                {
                    JobAssignmentId = 1,
                    ShippingId = 1,
                    TruckDriverId = 1,
                    JobAssignmentDate = new DateTime(2016, 7, 2).ToString(CultureInfo.CurrentCulture),
                    StartingPointJob = "Bangkok",
                    LatitudeStartJob = (float) 12.1215,
                    LongitudeStartJob = (float) 98.148,
                    DestinationJob = "Chiang Mai",
                    LatitudeDesJob = (float) 16.1245,
                    LongitudeDesJob = (float) 100.1454,
                    JobAssignmentStatus = true
                },
                new JobAssignment()
                {
                    JobAssignmentId = 2,
                    ShippingId = 2,
                    TruckDriverId = 2,
                    JobAssignmentDate = new DateTime(2017, 10, 01).ToString(CultureInfo.CurrentCulture),
                    StartingPointJob = "Chiang Rai",
                    LatitudeStartJob = (float) 15.2154,
                    LongitudeStartJob = (float) 97.148,
                    DestinationJob = "Chonburi",
                    LatitudeDesJob = (float) 13.1245,
                    LongitudeDesJob = (float) 92.1454,
                    JobAssignmentStatus = false
                }
            }.AsQueryable());



            return mockJob;
        }

        [TestInitialize]
        public Mock<ITruckDriverRepository> MockTruckDriverData()
        {

            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            mockTruckDriver.Setup(m => m.GetAllTruckDrivers()).Returns(new TruckDriver[] {
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
            }.AsQueryable());

            return mockTruckDriver;
        }

        [TestInitialize]
        public FormJobAssignmentViewModel MockViewModelData()
        {
            //Arrage
            var job = new JobAssignment()
            {
                JobAssignmentId = 1,
                ShippingId = 1,
                TruckDriverId = 1,
                JobAssignmentDate = new DateTime(2016, 7, 2).ToString(CultureInfo.CurrentCulture),
                StartingPointJob = "Bangkok",
                LatitudeStartJob = (float)12.1215,
                LongitudeStartJob = (float)98.148,
                DestinationJob = "Chiang Mai",
                LatitudeDesJob = (float)16.1245,
                LongitudeDesJob = (float)100.1454,
                JobAssignmentStatus = true
            };


            List<TruckDriver> list = new List<TruckDriver>();

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

            list.Add(truckDriver);

            var jobViewModel = new FormJobAssignmentViewModel
            {
                JobAssignment = job,
                TruckDrivers = list,
            };

            return jobViewModel;
        }
    }
}
