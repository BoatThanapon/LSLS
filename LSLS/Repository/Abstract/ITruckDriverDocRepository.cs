using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSLS.Models;

namespace LSLS.Repository.Abstract
{
    public interface ITruckDriverDocRepository
    {
        IEnumerable<TruckDriverDocument> GetAllListTruckDriverDocuments();
        TruckDriverDocument GetTruckDriverDocumentById(int truckDriverDocId);
        TruckDriverDocument GetTruckDriverDocAndTruckDriverById(int truckDriverDocId);
        IEnumerable<FileDetail> ListFilesByTruckDriverDocId(int truckDriverDocId);
        FileDetail GetFileDetailById(Guid fileId);

        void SaveChanges();

        bool DeleteFileFromTruckDriverDoc(FileDetail fileDetail);
    }
}
