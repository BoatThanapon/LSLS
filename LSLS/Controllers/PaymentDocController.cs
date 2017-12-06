using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using LSLS.Models;
using LSLS.Repository.Abstract;
using LSLS.ViewModels;

namespace LSLS.Controllers
{
    [Authorize]
    public class PaymentDocController : Controller
    {
        
        private readonly IPaymentDocRepository _paymentDocRepository;

        public PaymentDocController(IPaymentDocRepository paymentDocRepository)
        {
            _paymentDocRepository = paymentDocRepository;
        }

        // GET: PaymentDoc
        [HttpGet]
        public ViewResult ListAllPaymentDocs()
        {
            var listAllPaymentDocs = 
                _paymentDocRepository.GetAllListPaymentDocuments();
            
            return View("ListAllPaymentDocs", listAllPaymentDocs);
        }

        // GET: UploadPaymentDoc
        [HttpGet]
        public ViewResult UploadFilePaymentDocView()
        {
            return View("UploadFilePaymentDocView");
        }

        // POST: UploadPaymentDoc
        [ActionName("UploadFilePaymentDocView")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadFilePaymentDoc(PaymentDocument paymentDocument)
        {
            DateTime dateTime = DateTime.UtcNow;
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var convertedTime = TimeZoneInfo.ConvertTime(dateTime, timeZoneInfo);

            if (ModelState.IsValid)
            {
                List<PaymentDocument> paymentDocuments = new List<PaymentDocument>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        PaymentDocument payment = new PaymentDocument
                        {
                            PaymentFileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            PaymentDocId = Guid.NewGuid(),
                            PaymentLastModified = convertedTime

                        };

                        if (payment.Extension == ".pdf")
                        {
                            paymentDocuments.Add(payment);
                            payment.PaymentDocuments = paymentDocuments;

                            var path = Path.Combine(Server.MapPath("~/App_Data/PaymentDoc/"), payment.PaymentDocId + payment.Extension);
                            file.SaveAs(path);


                            bool addPaymentDoc = _paymentDocRepository.AddPaymentDoc(payment);
                            if (addPaymentDoc.Equals(true))
                            {
                                _paymentDocRepository.SaveChanges();
                                return RedirectToAction("ListAllPaymentDocs");

                            }
                        }

                        ViewBag.Message = "Please, upload PDF File Only";
                        return View("UploadFilePaymentDocView",payment);
                    }

                }

            }

            return View("UploadFilePaymentDocView",paymentDocument);
        }

        //GET: PrintPaymentDocView
        [HttpGet]
        public FileResult PrintPaymentDocView(Guid paymentDocId)
        {
            var findPaymentDocument = _paymentDocRepository.GetPaymentDocumentById(paymentDocId);
            var path = Path.Combine(Server.MapPath("~/App_Data/PaymentDoc/"), findPaymentDocument.PaymentDocId + findPaymentDocument.Extension);


            return File(path, "application/pdf");
        }

        [HttpGet]
        public ViewResult ShareFilePaymentDocView(Guid paymentDocId)
        {
            var findPaymentDocument = _paymentDocRepository.GetPaymentDocumentById(paymentDocId);

            SharePaymentViewModel sentToMail = new SharePaymentViewModel
            {
                PaymentDocument = findPaymentDocument               
            };

            return View("ShareFilePaymentDocView", sentToMail);
        }

        [HttpPost]
        [ActionName("ShareFilePaymentDocView")]
        public ActionResult ShareFilePaymentDoc(SharePaymentViewModel sentToMail)
        {
            var findPaymentDocument = _paymentDocRepository.GetPaymentDocumentById(sentToMail.PaymentDocument.PaymentDocId);

            var path = Path.Combine(Server.MapPath("~/App_Data/PaymentDoc/"), findPaymentDocument.PaymentDocId + findPaymentDocument.Extension);

            if (ModelState.IsValid)
            {
                string from = "boatbas57150@gmail.com"; //example:- sourabh9303@gmail.com

                using (MailMessage mail = new MailMessage(from, sentToMail.To))
                {
                    mail.Subject = sentToMail.Subject;
                    mail.Body = sentToMail.Body;
                    if (true)
                    {
                        string fileName = sentToMail.PaymentDocument.PaymentFileName;
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

                    return RedirectToAction("ListAllPaymentDocs", "PaymentDoc");
                }
            }

            return View("ShareFilePaymentDocView",sentToMail);
        }


        [HttpGet]
        public ViewResult DeleteFilePaymentDocView(Guid paymentDocId)
        {
            var findPaymentDocument = _paymentDocRepository.GetPaymentDocumentById(paymentDocId);

            return View("DeleteFilePaymentDocView", findPaymentDocument);
        }

        // POST: DeleteFileFromTruckDriver
        [HttpPost]
        [ActionName("DeleteFilePaymentDocView")]
        public ActionResult DeleteFileFromPaymentDoc(Guid paymentDocId)
        {
            var findPaymentDocument = _paymentDocRepository.GetPaymentDocumentById(paymentDocId);

            var deletePaymentDocument = _paymentDocRepository.DeleteFileFromPaymentDoc(findPaymentDocument);

            if (deletePaymentDocument.Equals(true))
            {
                //Delete file from the file system
                var path = Path.Combine(Server.MapPath("~/App_Data/PaymentDoc/"), findPaymentDocument.PaymentDocId + findPaymentDocument.Extension);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                return RedirectToAction("ListAllPaymentDocs","PaymentDoc");
            }

            return View(findPaymentDocument);

        }
    }
}