using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using LSLS.Models;
using LSLS.Repository.Abstract;

namespace LSLS.Repository
{
    public class TruckDriverDocRepository : ITruckDriverDocRepository
    {
        private readonly ApplicationDbContext _context;

        public TruckDriverDocRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TruckDriverDocument> GetAllListTruckDriverDocuments()
        {
            IEnumerable<TruckDriverDocument> listAllTruckDriverDocuments =
                _context.TruckDriverDocuments.Include(j => j.TruckDriver).ToList();

            return listAllTruckDriverDocuments;
        }

        public TruckDriverDocument GetTruckDriverDocumentById(int truckDriverDocId)
        {
            TruckDriverDocument getTruckDriverDocumentById = _context.TruckDriverDocuments.Find(truckDriverDocId);

            return getTruckDriverDocumentById;
        }

        public TruckDriverDocument GetTruckDriverDocAndTruckDriverById(int truckDriverDocId)
        {
            TruckDriverDocument getTruckDriverDocAndTruckDriverById = _context.TruckDriverDocuments.Include(j => j.TruckDriver)
                .SingleOrDefault(t => t.TruckDriverDocId == truckDriverDocId);

            return getTruckDriverDocAndTruckDriverById;
        }

        public IEnumerable<FileDetail> ListFilesByTruckDriverDocId(int truckDriverDocId)
        {
            IEnumerable<FileDetail> listFilesByTruckDriverDocId =
                _context.FileDetails.Where(f => f.TruckDriverDocId == truckDriverDocId);

            return listFilesByTruckDriverDocId;
        }

        public FileDetail GetFileDetailById(Guid fileId)
        {
            FileDetail fileDetail = _context.FileDetails.SingleOrDefault(f => f.FileId == fileId);

            return fileDetail;
        }

        public bool AddFileDetails(FileDetail fileDetail)
        {
            if (fileDetail == null)
            {
                return false;
            }

            _context.FileDetails.Add(fileDetail);
            return true;
        }


        public void SaveChanges()
        {           
            _context.SaveChanges();
        }

        public bool DeleteFileFromTruckDriverDoc(FileDetail fileDetail)
        {
            if (fileDetail != null)
            {
                _context.FileDetails.Remove(fileDetail);
                SaveChanges();
                return true;
            }

            return false;
        }


    }
}