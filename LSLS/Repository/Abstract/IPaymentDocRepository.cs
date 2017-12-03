using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSLS.Models;

namespace LSLS.Repository.Abstract
{
    public interface IPaymentDocRepository
    {
        IEnumerable<PaymentDocument> GetAllListPaymentDocuments();
        PaymentDocument GetPaymentDocumentById(Guid paymentDocId);
        bool DeleteFileFromPaymentDoc(PaymentDocument paymentDocument);

        bool AddPaymentDoc(PaymentDocument paymentDocument);
        void SaveChanges();
    }
}
