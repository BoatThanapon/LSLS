using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using LSLS.Models;
using LSLS.Repository.Abstract;

namespace LSLS.Repository
{
    public class FileOnlineRepository : IFileOnlineRepository
    {
        //defined scope.
        public static string[] Scopes = { DriveService.Scope.Drive };

        //create Drive API service.
        public static DriveService GetService()
        {
            //get Credentials from client_secret.json file 
            UserCredential credential;
            using (var stream = new FileStream(@"D:\client_secret.json", FileMode.Open, FileAccess.Read))
            {
                String FolderPath = @"D:\";
                String FilePath = Path.Combine(FolderPath, "DriveServiceCredentials.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(FilePath, true)).Result;
            }

            //create Drive API service.
            DriveService service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "GoogleDriveRestAPI-v3",
            });
            return service;
        }
        public List<FileOnline> GetAllFiles()
        {
            throw new NotImplementedException();
        }

        public bool FileUpload(HttpPostedFileBase file)
        {
            throw new NotImplementedException();
        }

        public string DowloadFile(string fileId)
        {
            throw new NotImplementedException();
        }

        public bool SaveStream(MemoryStream stream, string filePath)
        {
            throw new NotImplementedException();
        }

        public bool DeleteFile(FileOnline file)
        {
            throw new NotImplementedException();
        }
    }
}