using System;
using System.Collections.Generic;
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
    public class PaymentDocControllerTest
    {
        //ListAllPaymentDocs--------------------------------------------
        [TestMethod]
        public void Test_ListAllPaymentDocs_Should_Return_ListAllPaymentDocsView_And_ListData()
        {
            //Arrage
            var listPaymentDoc = MockListPaymentDocs();
            var mockRepo = new Mock<IPaymentDocRepository>();
            mockRepo.Setup(p => p.GetAllListPaymentDocuments()).Returns(listPaymentDoc);

            var controller = new PaymentDocController(mockRepo.Object);

            //Act
            var result = controller.ListAllPaymentDocs();
            var viewName = result.ViewName;
            var model = result.Model as IEnumerable<PaymentDocument>;

            //Assert
            Assert.AreEqual(viewName, "ListAllPaymentDocs");
            Assert.IsNotNull(model);

        }

        //UploadFilePaymentDocView---------------------------------------------------------------
        [TestMethod]
        public void Test_UploadFilePaymentDocView_Should_Return_ViewName_Correct()
        {
            //Arrage
            var mockRepo = new Mock<IPaymentDocRepository>();
            var controller = new PaymentDocController(mockRepo.Object);

            //Act
            var target = controller.UploadFilePaymentDocView();
            var viewName = target.ViewName;

            //Assert
            Assert.AreEqual(viewName, "UploadFilePaymentDocView");
        }

        //UploadPaymentDoc--------------------------------------------------------------
        [TestMethod]
        public void Test_UploadPaymentDoc_Should_Return_UploadFilePaymentDocView()
        {
            //Arrage
            var paymentFile = MockPaymentDoc();
            var mockRepo = new Mock<IPaymentDocRepository>();
            
            var controller = new PaymentDocController(mockRepo.Object);
            controller.ModelState.AddModelError("Error","");

            //Act
            var target = controller.UploadFilePaymentDoc(null) as ViewResult;
            var viewName = target.ViewName;

            //Assert
            Assert.AreEqual(viewName, "UploadFilePaymentDocView");
        }


        //PrintPaymentDocView--------------------------------------------------------------------------
        [TestMethod]
        public void Test_PrintPaymentDocView_Should_Return_PaymentFile()
        {
            
        }


        //ShareFilePaymentDocView---------------------------------------------------------------
        [TestMethod]
        public void Test_ShareFilePaymentDocView_Should_Retern_ShareFilePaymentDocView_And_ViewModel()
        {
            //Arrage
            var paymentPayment = MockPaymentDoc();
            var mockRepo = new Mock<IPaymentDocRepository>();
            mockRepo.Setup(p => p.GetPaymentDocumentById(paymentPayment.PaymentDocId)).Returns(paymentPayment);

            var controller = new PaymentDocController(mockRepo.Object);

            //Act
            var targer = controller.ShareFilePaymentDocView(paymentPayment.PaymentDocId);
            var viewName = targer.ViewName;
            var model = targer.Model as SharePaymentViewModel;

            //Assert
            Assert.AreEqual(viewName, "ShareFilePaymentDocView");
            Assert.IsNotNull(model);
        }

        //ShareFilePaymentDoc--------------------------------------------------------------
        [TestMethod]
        public void Test_ShareFilePaymentDoc_Should_Return_ShareFilePaymentDocView_When_ModelState_Is_Not_Valid()
        {
            /*
            //Arrage
            var sentViewModel = new SharePaymentViewModel();
            var paymentFile = MockPaymentDoc();
            var mockRepo = new Mock<IPaymentDocRepository>();
            mockRepo.Setup(p => p.GetPaymentDocumentById(sentViewModel.PaymentDocument.PaymentDocId)).Returns(paymentFile);

            var controller = new PaymentDocController(mockRepo.Object);
            //controller.ModelState.AddModelError("Error","");
            

            //Act
            var target = controller.ShareFilePaymentDoc(sentViewModel) as ViewResult;
            var viewName = target.ViewName;

            //Assert
            Assert.AreEqual(viewName, "ShareFilePaymentDocView");

    */
        }








        private IEnumerable<PaymentDocument> MockListPaymentDocs()
        {
            var listPaymentDoc = new PaymentDocument[]
            {
                new PaymentDocument
                {
                    PaymentDocId = Guid.Parse("21fbb2ce-75fd-42b9-9ec4-3b28b85e84c3"),
                    PaymentFileName = "Food Company",
                    PaymentLastModified = new DateTime(2017,12,4,3,06,30),
                    Extension = ".pdf",
                },
                new PaymentDocument
                {
                    PaymentDocId = Guid.Parse("1e74ebcc-5084-4054-bd70-ae1beb1dcbb3"),
                    PaymentFileName = "A Company",
                    PaymentLastModified = new DateTime(2017,12,5,14,06,30),
                    Extension = ".pdf",
                }
            };

            return listPaymentDoc;
        }


        private PaymentDocument MockPaymentDoc()
        {
            var paymentDoc = new PaymentDocument
            {
                PaymentDocId = Guid.Parse("21fbb2ce-75fd-42b9-9ec4-3b28b85e84c3"),
                PaymentFileName = "Food Company",
                PaymentLastModified = new DateTime(2017, 12, 4, 3, 06, 30),
                Extension = ".pdf",
            };

            return paymentDoc;
        }
    }
}
