using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ICSharpCode.SharpZipLib.Zip;

namespace AudioNetwork.Helpers
{
    public class SharpZipLibHelper
    {
        public static void ZipFolder(string folderPath, ZipOutputStream zipStream)  
        {  
            string path = !folderPath.EndsWith("\\") ? string.Concat(folderPath, "\\") : folderPath;  
            ZipFolder(path, path, zipStream);  
        }  
  
        private static void ZipFolder(string RootFolder, string CurrentFolder,  
            ZipOutputStream zipStream)  
        {  
            string[] SubFolders = Directory.GetDirectories(CurrentFolder);  
            foreach (string Folder in SubFolders)  
            {  
                ZipFolder(RootFolder, Folder, zipStream);  
            }  
            string relativePath = string.Concat(CurrentFolder.Substring(RootFolder.Length), "/");  
            if (relativePath.Length > 1)  
            {  
                ZipEntry dirEntry;  
                dirEntry = new ZipEntry(relativePath);  
                dirEntry.DateTime = DateTime.Now;  
  
            }  
            foreach (string file in Directory.GetFiles(CurrentFolder))  
            {  
                AddFileToZip(zipStream, relativePath, file);  
            }  
        }  
  
        public static void AddFileToZip(ZipOutputStream zStream, string relativePath, string file)  
        {  
            byte[] buffer = new byte[4096];  
           // string fileRelativePath = string.Concat((relativePath.Length > 1 ? relativePath : string.Empty), Path.GetFileName(file));  

            ZipEntry entry = new ZipEntry(file);  
            entry.DateTime = DateTime.Now;  
            zStream.PutNextEntry(entry);

            using (FileStream fs = File.OpenRead(relativePath))  
            {  
                int sourceBytes;  
                do  
                {  
                    sourceBytes = fs.Read(buffer, 0, buffer.Length);  
                    zStream.Write(buffer, 0, sourceBytes);  
                } while (sourceBytes > 0);  
            }  
  
        }  
    }
}