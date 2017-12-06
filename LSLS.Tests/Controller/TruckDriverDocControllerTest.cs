using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using LSLS.Controllers;
using LSLS.Models;
using LSLS.Properties;
using LSLS.Repository.Abstract;
using LSLS.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LSLS.Tests.Controller
{
    [TestClass]
    public class TruckDriverDocControllerTest
    {

        //ListAllTruckDriverDoc---------------------------------------------------------------------
        [TestMethod]
        public void Test_ListAllTruckDriverDoc_Return_DataList_()
        {
            //Arrage
            var listTruckDriverDoc = MockListTruckDriverDocuments();
            var truckDriverDocRepository = new Mock<ITruckDriverDocRepository>();
            truckDriverDocRepository.Setup(t => t.GetAllListTruckDriverDocuments())
                .Returns(listTruckDriverDoc.AsQueryable());

            var controller = new TruckDriverDocController(truckDriverDocRepository.Object);

            //Act
            var result = controller.ListAllTruckDriverDoc();
            var viewName = result.ViewName;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(viewName,"ListAllTruckDriverDoc");
        }


        //ListFilesByTruckDriverDocId---------------------------------------------------------------------
        [TestMethod]
        public void Test_ListFilesByTruckDriverDocId_Return_ListFilesTruckDriverDocView()
        {
            //Arrage
            var listFileDocuments = MockListFileDetailOfTruckDriverDocuments();
            var truckDriverDocRepository = new Mock<ITruckDriverDocRepository>();
            truckDriverDocRepository.Setup(t => t.ListFilesByTruckDriverDocId(1))
                .Returns(listFileDocuments.AsQueryable);

            var controller = new TruckDriverDocController(truckDriverDocRepository.Object);

            //Act
            var result = controller.ListFilesByTruckDriverDocId(1) as ViewResult;
            var viewName = result.ViewName;
            var model = result.Model as ListFileTruckDriverDocViewModel;

            //Assert
            Assert.AreEqual(viewName, "ListFilesTruckDriverDoc");
            Assert.IsNotNull(result);
            Assert.IsNotNull(model);
        }


        //UploadFileTruckDriverDocView-------------------------------------------------------------------------
        [TestMethod]
        public void Test_UploadFileTruckDriverDocView_Return_UploadFileTruckDriverDocView_When_TruckDriverDocId_Exist()
        {
            //Arrage
            var truckDriverDoc = MockTruckDriverDoc();
            var truckDriverDocRepository = new Mock<ITruckDriverDocRepository>();
            truckDriverDocRepository.Setup(t => t.GetTruckDriverDocumentById(truckDriverDoc.TruckDriverDocId))
                .Returns(truckDriverDoc);

            var controller = new TruckDriverDocController(truckDriverDocRepository.Object);

            //Act
            var result = controller.UploadFileTruckDriverDocView(truckDriverDoc.TruckDriverDocId) as ViewResult;
            var viewName = result.ViewName;
            var model = result.Model as FileDetail;
            //Assert
            Assert.AreEqual(viewName, "UploadFileTruckDriverDocView");
            Assert.IsNotNull(result);
            Assert.IsNotNull(model);
        }

        //UploadFileTruckDriverDoc------------------------------------------------------------------------------
        [TestMethod]
        public void Test_UploadFileTruckDriverDoc_Return_UploadFileTruckDriverDocView_When_Model_State_Is_Not_Valid()
        {
            //Arrage
            var file = MockFileDetail();
            var truckDriverDocRepository = new Mock<ITruckDriverDocRepository>();

            var controller = new TruckDriverDocController(truckDriverDocRepository.Object);
            controller.ModelState.AddModelError("Fake Error", "");

            //Act
            var result = controller.UploadFileTruckDriverDoc(file) as ViewResult;
            var viewName = result.ViewName;
            //Assert
            Assert.AreEqual(viewName, "UploadFileTruckDriverDocView");
            Assert.IsNotNull(result);
        }

        /*
        [TestMethod]
        public void Test_UploadFileTruckDriverDoc_Return_UploadFileTruckDriverDocView_When_File_Null()
        {
            //Arrage
            var fileToUpload = new Mock<HttpRequestBase>();

            var truckDriverDocRepository = new Mock<ITruckDriverDocRepository>();

            var controller = new TruckDriverDocController(truckDriverDocRepository.Object);
            

            //Act
            var result = controller.UploadFileTruckDriverDoc(null) as ViewResult;
            var viewName = result.ViewName;
            //Assert
            Assert.AreEqual(viewName, "UploadFileTruckDriverDocView");
        }
        */


        //DeleteFileFromTruckDriverDocView-----------------------------------------------------
        [TestMethod]
        public void Test_DeleteFileFromTruckDriverDocView_Return_View_Correct()
        {
            //Arrage
            var file = MockFileDetail();
            var truckDriverDocRepository = new Mock<ITruckDriverDocRepository>();
            truckDriverDocRepository.Setup(t => t.GetFileDetailById(file.FileId)).Returns(file);

            var controller = new TruckDriverDocController(truckDriverDocRepository.Object);

            //Act
            var result = controller.DeleteFileFromTruckDriverDocView(file.FileId);
            var viewName = result.ViewName;
            var model = result.Model;

            //Assert
            Assert.AreEqual(viewName, "DeleteFileFromTruckDriverDocView");
            Assert.IsNotNull(model);
        }


        //DeleteFileFromTruckDriverDoc-------------------------------------------------------------------






        //PrintFileFromTruckDriverView-------------------------------------------------------------------
        public void Test_PrintFileFromTruckDriverView_Should_Return_FileView()
        {
            //Arrage
            var file = MockFileDetail();
            var truckDriverDocRepository = new Mock<ITruckDriverDocRepository>();
            truckDriverDocRepository.Setup(t => t.GetFileDetailById(file.FileId)).Returns(file);

            var controller = new TruckDriverDocController(truckDriverDocRepository.Object);

            //Act

            //var serverStub = new StubHttpServerUtilityBase();
            //serverStub.MapPathString = (path) => path.Replace("~", string.Empty).Replace("/", @"\");

            //var contextStub = new StubHttpContextBase();
            //contextStub.ServerGet = () => serverStub;

            //controller.ControllerContext = new ControllerContext();
            //controller.ControllerContext.HttpContext = contextStub;

            var result = (FilePathResult)controller.PrintFileFromTruckDriverView(file.FileId);

            //Assert
            Assert.AreEqual(@"\Content\Test.txt", result.FileName);
        }


        //ShareFileTruckDriverView------------------------------------------------------------------
        [TestMethod]
        public void Test_ShareFileTruckDriverView_Should_Return_ViewName()
        {
            //Arrage
            var fileDetail = MockFileDetail();
            var mockRepo = new Mock<ITruckDriverDocRepository>();
            mockRepo.Setup(t => t.GetFileDetailById(fileDetail.FileId)).Returns(fileDetail);

            var controller = new TruckDriverDocController(mockRepo.Object);

            //Act
            var result = controller.ShareFileTruckDriverView(fileDetail.FileId);
            var viewName = result.ViewName;
            var model = result.Model as SentToMailViewModel;

            //Assert
            Assert.AreEqual(viewName, "ShareFileTruckDriverView");
            Assert.IsNotNull(model);
        }


        //ShareFileTruckDriver-------------------------------------------------------------------











        public IEnumerable<TruckDriverDocument> MockListTruckDriverDocuments()
        {
            var listTruckDriverDoc = new TruckDriverDocument[]
            {
                new TruckDriverDocument()
                {
                    TruckDriverDocId = 1,
                    TruckDriverId = 1
                },
                new TruckDriverDocument()
                {
                    TruckDriverDocId = 2,
                    TruckDriverId = 2
                }
            };

            return listTruckDriverDoc;
        }

        public IEnumerable<FileDetail> MockListFileDetailOfTruckDriverDocuments()
        {
            var listFileDetail = new FileDetail[]
            {
                new FileDetail()
                {
                    TruckDriverDocId = 1,
                    FileId = Guid.Parse("1A3B944E-3632-467B-A53A-206305310BAE"),
                    FileName = "DriverA_DriverLicence",
                    Extension = ".pdf",
                    FileCategory = "Driver Licence",
                    LastModified = new DateTime(2017,8,12,5,50,30)                    
                },
                new FileDetail()
                {
                    TruckDriverDocId = 1,
                    FileId = Guid.Parse("a2c11e77-06ff-48da-9514-8bc2b3bfdb27"),
                    FileName = "DriverA_CitizenId",
                    Extension = ".pdf",
                    FileCategory = "CitizenId",
                    LastModified = new DateTime(2017,12,4,3,24,30)
                }
            };

            return listFileDetail;
        }

        private TruckDriverDocument MockTruckDriverDoc()
        {
            var truckDriverDoc = new TruckDriverDocument()
            {
                TruckDriverDocId = 1,
                TruckDriverId = 1
            };

            return truckDriverDoc;
        }

        private FileDetail MockFileDetail()
        {
            var file = new FileDetail()
            {
                TruckDriverDocId = 1,
                FileId = Guid.Parse("1A3B944E-3632-467B-A53A-206305310BAE"),
                FileName = "DriverA_DriverLicence",
                Extension = ".pdf",
                FileCategory = "Driver Licence",
                LastModified = new DateTime(2017, 8, 12, 5, 50, 30)
            };

            return file;
        }
    }
}
