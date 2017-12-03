using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LSLS.Models;
using LSLS.Repository.Abstract;

namespace LSLS.Repository
{
    public class PaymentDocRepository : IPaymentDocRepository
    {
        
        private readonly ApplicationDbContext _context;

        public PaymentDocRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<PaymentDocument> GetAllListPaymentDocuments()
        {
            IEnumerable<PaymentDocument> listAllPaymentDocuments = _context.PaymentDocuments.ToList();

            return listAllPaymentDocuments;
        }

        public PaymentDocument GetPaymentDocumentById(Guid paymentDocId)
        {
            PaymentDocument paymentDocument =
                _context.PaymentDocuments.SingleOrDefault(p => p.PaymentDocId == paymentDocId);

            return paymentDocument;
        }

        public bool DeleteFileFromPaymentDoc(PaymentDocument paymentDocument)
        {
            if (paymentDocument != null)
            {
                _context.PaymentDocuments.Remove(paymentDocument);
                SaveChanges();
                return true;
            }

            return false;
        }

        public bool AddPaymentDoc(PaymentDocument paymentDocument)
        {
            if (paymentDocument == null)
            {
                return false;
            }

            _context.PaymentDocuments.Add(paymentDocument);
            return true;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}