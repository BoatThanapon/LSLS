using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LSLS.Models;
using LSLS.Repository;
using LSLS.Repository.Abstract;
using LSLS.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace LSLS.Tests.Repository
{

    public class JobAssignmentRepositoryTest
    {
        //UTC-50
        [Test]
        public void GetAllJobAssignmentsTest()
        {
            //Arrange
            var mock = new Mock<IJobAssignmentRepository>();
            mock.Setup(j => j.GetAllJobAssignments()).Returns(ListJobAssignmentData);
            var repo = mock.Object;

            // Action             
            var result = repo.GetAllJobAssignments();
            var listJob = ListJobAssignmentData();

            // Assert             
            Assert.AreEqual(result.ToString(), listJob.ToString());

            //Arrange
            var mockNull = new Mock<IJobAssignmentRepository>();
            if (mockNull.Object == null)
            {
                var repoNull = mockNull.Object;

                // Action             
                var resultNull = repoNull.GetAllJobAssignments();

                // Assert             
                Assert.IsNull(resultNull);
            }

        }

        //UTC-51
        [Test]
        public void GetJobAssignmentByIdTest()
        {
            //Arrange
            var jobA = new JobAssignment()
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

            var mock = new Mock<IJobAssignmentRepository>();
            mock.Setup(j => j.GetJobAssignmentById(1)).Returns(jobA);
            var repo = mock.Object;

            // Action             
            var result = repo.GetJobAssignmentById(1);

            // Assert             
            Assert.AreEqual(result.ToString(), jobA.ToString());

            //Arrange
            var mockNull = new Mock<IJobAssignmentRepository>();
            if (mockNull.Object == null)
            {
                var repoNull = mockNull.Object;

                // Action             
                var resultNull = repoNull.GetJobAssignmentById(null);

                // Assert             
                Assert.IsNull(resultNull);
            }
        }

        //UTC-52
        [Test]
        public void AddJobAssignmentTest()
        {
            //Arrange
            var jobViewModel = MockViewModelData();

            var mock = new Mock<IJobAssignmentRepository>();
            mock.Setup(j => j.AddJobAssignment(jobViewModel)).Returns(true);
            var repo = mock.Object;

            // Action             
            var result = repo.AddJobAssignment(jobViewModel);

            // Assert             
            Assert.IsTrue(result);


            //Arrange
            var jobViewModelNull = new FormJobAssignmentViewModel
            {
                JobAssignment = null,
                TruckDrivers = null
            };

            var mockNull = new Mock<IJobAssignmentRepository>();
            mockNull.Setup(j => j.AddJobAssignment(jobViewModelNull)).Returns(false);
            var repoNull = mockNull.Object;

            // Action             
            var resultNull = repoNull.AddJobAssignment(jobViewModelNull);

            // Assert             
            Assert.IsFalse(resultNull);
        }

        //UTC-53
        [Test]
        public void UpdateJobAssignmentTest()
        {
            //Arrange
            var jobViewModel = MockViewModelData();

            var mock = new Mock<IJobAssignmentRepository>();
            mock.Setup(j => j.UpdateJobAssignment(jobViewModel)).Returns(true);
            var repo = mock.Object;

            // Action             
            var result = repo.UpdateJobAssignment(jobViewModel);

            // Assert             
            Assert.IsTrue(result);


            //Arrange
            var jobViewModelNull = new FormJobAssignmentViewModel
            {
                JobAssignment = null,
                TruckDrivers = null
            };

            var mockNull = new Mock<IJobAssignmentRepository>();
            mockNull.Setup(j => j.UpdateJobAssignment(jobViewModelNull)).Returns(false);
            var repoNull = mockNull.Object;

            // Action             
            var resultNull = repoNull.UpdateJobAssignment(jobViewModelNull);

            // Assert             
            Assert.IsFalse(resultNull);
        }

        //UTC-54
        [Test]
        public void DeleteJobAssignmentTest()
        {
            //Arrange
            var mock = new Mock<IJobAssignmentRepository>();
            mock.Setup(j => j.DeleteJobAssignment(1)).Returns(true);
            var repo = mock.Object;

            // Action             
            var result = repo.DeleteJobAssignment(1);

            // Assert             
            Assert.IsTrue(result);


            //Arrange
            var mockNull = new Mock<IJobAssignmentRepository>();
            mockNull.Setup(j => j.DeleteJobAssignment(null)).Returns(false);
            var repoNull = mockNull.Object;

            // Action             
            var resultNull = repoNull.DeleteJobAssignment(null);

            // Assert             
            Assert.IsFalse(resultNull);
        }

        //UTC-55
        [Test]
        public void GetListJobByTruckDriverIdTest()
        {
            //Arrange
            var job = ListJobAssignment();
            var mock = new Mock<IJobAssignmentRepository>();
            mock.Setup(j => j.GetListJobByTruckDriverId(1)).Returns(job);
            var repo = mock.Object;

            // Action             
            var result = repo.GetListJobByTruckDriverId(1);
            var listJob = ListJobAssignmentData();


            // Assert             
            Assert.AreEqual(result.ToString(), listJob.ToString());

            //Arrange
            int? number = null;

            var jobListNull = new List<JobAssignment>();
            var mockNull = new Mock<IJobAssignmentRepository>();
            mockNull.Setup(j => j.GetListJobByTruckDriverId(number)).Returns(jobListNull);

            if (mockNull.Object == null)
            {
                var repoNull = mockNull.Object;

                // Action             
                var resultNull = repoNull.GetListJobByTruckDriverId(number);

                // Assert             
                Assert.IsNull(resultNull);
            }

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
        public IEnumerable<JobAssignment> ListJobAssignmentData()
        {
            List<JobAssignment> listJobAssignments = new List<JobAssignment>();

            var jobA = new JobAssignment()
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
            var jobB = new JobAssignment()
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
            };

            listJobAssignments.Add(jobA);
            listJobAssignments.Add(jobB);

            return listJobAssignments;
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

        [TestInitialize]
        public List<JobAssignment> ListJobAssignment()
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

            listJobAssignments.Add(jobA);
            return listJobAssignments;
        }

    }
}
