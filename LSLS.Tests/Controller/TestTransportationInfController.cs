using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
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
    public class TestTransportationInfController
    {
        //UTC-40
        [Test]
        public void TestListAllTransportationInfs()
        {
            var mockTransInf = MockTransportationInfData();
            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();
            // Arrange
            var controller = new TransportationInfController(mockTransInf.Object, mockJob.Object, mockTruckDriver.Object);

            // Act
            var result = controller.ListAllTransportationInfs() as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual("ListAllTransportationInfs", result.ViewName);

            
            // Action             
            TransportationInf[] resultA = ((IEnumerable<TransportationInf>)controller.ListAllTransportationInfs().ViewData.Model).ToArray();

            // Assert             
            Assert.AreEqual(resultA.Length, 2);
            Assert.AreEqual(1, resultA[0].ShippingId);
            Assert.AreEqual(2, resultA[1].ShippingId);
            
        }

        //UTC-41
        [Test]
        public void DetailsTransportationInfTest()
        {
            //Arrange
            var transportationInf = TransportationInfData();

            var mockTrans = new Mock<ITransportationInfRepository>();
            mockTrans.Setup(j => j.GetTransportationInfById(1)).Returns(transportationInf);

            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();

            // Arrange
            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);


            //Act id = 1
            var resultA = controller.DetailsTransportationInf(1);

            //Assert id = 1
            Assert.IsNotNull(resultA);

            //Act id = null & job = null
            var resultB = controller.DetailsTransportationInf(null) as ActionResult;

            //Assert id = null & job = null
            Assert.IsNotNull(resultB);
        }

        //UTC-42
        [Test]
        public void FormCreateTransportationInfTest()
        {
            var mockTransInf = new Mock<ITransportationInfRepository>();
            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();

            // Arrange
            var controller = new TransportationInfController(mockTransInf.Object, mockJob.Object, mockTruckDriver.Object);

            // Act
            var result = controller.FormCreateTransportationInf() as ViewResult;

            // Assert
            Assert.AreEqual("FormCreateTransportationInf", result.ViewName);
        }

        //UTC-43
        [Test]
        public void CreateTransportationInfTest()
        {
            var transportationInf = TransportationInfData();

            var mockTrans = new Mock<ITransportationInfRepository>();
            mockTrans.Setup(j => j.AddTransportationInf(transportationInf)).Returns(true);

            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();

            // Arrange
            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);

            // Act
            var result = controller.CreateTransportationInf(transportationInf) as ActionResult;

            // Assert
            Assert.IsNotNull(result);
        }

        //UTC-44
        [Test]
        public void FormEditTransportationInfTest()
        {
            var transportationInf = TransportationInfData();

            var mockTrans = new Mock<ITransportationInfRepository>();
            mockTrans.Setup(j => j.GetTransportationInfById(1)).Returns(transportationInf);

            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();

            // Arrange
            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);

            var result = controller.FormEditTransportationInf(1) as ViewResult;
            var resultNull = controller.FormEditTransportationInf(null);

            Assert.AreEqual(result.ViewName, "FormEditTransportationInf");
            Assert.IsNotNull(result);

            Assert.IsNull(resultNull);
            
        }

        //UTC-45
        [Test]
        public void EditTransportationInfTest()
        {
            var transportationInf = TransportationInfData();

            var mockTrans = new Mock<ITransportationInfRepository>();
            mockTrans.Setup(j => j.UpdateTransportationInf(transportationInf)).Returns(true);

            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();

            // Arrange
            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);
            // Arrange - add an error to the model state     
            controller.ModelState.AddModelError("error", @"error");

            // Act - try to save the product     
            ActionResult result = controller.EditTransportationInf(transportationInf);

            // Assert - check that the repository was not called     
            Assert.IsNotNull(result);

            // Assert - check the method result type     
            if (controller.EditTransportationInf(transportationInf) is RedirectToRouteResult resultA)
                Assert.Equals(resultA.RouteValues["action"], "ListAllTransportationInfs");

        }


        //UTC-46
        [Test]
        public void DeleteTransportationInfTest()
        {
            var transportationInf = TransportationInfData();

            var mockTrans = new Mock<ITransportationInfRepository>();
            mockTrans.Setup(j => j.GetTransportationInfById(1)).Returns(transportationInf);

            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();

            // Arrange
            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);

            var result = controller.DeleteTransportationInf(1) as ViewResult;
            var resultNull = controller.DeleteTransportationInf(null) as ActionResult;


            //Assert
            Assert.IsNotNull(result);

            Assert.AreEqual(result.ViewName, "DeleteTransportationInf");

            Assert.IsNull(resultNull);


        }

        //UTC-47
        [Test]
        public void DeleteTransportationInfConfirmedTest()
        {
            var transportationInf = TransportationInfData();

            var mockTrans = new Mock<ITransportationInfRepository>();
            mockTrans.Setup(j => j.DeleteTransportationInf(transportationInf.ShippingId)).Returns(true);

            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();

            // Arrange
            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);

            var result = controller.DeleteTransportationInfConfirmed(1);

            //Assert
            Assert.IsNotNull(result);            

            if (controller.DeleteTransportationInfConfirmed(1) is RedirectToRouteResult resultA)
                Assert.AreEqual(resultA.RouteValues["action"], "ListAllTransportationInfs");


        }

        //UTC-48
        [Test]
        public void FormCreateJobAssignmentTest()
        {
            var transportationInf = TransportationInfData();

            var mockTrans = new Mock<ITransportationInfRepository>();
            mockTrans.Setup(j => j.GetTransportationInfById(1)).Returns(transportationInf);

            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();

            // Arrange
            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);

            var result = controller.FormCreateJobAssignment(1) as ViewResult;
            var resultNull = controller.FormCreateJobAssignment(null) as ActionResult;

            //Assert
            Assert.IsNotNull(result);

            Assert.AreEqual(result.ViewName, "FormCreateJobAssignment");

            Assert.IsNull(resultNull);


        }

        //UTC-49
        [Test]
        public void CreateJobAssignmentTest()
        {
            var jobAssignmentViewModel = MockViewModelData();

            var mockTrans = new Mock<ITransportationInfRepository>();
            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();
            mockJob.Setup(j => j.AddJobAssignment(jobAssignmentViewModel)).Returns(true);

            // Arrange
            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);

            var result = controller.CreateJobAssignment(jobAssignmentViewModel) as ActionResult;

            //Assert
            Assert.IsNotNull(result);

            if (controller.CreateJobAssignment(jobAssignmentViewModel) is RedirectToRouteResult resultA)
                Assert.AreEqual(resultA.RouteValues["action"], "ListAllTransportationInfs");
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
        [TestInitialize]
        public Mock<ITransportationInfRepository> MockTransportationInfData()
        {
            var mockTransportationInf = new Mock<ITransportationInfRepository>();
            mockTransportationInf.Setup(m => m.GetAllTransportationInfs()).Returns(new TransportationInf[] {
                new TransportationInf()
                {
                    ShippingId = 1,
                    OrderDate = new DateTime(2017, 10, 01).ToString(),
                    DateOfTransportation = new DateTime(2017, 11, 13).ToString(),
                    ProductName = "Computer",
                    StartingPoint = "Bangkok",
                    Destination = "Hua Hin",
                    Employer = " LD Logistic Co,Ltd.",
                    ReceiverName = "Thanapon",
                    StatusOfTransportation = false,
                    JobIsActive = true,
                    ReceiveDateTime = new DateTime(2017, 10, 05 , 4, 40, 29),
                    ShippingDocImage = null,
                    ShippingNote = "accident"
                },
                new TransportationInf()
                {
                    ShippingId = 2,
                    OrderDate = new DateTime(2017, 10, 04).ToString(),
                    DateOfTransportation = new DateTime(2017, 10, 20).ToString(),
                    ProductName = "Organic PlantC",
                    StartingPoint = "Saraburi",
                    Destination = " Chiangmai ",
                    Employer = "Chai-Jha-Learn",
                    ReceiverName = "Somjit",
                    StatusOfTransportation = false,
                    JobIsActive = true,
                    ReceiveDateTime = new DateTime(2017, 10, 05 , 4, 40, 29),
                    ShippingDocImage = null,
                    ShippingNote = "accident"
                }
            }.AsQueryable());

            return mockTransportationInf;
        }

        [TestInitialize]
        public TransportationInf TransportationInfData()
        {
            var transportationInf = new TransportationInf()
            {
                ShippingId = 1,
                OrderDate = new DateTime(2017, 10, 01).ToString(),
                DateOfTransportation = new DateTime(2017, 11, 13).ToString(),
                ProductName = "Computer",
                StartingPoint = "Bangkok",
                Destination = "Hua Hin",
                Employer = " LD Logistic Co,Ltd.",
                ReceiverName = "Thanapon",
                StatusOfTransportation = false,
                JobIsActive = true,
                ReceiveDateTime = new DateTime(2017, 10, 05, 4, 40, 29),
                ShippingDocImage = null,
                ShippingNote = "accident"
            };


            return transportationInf;
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
