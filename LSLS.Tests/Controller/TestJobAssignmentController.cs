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
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace LSLS.Tests.Controller
{
    public class TestJobAssignmentController
    {

        [Fact]
        public void ListAllJobAssignments_ShouldReturnActionResult()
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


            // Arrange
            var controller = new JobAssignmentController(mockJob.Object, mockTruckDriver.Object);
            // Act
            var result = controller.ListAllJobAssignments() as ActionResult;
            var listResult = controller.ListAllJobAssignments();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));

            // Action             
            JobAssignment[] resultA = ((IEnumerable<JobAssignment>)controller.ListAllJobAssignments().ViewData.Model).ToArray();

            // Assert             
            Assert.AreEqual(resultA.Length, 2);
            Assert.AreEqual(1, resultA[0].JobAssignmentId);
            Assert.AreEqual(2, resultA[1].JobAssignmentId);

        }

        [Fact]
        public void DetailsJobAssignment_ShouldReturnJobAssignment()
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


            // Arrange
            var controller = new JobAssignmentController(mockJob.Object, mockTruckDriver.Object);
            
            //Act id = 1
            var resultA = controller.DetailsJobAssignment(1);

            //Assert id = 1
            Assert.IsNotNull(resultA);


            //Act id = null & job = null
            var resultB = controller.DetailsJobAssignment(null) as ActionResult;

            //Assert id = null & job = null
            Assert.IsNotNull(resultB);
        }

        [Fact]
        public void FormEditJobAssignmentTest()
        {
            var mockJob = new Mock<IJobAssignmentRepository>();
            var mockTruckDriver = new Mock<ITruckDriverRepository>();

            /*
            JobAssignment jobAssignment = new JobAssignment { ShippingId = 1 };
            List<TruckDriver> list = new List<TruckDriver>();

            var jobViewModel = new FormJobAssignmentViewModel
            {
                JobAssignment = jobAssignment,
                TruckDrivers = list,
             };
             */
           
            // Arrange
            var controller = new JobAssignmentController(mockJob.Object, mockTruckDriver.Object);

            var resultA = controller.FormEditJobAssignment(1) as ActionResult;
            Assert.IsInstanceOfType(resultA, typeof(ActionResult));
            Assert.IsNotNull(resultA);

            var resultB = controller.FormEditJobAssignment(null);
            Assert.IsInstanceOfType(resultB, typeof(ActionResult));


            //Assert.AreEqual(1,jobViewModel.JobAssignment.ShippingId);
        }

        [Fact]
        public void EditJobAssignmentTest()
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

            var jobViewModel = new FormJobAssignmentViewModel
            {
                JobAssignment = job,
                TruckDrivers = list,
            };

            var mockJob = new Mock<IJobAssignmentRepository>();
            mockJob.Setup(j => j.UpdateJobAssignment(jobViewModel)).Returns(true);
            var mockTruckDriver = new Mock<ITruckDriverRepository>();

            
           

            var controller = new JobAssignmentController(mockJob.Object, mockTruckDriver.Object);

            // Arrange - add an error to the model state     
            controller.ModelState.AddModelError("error", "error");
            // Act - try to save the product     
            ActionResult result = controller.EditJobAssignment(jobViewModel);

            // Assert - check that the repository was not called     
            Assert.IsNotNull(result);

            // Assert - check the method result type     
            Assert.IsInstanceOfType(result, typeof(ActionResult));

        }


    }
}
