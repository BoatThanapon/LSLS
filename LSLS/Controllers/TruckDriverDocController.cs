using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using LSLS.Models;
using LSLS.Repository.Abstract;
using LSLS.ViewModels;

namespace LSLS.Controllers
{
    [Authorize]
    public class TruckDriverDocController : Controller
    {

        private readonly ITruckDriverDocRepository _truckDriverDocRepository;

        public TruckDriverDocController(ITruckDriverDocRepository truckDriverDocRepository)
        {
            _truckDriverDocRepository = truckDriverDocRepository;
        }

        // GET: TruckDriverDoc
        [HttpGet]
        public ViewResult ListAllTruckDriverDoc()
        {
            var listAllTruckDriverDoc = _truckDriverDocRepository.GetAllListTruckDriverDocuments();

            return View("ListAllTruckDriverDoc", listAllTruckDriverDoc);
        }

        //GET: List File
        [HttpGet]
        public ViewResult ListFilesByTruckDriverDocId(int truckDriverDocId)
        {
            var listFilesByTruckDriverDocId = _truckDriverDocRepository.ListFilesByTruckDriverDocId(truckDriverDocId);

            if (listFilesByTruckDriverDocId == null)
            {
                return null;
            }

            var findTruckDriverDocAndTruckDriverById =
                _truckDriverDocRepository.GetTruckDriverDocAndTruckDriverById(truckDriverDocId);

            ListFileTruckDriverDocViewModel listFileTruckDriverDocViewModel = new ListFileTruckDriverDocViewModel
            {
                TruckDriverDocument = findTruckDriverDocAndTruckDriverById,
                FileDetails = listFilesByTruckDriverDocId
            };

            return View("ListFilesTruckDriverDoc", listFileTruckDriverDocViewModel);
        }

        //GET: Upload File
        [HttpGet]
        public ActionResult UploadFileTruckDriverDocView(int truckDriverDocId)
        {
            var findTruckDriverDoc = _truckDriverDocRepository.GetTruckDriverDocumentById(truckDriverDocId);
            if (findTruckDriverDoc == null)
            {
                return HttpNotFound();
            }

            FileDetail fileDetail = new FileDetail()
            {
                TruckDriverDocId = findTruckDriverDoc.TruckDriverDocId
            };

            return View("UploadFileTruckDriverDocView", fileDetail);
        }


        //POST: Upload File
        [HttpPost]
        [ActionName("UploadFileTruckDriverDocView")]
        [ValidateAntiForgeryToken]
        public ActionResult UploadFileTruckDriverDoc(FileDetail fileUpload)
        {
            DateTime dateTime = DateTime.UtcNow;
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var convertedTime = TimeZoneInfo.ConvertTime(dateTime, timeZoneInfo);

            if (ModelState.IsValid)
            {
                List<FileDetail> fileDetails = new List<FileDetail>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        FileDetail fileDetail = new FileDetail()
                        {
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            FileId = Guid.NewGuid(),
                            FileCategory = fileUpload.FileCategory,
                            LastModified = convertedTime

                        };

                        if (fileDetail.Extension == ".pdf")
                        {
                            fileDetails.Add(fileDetail);

                            var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), fileDetail.FileId + fileDetail.Extension);
                            file.SaveAs(path);

                            var findTruckDriverDocAndTruckDriverById =
                                _truckDriverDocRepository.GetTruckDriverDocAndTruckDriverById(fileUpload.TruckDriverDocId);

                            if (findTruckDriverDocAndTruckDriverById != null)
                            {
                                findTruckDriverDocAndTruckDriverById.FileDetails = fileDetails;
                                bool addFileDetail = _truckDriverDocRepository.AddFileDetails(fileDetail);
                                if (addFileDetail.Equals(true))
                                {
                                    _truckDriverDocRepository.SaveChanges();

                                    var listFilesByTruckDriverDocId =
                                        _truckDriverDocRepository.ListFilesByTruckDriverDocId(fileUpload.TruckDriverDocId);

                                    ListFileTruckDriverDocViewModel listFileTruckDriverDoc = new ListFileTruckDriverDocViewModel
                                    {
                                        TruckDriverDocument = findTruckDriverDocAndTruckDriverById,
                                        FileDetails = listFilesByTruckDriverDocId
                                    };

                                    return View("ListFilesTruckDriverDoc", listFileTruckDriverDoc);

                                }

                            }
                        }

                        ViewBag.Message = "Please, upload PDF File Only";
                        return View(fileUpload);
                    }
                }
                return View(fileUpload);
            }
            return View(fileUpload);
        }


        //GET DeleteFileView
        [HttpGet]
        public ViewResult DeleteFileFromTruckDriverDocView(Guid fileId)
        {
            var fileDetail = _truckDriverDocRepository.GetFileDetailById(fileId);
            
            return View("DeleteFileFromTruckDriverDocView", fileDetail);
        }

        // POST: DeleteFileFromTruckDriver
        [HttpPost]
        [ActionName("DeleteFileFromTruckDriverDocView")]
        public ActionResult DeleteFileFromTruckDriverDoc(Guid fileId)
        {
            var fileDetail = _truckDriverDocRepository.GetFileDetailById(fileId);

            var deleteFileFromTruckDriverDoc = _truckDriverDocRepository.DeleteFileFromTruckDriverDoc(fileDetail);

            if (deleteFileFromTruckDriverDoc.Equals(true))
            {
                //Delete file from the file system
                var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), fileDetail.FileId + fileDetail.Extension);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                var listFilesByTruckDriverDocId =
                    _truckDriverDocRepository.ListFilesByTruckDriverDocId(fileDetail.TruckDriverDocId);

                var findTruckDriverDocAndTruckDriverById =
                    _truckDriverDocRepository.GetTruckDriverDocAndTruckDriverById(fileDetail.TruckDriverDocId);

                ListFileTruckDriverDocViewModel listFileTruckDriverDoc = new ListFileTruckDriverDocViewModel
                {
                    TruckDriverDocument = findTruckDriverDocAndTruckDriverById,
                    FileDetails = listFilesByTruckDriverDocId
                };

                return View("ListFilesTruckDriverDoc", listFileTruckDriverDoc);
            }

            return View(fileDetail);
        }


        public ActionResult PrintFileFromTruckDriverView(Guid fileId)
        {
            var fileDetail = _truckDriverDocRepository.GetFileDetailById(fileId);

            var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), fileDetail.FileId + fileDetail.Extension);

            return File(path, "application/pdf");

        }

        [HttpGet]
        public ViewResult ShareFileTruckDriverView(Guid fileId)
        {
            var fileDetail = _truckDriverDocRepository.GetFileDetailById(fileId);

            SentToMailViewModel sentToMail = new SentToMailViewModel
            {
                FileDetail = fileDetail,
            };

            return View("ShareFileTruckDriverView", sentToMail);
        }

        [HttpPost]
        [ActionName("ShareFileTruckDriverView")]
        public ActionResult ShareFileTruckDriver(SentToMailViewModel sentToMail)
        {
            var fileDetail = _truckDriverDocRepository.GetFileDetailById(sentToMail.FileDetail.FileId);

            var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), fileDetail.FileId + fileDetail.Extension);

            if (ModelState.IsValid)
            {
                string from = "boatbas57150@gmail.com"; //example:- sourabh9303@gmail.com

                using (MailMessage mail = new MailMessage(from, sentToMail.To))
                {
                    mail.Subject = sentToMail.Subject;
                    mail.Body = sentToMail.Body;
                    if (true)
                    {
                        string fileName = sentToMail.FileDetail.FileName;
                        mail.Attachments.Add(new Attachment(path, fileName));
                    }
                    mail.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        EnableSsl = true
                    };
                    NetworkCredential networkCredential = new NetworkCredential(from, "boatboat57150");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = networkCredential;
                    smtp.Port = 587;
                    smtp.Send(mail);
                    ViewBag.Message = "Sent";

                    var listFilesByTruckDriverDocId =
                        _truckDriverDocRepository.ListFilesByTruckDriverDocId(fileDetail.TruckDriverDocId);

                    var findTruckDriverDocAndTruckDriverById =
                        _truckDriverDocRepository.GetTruckDriverDocAndTruckDriverById(fileDetail.TruckDriverDocId);

                    ListFileTruckDriverDocViewModel listFileTruckDriverDoc = new ListFileTruckDriverDocViewModel
                    {
                        TruckDriverDocument = findTruckDriverDocAndTruckDriverById,
                        FileDetails = listFilesByTruckDriverDocId
                    };

                    return View("ListFilesTruckDriverDoc", listFileTruckDriverDoc);
                }
            }

            return View(sentToMail);
        }
    }
}