using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LSLS.Models;

namespace LSLS.Repository.Abstract
{
    public interface IFileOnlineRepository
    {
        List<FileOnline> GetAllFiles();
        bool FileUpload(HttpPostedFileBase file);
        string DowloadFile(string fileId);
        bool SaveStream(MemoryStream stream, string filePath);
        bool DeleteFile(FileOnline file);
    }
}
