using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using LSLS.Controllers.Api;
using LSLS.Models;
using LSLS.Repository.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace LSLS.Tests.Controller.Api
{
    public class TransportationInfControllerApiTest
    {
        //UTC-62
        [Test]
        public void GetTransportationInfByShippingIdTest()
        {
            var transportationInf = TransportationInfData();

            var mockRepository = new Mock<ITransportationInfRepository>();
            mockRepository.Setup(x => x.GetTransportationInfById(1)).Returns(transportationInf);

            // Act
            var controller = new TransportationInfController(mockRepository.Object);

            // Assert
            var result = controller.GetTransportationInfByShippingId(1) as TransportationInf;
            if (result != null)
                Assert.AreEqual(result, transportationInf);

            // Arrange
            var transportationInfNull = new TransportationInf();

            var mockRepositoryNull = new Mock<ITransportationInfRepository>();
            mockRepositoryNull.Setup(x => x.GetTransportationInfById(50)).Returns(transportationInfNull);

            // Act
            var controllerNull = new TransportationInfController(mockRepositoryNull.Object);

            // Assert
            var resultNull = controllerNull.GetTransportationInfByShippingId(50) as TransportationInf;
            Assert.IsNull(resultNull);

        }

        //UTC-63
        [Test]
        public void UpdateTransportationInfReturn_True_Test()
        {
            var transportationInf = TransportationInfData();

            var mockRepository = new Mock<ITransportationInfRepository>();          
            mockRepository.Setup(x => x.GetTransportationInfById(1)).Returns(transportationInf);
            mockRepository.Setup(x => x.UpdateTransportationInf(transportationInf)).Returns(true);

            // Act
            var controller = new TransportationInfController(mockRepository.Object);

            // Assert
            var result = controller.UpdateTransportationInf(1,transportationInf) as OkNegotiatedContentResult<Boolean>; ;
            Assert.IsTrue(result.Content);
            

        }

        //UTC-63
        [Test]
        public void UpdateTransportationInfReturn_False_Test()
        {
            var transportationInf = TransportationInfData();
            transportationInf.ShippingId = 50;

            var mockRepository = new Mock<ITransportationInfRepository>();
            mockRepository.Setup(x => x.GetTransportationInfById(50)).Returns(transportationInf);
            mockRepository.Setup(x => x.UpdateTransportationInf(transportationInf)).Returns(false);

            // Act
            var controller = new TransportationInfController(mockRepository.Object);

            // Assert
            var result = controller.UpdateTransportationInf(50, transportationInf) as OkNegotiatedContentResult<Boolean>; ;
            Assert.IsFalse(result.Content);


        }

        //UTC-63
        [Test]
        public void UpdateTransportationInfReturn_Http_Not_Found_Test()
        {
            var mockRepository = new Mock<ITransportationInfRepository>();
            // Act
            var controller = new TransportationInfController(mockRepository.Object);

            int? num = null;
            // Assert
            var result = controller.UpdateTransportationInf(num, new TransportationInf());
            Assert.IsInstanceOf<NotFoundResult>(result);


        }




        [TestInitialize]
        public TransportationInf TransportationInfData()
        {
            var transportationInfA = new TransportationInf()
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

            return transportationInfA;
        }
    }
}
