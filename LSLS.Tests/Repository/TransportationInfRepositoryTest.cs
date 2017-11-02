using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LSLS.Models;
using LSLS.Repository.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace LSLS.Tests.Repository
{

    public class TransportationInfRepositoryTest
    {
        //UTC-56
        [Test]
        public void GetAllTransportationInfsTest()
        {
            //Arrange
            var mock = new Mock<ITransportationInfRepository>();
            mock.Setup(j => j.GetAllTransportationInfs()).Returns(ListTransportationInfData);
            var repo = mock.Object;

            // Action             
            var result = repo.GetAllTransportationInfs();
            var listTransportation = ListTransportationInfData();

            // Assert             
            Assert.AreEqual(result.ToString(), listTransportation.ToString());

            //Arrange
            var mockNull = new Mock<ITransportationInfRepository>();
            if (mockNull.Object == null)
            {
                var repoNull = mockNull.Object;

                // Action             
                var resultNull = repoNull.GetAllTransportationInfs();

                // Assert             
                Assert.IsNull(resultNull);
            }

        }

        //UTC-57
        [Test]
        public void GetJobAssignmentByIdTest()
        {
            //Arrange
            var tranA = TransportationInfData();

            var mock = new Mock<ITransportationInfRepository>();
            mock.Setup(j => j.GetTransportationInfById(1)).Returns(tranA);
            var repo = mock.Object;

            // Action             
            var result = repo.GetTransportationInfById(1);

            // Assert             
            Assert.AreEqual(result.ToString(), tranA.ToString());

            //Arrange
            var mockNull = new Mock<ITransportationInfRepository>();
            if (mockNull.Object == null)
            {
                var repoNull = mockNull.Object;

                // Action             
                var resultNull = repoNull.GetTransportationInfById(null);

                // Assert             
                Assert.IsNull(resultNull);
            }
        }

        //UTC-58
        [Test]
        public void AddTransportationInfTest()
        {
            //Arrange
            var tranA = TransportationInfData();

            var mock = new Mock<ITransportationInfRepository>();
            mock.Setup(j => j.AddTransportationInf(tranA)).Returns(true);
            var repo = mock.Object;

            // Action             
            var result = repo.AddTransportationInf(tranA);

            // Assert             
            Assert.IsTrue(result);


            //Arrange
            var tranB = new TransportationInf();

            var mockNull = new Mock<ITransportationInfRepository>();
            mockNull.Setup(j => j.AddTransportationInf(tranB)).Returns(false);
            var repoNull = mockNull.Object;

            // Action             
            var resultNull = repoNull.AddTransportationInf(tranB);

            // Assert             
            Assert.IsFalse(resultNull);
        }

        //UTC-59
        [Test]
        public void UpdateTransportationInfTest()
        {
            //Arrange
            var tranA = TransportationInfData();

            var mock = new Mock<ITransportationInfRepository>();
            mock.Setup(j => j.UpdateTransportationInf(tranA)).Returns(true);
            var repo = mock.Object;

            // Action             
            var result = repo.UpdateTransportationInf(tranA);

            // Assert             
            Assert.IsTrue(result);


            //Arrange
            var tranB = new TransportationInf();

            var mockNull = new Mock<ITransportationInfRepository>();
            mockNull.Setup(j => j.UpdateTransportationInf(tranB)).Returns(false);
            var repoNull = mockNull.Object;

            // Action             
            var resultNull = repoNull.UpdateTransportationInf(tranB);

            // Assert             
            Assert.IsFalse(resultNull);
        }

        //UTC-60
        [Test]
        public void DeleteJobAssignmentTest()
        {
            //Arrange
            var mock = new Mock<ITransportationInfRepository>();
            mock.Setup(j => j.DeleteTransportationInf(1)).Returns(true);
            var repo = mock.Object;

            // Action             
            var result = repo.DeleteTransportationInf(1);

            // Assert             
            Assert.IsTrue(result);


            //Arrange
            var mockNull = new Mock<ITransportationInfRepository>();
            mockNull.Setup(j => j.DeleteTransportationInf(null)).Returns(false);
            var repoNull = mockNull.Object;

            // Action             
            var resultNull = repoNull.DeleteTransportationInf(null);

            // Assert             
            Assert.IsFalse(resultNull);
        }


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
        public IEnumerable<TransportationInf> ListTransportationInfData()
        {
            List<TransportationInf> listTransportationInf = new List<TransportationInf>();

            var tranA = new TransportationInf()
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
            var tranB = new TransportationInf()
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
            };

            listTransportationInf.Add(tranA);
            listTransportationInf.Add(tranB);

            return listTransportationInf;
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
