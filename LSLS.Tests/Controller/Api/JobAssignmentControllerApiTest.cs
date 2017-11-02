using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http.Results;
using LSLS.Controllers.Api;
using LSLS.Models;
using LSLS.Repository.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace LSLS.Tests.Controller.Api
{
    public class JobAssignmentControllerApiTest
    {
        [Test]
        public void ListJobAssignmentByTruckDriverIdTest()
        {
            // Arrange
            var job = ListJobAssignmentData();

            var mockRepository = new Mock<IJobAssignmentRepository>();
            mockRepository.Setup(x => x.GetListJobByTruckDriverId(1)).Returns(job);

            // Act
            var controller = new JobAssignmentController(mockRepository.Object);

            // Assert
            var result = controller.ListJobAssignmentByTruckDriverId(1) as List<JobAssignment>;
            if (result != null)
                Assert.AreEqual(result, job);

            // Arrange
            var jobNull = new List<JobAssignment>();

            var mockRepositoryNull = new Mock<IJobAssignmentRepository>();
            mockRepositoryNull.Setup(x => x.GetListJobByTruckDriverId(30)).Returns(jobNull);

            // Act
            var controllerNull = new JobAssignmentController(mockRepositoryNull.Object);

            // Assert
            var resultNull = controllerNull.ListJobAssignmentByTruckDriverId(30) as List<JobAssignment>;
            Assert.IsNull(resultNull);
        }


        [TestInitialize]
        public List<JobAssignment> ListJobAssignmentData()
        {
            List<JobAssignment> listJobAssignments = new List<JobAssignment>();

            var jobA = new JobAssignment()
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
            var jobB = new JobAssignment()
            {
                JobAssignmentId = 2,
                ShippingId = 2,
                TruckDriverId = 2,
                JobAssignmentDate = new DateTime(2017, 10, 01).ToString(CultureInfo.CurrentCulture),
                StartingPointJob = "Chiang Rai",
                LatitudeStartJob = (float)15.2154,
                LongitudeStartJob = (float)97.148,
                DestinationJob = "Chonburi",
                LatitudeDesJob = (float)13.1245,
                LongitudeDesJob = (float)92.1454,
                JobAssignmentStatus = false
            };

            listJobAssignments.Add(jobA);
            listJobAssignments.Add(jobB);

            return listJobAssignments;
        }
    }
}
