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
    public class TransportationInfControllerTest
    {

        //ListAllTransportationInfs-------------------------------------------------------------
        [TestMethod]
        public void Test_ListAllTransportationInfs_Return_ListAllTransportationInfs_ViewName()
        {
            //Arrage
            var transportationInfList = MockListTransportationInfs();
            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();

            var mockTrans = new Mock<ITransportationInfRepository>();
            mockTrans.Setup(m => m.GetAllTransportationInfs()).Returns(transportationInfList.AsQueryable());

            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);

            // Act 
            var result = controller.ListAllTransportationInfs() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.ViewName, "ListAllTransportationInfs");
        }

        //DetailsTransportationInf----------------------------------------------------------------------
        [TestMethod]
        public void Test_DetailsTransportationInf_Return_HttpBadRequest_When_ShippingId_Equal_Null()
        {
            //Arrage
            var transportationInf = new TransportationInf();
            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();

            var mockTrans = new Mock<ITransportationInfRepository>();
            mockTrans.Setup(m => m.GetTransportationInfById(null)).Returns(transportationInf);

            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);

            // Act
            var result = controller.DetailsTransportationInf(null) as ActionResult;
            var shippingIdNull = new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // Assert
            Assert.AreEqual(result.ToString(), shippingIdNull.ToString());
        }

        [TestMethod]
        public void Test_DetailsTransportationInf_Return_HttpNotFound_When_TransRepo_Equal_Null()
        {
            //Arrage
            var transportationInf = MockTransportationInf();
            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();

            var mockTrans = new Mock<ITransportationInfRepository>();
            mockTrans.Setup(m => m.GetTransportationInfById(transportationInf.ShippingId)).Returns(transportationInf);

            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);

            // Act
            var result = controller.DetailsTransportationInf(5) as ActionResult;
            var shippingIdNull = new HttpNotFoundResult();

            // Assert
            Assert.AreEqual(result.ToString(), shippingIdNull.ToString());
        }

        [TestMethod]
        public void Test_DetailsTransportationInf_Return_Shipping_And_ViewName_Correct()
        {
            //Arrage
            var transportationInf = MockTransportationInf();
            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();

            var mockTrans = new Mock<ITransportationInfRepository>();
            mockTrans.Setup(m => m.GetTransportationInfById(transportationInf.ShippingId)).Returns(transportationInf);

            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);

            // Act
            var result = controller.DetailsTransportationInf(1) as ViewResult;
            var viewName = result.ViewName;

            // Assert       
            Assert.AreEqual(viewName, "DetailsTransportationInf");
        }

        //FormCreateTransportationInf----------------------------------------------------------------------
        [TestMethod]
        public void Test_FormCreateTransportationInf_Return_FormCreateTransportationInf_ViewName()
        {
            //Arrage
            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();
            var mockTrans = new Mock<ITransportationInfRepository>();

            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);

            // Act
            var result = controller.FormCreateTransportationInf();
            var viewName = result.ViewName;

            // Assert       
            Assert.AreEqual(viewName, "FormCreateTransportationInf");
        }


        //CreateTransportationInf-----------------------------------------------------------------------------
        [TestMethod]
        public void Test_CreateTransportationInf_Return_Shipping_And_View_When_ModelState_Is_Not_Valid()
        {
            //Arrage
            var transportationInf = MockTransportationInf();
            transportationInf.Destination = null;
            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();

            var mockTrans = new Mock<ITransportationInfRepository>();
            mockTrans.Setup(m => m.AddTransportationInf(transportationInf)).Returns(true);

            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);
            controller.ModelState.AddModelError("fakeError", "");

            // Act
            var result = controller.CreateTransportationInf(transportationInf) as ViewResult;
            var viewName = result.ViewName;

            // Assert
            Assert.AreEqual(viewName, "FormCreateTransportationInf");
        }

        [TestMethod]
        public void Test_CreateTransportationInf_Return_ListAllTransportationInfsView_When_ModelState_Is__Valid_And_Repo_Equal_True()
        {
            //Arrage
            var transportationInf = MockTransportationInf();
            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();

            var mockTrans = new Mock<ITransportationInfRepository>();
            mockTrans.Setup(m => m.AddTransportationInf(transportationInf)).Returns(true);

            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);

            // Act
            var result = controller.CreateTransportationInf(transportationInf) as RedirectToRouteResult;
            var redirectViewName = result.RouteValues["Action"];

            // Assert
            Assert.AreEqual(redirectViewName, "ListAllTransportationInfs");
        }

        [TestMethod]
        public void Test_CreateTransportationInf_Return_ListAllTransportationInfsView_When_ModelState_Is__Valid_And_Repo_Equal_False()
        {
            //Arrage
            var transportationInf = new TransportationInf();
            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();

            var mockTrans = new Mock<ITransportationInfRepository>();
            mockTrans.Setup(m => m.AddTransportationInf(transportationInf)).Returns(false);

            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);

            // Act
            var result = controller.CreateTransportationInf(transportationInf) as ViewResult;
            var viewName = result.ViewName;

            // Assert
            Assert.AreEqual(viewName, "FormCreateTransportationInf");
        }


        //FormEditTransportationInf-----------------------------------------------------
        [TestMethod]
        public void Test_FormEditTransportationInf_Return_HttpBadRequest_When_ShippingId_Equal_Null()
        {
            //Arrage
            var transportationInf = new TransportationInf();
            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();

            var mockTrans = new Mock<ITransportationInfRepository>();
            mockTrans.Setup(m => m.GetTransportationInfById(null)).Returns(transportationInf);

            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);

            // Act
            var result = controller.FormEditTransportationInf(null) as ActionResult;
            var shippingIdNull = new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // Assert
            Assert.AreEqual(result.ToString(), shippingIdNull.ToString());
        }

        [TestMethod]
        public void Test_FormEditTransportationInf_Return_HttpNotFound_When_TransRepo_Equal_Null()
        {
            //Arrage
            var transportationInf = MockTransportationInf();
            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();

            var mockTrans = new Mock<ITransportationInfRepository>();
            mockTrans.Setup(m => m.GetTransportationInfById(transportationInf.ShippingId)).Returns(transportationInf);

            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);

            // Act
            var result = controller.FormEditTransportationInf(5) as ActionResult;
            var shippingIdNull = new HttpNotFoundResult();

            // Assert
            Assert.AreEqual(result.ToString(), shippingIdNull.ToString());
        }

        [TestMethod]
        public void Test_FormEditTransportationInf_Return_Shipping_And_ViewName_Correct()
        {
            //Arrage
            var transportationInf = MockTransportationInf();
            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();

            var mockTrans = new Mock<ITransportationInfRepository>();
            mockTrans.Setup(m => m.GetTransportationInfById(transportationInf.ShippingId)).Returns(transportationInf);

            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);

            // Act
            var result = controller.FormEditTransportationInf(1) as ViewResult;
            var viewName = result.ViewName;

            // Assert       
            Assert.AreEqual(viewName, "FormEditTransportationInf");
        }

        //EditTransportationInf-----------------------------------------------------------------------------
        [TestMethod]
        public void Test_EditTransportationInf_Return_Shipping_And_View_When_ModelState_Is_Not_Valid()
        {
            //Arrage
            var transportationInf = MockTransportationInf();
            transportationInf.Destination = null;
            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();

            var mockTrans = new Mock<ITransportationInfRepository>();
            mockTrans.Setup(m => m.UpdateTransportationInf(transportationInf)).Returns(true);

            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);
            controller.ModelState.AddModelError("fakeError", "");

            // Act
            var result = controller.EditTransportationInf(transportationInf) as ViewResult;
            var viewName = result.ViewName;

            // Assert
            Assert.AreEqual(viewName, "FormEditTransportationInf");
        }

        [TestMethod]
        public void Test_EditTransportationInf_Return_ListAllTransportationInfsView_When_ModelState_Is__Valid_And_Repo_Equal_True()
        {
            //Arrage
            var transportationInf = MockTransportationInf();
            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();

            var mockTrans = new Mock<ITransportationInfRepository>();
            mockTrans.Setup(m => m.UpdateTransportationInf(transportationInf)).Returns(true);

            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);

            // Act
            var result = controller.EditTransportationInf(transportationInf) as RedirectToRouteResult;
            var redirectViewName = result.RouteValues["Action"];

            // Assert
            Assert.AreEqual(redirectViewName, "ListAllTransportationInfs");
        }

        [TestMethod]
        public void Test_EditTransportationInf_Return_ListAllTransportationInfsView_When_ModelState_Is__Valid_And_Repo_Equal_False()
        {
            //Arrage
            var transportationInf = new TransportationInf();
            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();

            var mockTrans = new Mock<ITransportationInfRepository>();
            mockTrans.Setup(m => m.UpdateTransportationInf(transportationInf)).Returns(false);

            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);

            // Act
            var result = controller.EditTransportationInf(transportationInf) as ViewResult;
            var viewName = result.ViewName;

            // Assert
            Assert.AreEqual(viewName, "FormEditTransportationInf");
        }


        //DeleteTransportationInf--------------------------------------------------
        [TestMethod]
        public void Test_DeleteTransportationInf_Return_HttpBadRequest_When_ShippingId_Equal_Null()
        {
            //Arrage
            var transportationInf = new TransportationInf();
            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();

            var mockTrans = new Mock<ITransportationInfRepository>();
            mockTrans.Setup(m => m.GetTransportationInfById(null)).Returns(transportationInf);

            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);

            // Act
            var result = controller.DeleteTransportationInf(null) as ActionResult;
            var shippingIdNull = new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // Assert
            Assert.AreEqual(result.ToString(), shippingIdNull.ToString());
        }

        [TestMethod]
        public void Test_DeleteTransportationInf_Return_HttpNotFound_When_TransRepo_Equal_Null()
        {
            //Arrage
            var transportationInf = MockTransportationInf();
            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();

            var mockTrans = new Mock<ITransportationInfRepository>();
            mockTrans.Setup(m => m.GetTransportationInfById(transportationInf.ShippingId)).Returns(transportationInf);

            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);

            // Act
            var result = controller.DeleteTransportationInf(5) as ActionResult;
            var shippingIdNull = new HttpNotFoundResult();

            // Assert
            Assert.AreEqual(result.ToString(), shippingIdNull.ToString());
        }

        [TestMethod]
        public void Test_DeleteTransportationInf_Return_Shipping_And_ViewName_Correct()
        {
            //Arrage
            var transportationInf = MockTransportationInf();
            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();

            var mockTrans = new Mock<ITransportationInfRepository>();
            mockTrans.Setup(m => m.GetTransportationInfById(transportationInf.ShippingId)).Returns(transportationInf);

            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);

            // Act
            var result = controller.DeleteTransportationInf(1) as ViewResult;
            var viewName = result.ViewName;

            // Assert       
            Assert.AreEqual(viewName, "DeleteTransportationInf");
        }


        //DeleteTransportationInfConfirmed----------------------------------------------------------------------------
        [TestMethod]
        public void Test_DeleteTransportationInfConfirmed_Return_ListAllTransportationInfsViewName_When_Repo_Reture_True()
        {
            //Arrage
            var transportationInf = MockTransportationInf();
            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();

            var mockTrans = new Mock<ITransportationInfRepository>();
            mockTrans.Setup(m => m.DeleteTransportationInf(transportationInf.ShippingId)).Returns(true);

            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);

            // Act
            var result = controller.DeleteTransportationInfConfirmed(1) as RedirectToRouteResult;
            var viewName = result.RouteValues["Action"];

            // Assert       
            Assert.AreEqual(viewName, "ListAllTransportationInfs");
        }

        [TestMethod]
        public void Test_DeleteTransportationInfConfirmed_Return_ListAllTransportationInfsViewName_When_Repo_Reture_False()
        {
            //Arrage
            var transportationInf = MockTransportationInf();
            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();

            var mockTrans = new Mock<ITransportationInfRepository>();
            mockTrans.Setup(m => m.DeleteTransportationInf(transportationInf.ShippingId)).Returns(false);

            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);

            // Act
            var result = controller.DeleteTransportationInfConfirmed(5) as ViewResult;
            var viewName = result.ViewName;

            // Assert       
            Assert.AreEqual(viewName, "DeleteTransportationInf");
        }



        //FormCreateJobAssignment-----------------------------------------------------------------------------
        [TestMethod]
        public void Test_FormCreateJobAssignment_Return_HttpBadRequest_When_ShippingId_Equal_Null()
        {
            //Arrage
            var transportationInf = new TransportationInf();
            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();

            var mockTrans = new Mock<ITransportationInfRepository>();
            mockTrans.Setup(m => m.GetTransportationInfById(null)).Returns(transportationInf);

            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);

            // Act
            var result = controller.FormCreateJobAssignment(null) as ActionResult;
            var shippingIdNull = new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // Assert
            Assert.AreEqual(result.ToString(), shippingIdNull.ToString());
        }

        [TestMethod]
        public void Test_FormCreateJobAssignment_Return_HttpNotFound_When_TransRepo_Equal_Null()
        {
            //Arrage
            var transportationInf = MockTransportationInf();
            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();

            var mockTrans = new Mock<ITransportationInfRepository>();
            mockTrans.Setup(m => m.GetTransportationInfById(transportationInf.ShippingId)).Returns(transportationInf);

            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);

            // Act
            var result = controller.FormCreateJobAssignment(5) as ActionResult;
            var shippingIdNull = new HttpNotFoundResult();

            // Assert
            Assert.AreEqual(result.ToString(), shippingIdNull.ToString());
        }

        [TestMethod]
        public void Test_FormCreateJobAssignment_Return_Shipping_And_ViewName_Correct()
        {
            //Arrage
            var transportationInf = MockTransportationInf();
            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();

            var mockTrans = new Mock<ITransportationInfRepository>();
            mockTrans.Setup(m => m.GetTransportationInfById(transportationInf.ShippingId)).Returns(transportationInf);

            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);

            // Act
            var result = controller.FormCreateJobAssignment(1) as ViewResult;
            var viewName = result.ViewName;
            var model = result.Model as FormJobAssignmentViewModel;

            // Assert       
            Assert.AreEqual(viewName, "FormCreateJobAssignment");
            Assert.IsNotNull(model);
        }



        //CreateJobAssignment----------------------------------------------------------------------------------------------
        [TestMethod]
        public void Test_CreateJobAssignment_Return_JobAssignmentViewModel_And_FormCreateJobAssignmentView_When_ModelState_Is_Not_Valid()
        {
            //Arrage
            var transportationInf = MockViewModelData();
            transportationInf.JobAssignment.DestinationJob = null;
            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();
            mockJob.Setup(m => m.AddJobAssignment(transportationInf)).Returns(true);

            var mockTrans = new Mock<ITransportationInfRepository>();
            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);
            controller.ModelState.AddModelError("fakeError", "");

            // Act
            var result = controller.CreateJobAssignment(transportationInf) as ViewResult;
            var viewName = result.ViewName;

            // Assert
            Assert.AreEqual(viewName, "FormCreateJobAssignment");
        }

        [TestMethod]
        public void Test_CreateJobAssignment_Return_ListAllTransportationInfsView_When_ModelState_Is__Valid_And_Repo_Equal_True()
        {
            //Arrage
            var transportationInf = MockViewModelData();
            transportationInf.JobAssignment.DestinationJob = null;
            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();
            mockJob.Setup(m => m.AddJobAssignment(transportationInf)).Returns(true);

            var mockTrans = new Mock<ITransportationInfRepository>();
            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);

            // Act
            var result = controller.CreateJobAssignment(transportationInf) as RedirectToRouteResult;
            var redirectViewName = result.RouteValues["Action"];

            // Assert
            Assert.AreEqual(redirectViewName, "ListAllTransportationInfs");
        }

        [TestMethod]
        public void Test_CreateJobAssignment_Return_ListAllTransportationInfsView_When_ModelState_Is__Valid_And_Repo_Equal_False()
        {
            //Arrage
            var transportationInf = MockViewModelData();
            transportationInf.JobAssignment.DestinationJob = null;
            var mockTruckDriver = new Mock<ITruckDriverRepository>();
            var mockJob = new Mock<IJobAssignmentRepository>();
            mockJob.Setup(m => m.AddJobAssignment(transportationInf)).Returns(false);

            var mockTrans = new Mock<ITransportationInfRepository>();

            var controller = new TransportationInfController(mockTrans.Object, mockJob.Object, mockTruckDriver.Object);

            // Act
            var result = controller.CreateJobAssignment(transportationInf) as ViewResult;
            var viewName = result.ViewName;

            // Assert
            Assert.AreEqual(viewName, "FormCreateJobAssignment");
        }


        public IEnumerable<TransportationInf> MockListTransportationInfs()
        {
            var listTransInf = new TransportationInf[]
            {
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
                    ReceiveDateTime = new DateTime(2017, 10, 05, 4, 40, 29),
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
                    ReceiveDateTime = new DateTime(2017, 10, 05, 4, 40, 29),
                    ShippingDocImage = null,
                    ShippingNote = "accident"
                }

            };

            return listTransInf;
        }

        public TransportationInf MockTransportationInf()
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
