using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LSLS.Controllers;
using LSLS.Models;
using LSLS.Repository.Abstract;
using LSLS.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LSLS.Tests.Controller
{
    [TestClass]
    public class JobAssignmentControllerTest
    {

        //ListAllJobAssignments--------------------------------------
        [TestMethod]
        public void Test_ListAllJobAssignments_Return_List_And_ViewName_Correct()
        {
            // Arrange
            var jobList = MockJobAssignmentsList();
            var jobAssignmentRepository = new Mock<IJobAssignmentRepository>();
            jobAssignmentRepository.Setup(e => e.GetAllJobAssignments()).Returns(jobList.AsQueryable());

            var truckDriverList = MockListTruckDrivers();
            var truckDriverRepository = new Mock<ITruckDriverRepository>();
            truckDriverRepository.Setup(e => e.GetAllTruckDrivers()).Returns(truckDriverList.AsQueryable());


            var controller = new JobAssignmentController(jobAssignmentRepository.Object,truckDriverRepository.Object);

            // Act 
            var result = controller.ListAllJobAssignments() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.ViewName, "ListAllJobAssignments");
        }


        //FormEditJobAssignment--------------------------------------
        [TestMethod]
        public void Test_FormEditJobAssignment_Return_BadRequest_When_JobId_Is_Null()
        {
            // Arrange
            var jobAssignmentRepository = new Mock<IJobAssignmentRepository>();
            var truckDriverRepository = new Mock<ITruckDriverRepository>();

            var controller = new JobAssignmentController(jobAssignmentRepository.Object, truckDriverRepository.Object);

            // Act 
            var result = controller.FormEditJobAssignment(null) as ActionResult;
            var jobNull = new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // Assert
            Assert.AreEqual(result.ToString(), jobNull.ToString());
        }

        [TestMethod]
        public void Test_FormEditJobAssignment_Return_HttpNotFound_When_JobRepo_Return_Null()
        {
            // Arrange
            var job = MockJob();
            var jobAssignmentRepository = new Mock<IJobAssignmentRepository>();
            jobAssignmentRepository.Setup(e => e.GetJobAssignmentById(job.JobAssignmentId)).Returns(job);

            var truckDriverRepository = new Mock<ITruckDriverRepository>();

            var controller = new JobAssignmentController(jobAssignmentRepository.Object, truckDriverRepository.Object);

            // Act 
            var result = controller.FormEditJobAssignment(5) as ActionResult;
            var jobRepoNull = new HttpNotFoundResult();

            // Assert
            Assert.AreEqual(result.ToString(), jobRepoNull.ToString());
        }

        [TestMethod]
        public void Test_FormEditJobAssignment_Return_Job_And_View_When_Job_Exist()
        {
            // Arrange
            var job = MockJob();
            var jobAssignmentRepository = new Mock<IJobAssignmentRepository>();
            jobAssignmentRepository.Setup(e => e.GetJobAssignmentById(job.JobAssignmentId)).Returns(job);

            var truckDriverRepository = new Mock<ITruckDriverRepository>();

            var controller = new JobAssignmentController(jobAssignmentRepository.Object, truckDriverRepository.Object);

            // Act 
            var result = controller.FormEditJobAssignment(1) as ViewResult;
            var viewName = result.ViewName;
            var model = result.Model as FormJobAssignmentViewModel;

            // Assert
            Assert.AreEqual(viewName, "FormEditJobAssignment");
            Assert.IsNotNull(model);
        }

        //EditJobAssignment-------------------------------------------------------------------

        [TestMethod]
        public void Test_EditJobAssignment_Return_Job_And_View_When_ModelState_Is_Not_Valid()
        {
            // Arrange
            var job = MockJobViewModelData();
            job.JobAssignment.DestinationJob = null;
            var jobAssignmentRepository = new Mock<IJobAssignmentRepository>();
            jobAssignmentRepository.Setup(e => e.UpdateJobAssignment(job)).Returns(true);

            var truckDriverRepository = new Mock<ITruckDriverRepository>();

            var controller = new JobAssignmentController(jobAssignmentRepository.Object, truckDriverRepository.Object);
            controller.ModelState.AddModelError("fakeError", "");

            // Act
            var result = controller.EditJobAssignment(job) as ViewResult;
            var modelView = result.Model as FormJobAssignmentViewModel;
            var viewName = result.ViewName;

            // Assert
            Assert.AreEqual(viewName, "FormEditJobAssignment");
            Assert.IsNotNull(modelView);
        }

        [TestMethod]
        public void Test_EditJobAssignment_Return_ListAllJobAssignments_When_JobRepo_Return_True()
        {
            // Arrange
            var job = MockJobViewModelData();

            var jobAssignmentRepository = new Mock<IJobAssignmentRepository>();
            jobAssignmentRepository.Setup(e => e.UpdateJobAssignment(job)).Returns(true);

            var truckDriverRepository = new Mock<ITruckDriverRepository>();
            var controller = new JobAssignmentController(jobAssignmentRepository.Object, truckDriverRepository.Object);


            // Act
            var result = controller.EditJobAssignment(job) as RedirectToRouteResult;
            var redirectViewName = result.RouteValues["Action"];

            // Assert
            Assert.AreEqual(redirectViewName, "ListAllJobAssignments");
        }


        [TestMethod]
        public void Test_EditJobAssignment_Return_FormEditJobAssignment_When_JobIsNull_And_JobRepo_Return_False()
        {
            // Arrange
            var job = new FormJobAssignmentViewModel();

            var jobAssignmentRepository = new Mock<IJobAssignmentRepository>();
            jobAssignmentRepository.Setup(e => e.UpdateJobAssignment(job)).Returns(false);

            var truckDriverRepository = new Mock<ITruckDriverRepository>();
            var controller = new JobAssignmentController(jobAssignmentRepository.Object, truckDriverRepository.Object);


            // Act
            var result = controller.EditJobAssignment(job) as ViewResult;
            var modelView = result.Model as FormJobAssignmentViewModel;
            var viewName = result.ViewName;

            // Assert
            Assert.AreEqual(viewName, "FormEditJobAssignment");
            Assert.IsNotNull(modelView);
        }

        //DetailsJobAssignment--------------------------------------
        [TestMethod]
        public void Test_DetailsJobAssignment_Return_BadRequest_When_JobId_Is_Null()
        {
            // Arrange
            var jobAssignmentRepository = new Mock<IJobAssignmentRepository>();
            var truckDriverRepository = new Mock<ITruckDriverRepository>();

            var controller = new JobAssignmentController(jobAssignmentRepository.Object, truckDriverRepository.Object);

            // Act 
            var result = controller.DetailsJobAssignment(null) as ActionResult;
            var jobNull = new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // Assert
            Assert.AreEqual(result.ToString(), jobNull.ToString());
        }

        [TestMethod]
        public void Test_DetailsJobAssignment_Return_HttpNotFound_When_JobRepo_Return_Null()
        {
            // Arrange
            var job = MockJob();
            var jobAssignmentRepository = new Mock<IJobAssignmentRepository>();
            jobAssignmentRepository.Setup(e => e.GetJobAssignmentById(job.JobAssignmentId)).Returns(job);

            var truckDriverRepository = new Mock<ITruckDriverRepository>();

            var controller = new JobAssignmentController(jobAssignmentRepository.Object, truckDriverRepository.Object);

            // Act 
            var result = controller.DetailsJobAssignment(5) as ActionResult;
            var jobRepoNull = new HttpNotFoundResult();

            // Assert
            Assert.AreEqual(result.ToString(), jobRepoNull.ToString());
        }

        [TestMethod]
        public void Test_DetailsJobAssignment_Return_Job_And_View_When_Job_Exist()
        {
            // Arrange
            var job = MockJob();
            var jobAssignmentRepository = new Mock<IJobAssignmentRepository>();
            jobAssignmentRepository.Setup(e => e.GetJobAssignmentById(job.JobAssignmentId)).Returns(job);

            var truckDriverRepository = new Mock<ITruckDriverRepository>();

            var controller = new JobAssignmentController(jobAssignmentRepository.Object, truckDriverRepository.Object);

            // Act 
            var result = controller.DetailsJobAssignment(1) as ViewResult;
            var viewName = result.ViewName;
            var model = result.Model as FormJobAssignmentViewModel;

            // Assert
            Assert.AreEqual(viewName, "DetailsJobAssignment");
            Assert.IsNotNull(model);
        }



        //DeleteJobAssignment--------------------------------------
        [TestMethod]
        public void Test_DeleteJobAssignment_Return_BadRequest_When_JobId_Is_Null()
        {
            // Arrange
            var jobAssignmentRepository = new Mock<IJobAssignmentRepository>();
            var truckDriverRepository = new Mock<ITruckDriverRepository>();

            var controller = new JobAssignmentController(jobAssignmentRepository.Object, truckDriverRepository.Object);

            // Act 
            var result = controller.DeleteJobAssignment(null) as ActionResult;
            var jobNull = new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // Assert
            Assert.AreEqual(result.ToString(), jobNull.ToString());
        }

        [TestMethod]
        public void Test_DeleteJobAssignment_Return_HttpNotFound_When_JobRepo_Return_Null()
        {
            // Arrange
            var job = MockJob();
            var jobAssignmentRepository = new Mock<IJobAssignmentRepository>();
            jobAssignmentRepository.Setup(e => e.GetJobAssignmentById(job.JobAssignmentId)).Returns(job);

            var truckDriverRepository = new Mock<ITruckDriverRepository>();

            var controller = new JobAssignmentController(jobAssignmentRepository.Object, truckDriverRepository.Object);

            // Act 
            var result = controller.DeleteJobAssignment(5) as ActionResult;
            var jobRepoNull = new HttpNotFoundResult();

            // Assert
            Assert.AreEqual(result.ToString(), jobRepoNull.ToString());
        }

        [TestMethod]
        public void Test_DeleteJobAssignment_Return_Job_And_View_When_Job_Exist()
        {
            // Arrange
            var job = MockJob();
            var jobAssignmentRepository = new Mock<IJobAssignmentRepository>();
            jobAssignmentRepository.Setup(e => e.GetJobAssignmentById(job.JobAssignmentId)).Returns(job);

            var truckDriverRepository = new Mock<ITruckDriverRepository>();

            var controller = new JobAssignmentController(jobAssignmentRepository.Object, truckDriverRepository.Object);

            // Act 
            var result = controller.DeleteJobAssignment(job.JobAssignmentId) as ViewResult;
            var viewName = result.ViewName;
            var model = result.Model as JobAssignment;

            // Assert
            Assert.AreEqual(viewName, "DeleteJobAssignment");
            Assert.IsNotNull(model);
        }


        //DeleteJobAssignmentConfirmed-------------------------------------------------------------
        [TestMethod]
        public void Test_DeleteJobAssignmentConfirmed_Return_ListAllJobAssignmentsViewName_When_JobRepo_Return_true()
        {
            // Arrange
            var job = MockJob();
            var jobAssignmentRepository = new Mock<IJobAssignmentRepository>();
            jobAssignmentRepository.Setup(e => e.DeleteJobAssignment(job.JobAssignmentId)).Returns(true);

            var truckDriverRepository = new Mock<ITruckDriverRepository>();

            var controller = new JobAssignmentController(jobAssignmentRepository.Object, truckDriverRepository.Object);

            // Act
            var result = controller.DeleteJobAssignmentConfirmed(job.JobAssignmentId) as RedirectToRouteResult;
            var redirectViewName = result.RouteValues["Action"];

            // Assert
            Assert.AreEqual(redirectViewName, "ListAllJobAssignments");
        }

        [TestMethod]
        public void Test_DeleteTruckDriverConfirmed_Return_TruckDriver_And_ViewName_Correct_When_TruckDriverRepo_Return_false()
        {
            // Arrange
            var job = MockJob();
            var jobAssignmentRepository = new Mock<IJobAssignmentRepository>();
            jobAssignmentRepository.Setup(e => e.DeleteJobAssignment(job.JobAssignmentId)).Returns(false);

            var truckDriverRepository = new Mock<ITruckDriverRepository>();

            var controller = new JobAssignmentController(jobAssignmentRepository.Object, truckDriverRepository.Object);

            // Act
            var result = controller.DeleteJobAssignmentConfirmed(5) as ViewResult;
            var redirectViewName = result.ViewName;

            // Assert
            Assert.AreEqual(redirectViewName, "DeleteJobAssignment");
        }








        // List Job
        public IEnumerable<JobAssignment> MockJobAssignmentsList()
        {
            var listJob = new JobAssignment[]
            {
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
            };

            return listJob;
        }


        // Mock Job 
        private JobAssignment MockJob()
        {
            var job = new JobAssignment()
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
            };

            return job;
        }

        // Mock FormJobAssignmentViewModel
        private FormJobAssignmentViewModel MockJobViewModelData()
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




    }
}
