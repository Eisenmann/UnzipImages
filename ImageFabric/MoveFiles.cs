using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ImageFabric
{
    class MoveFiles
    {
        //create temp folder
        public void CreateFolderTemp()
        {
            if (!Directory.Exists("temp"))
            {
                Directory.CreateDirectory("temp");
            }
        }

        public void DeleteFolderTemp()
        {
            if (Directory.Exists("temp"))
            {
                Directory.Delete("temp", true);
            }
        
        }

        public void MoveAllFiles(string outputDir, List<FileAndDir> filesList)
        {
             string destFN = String.Empty;
             string sourceFN = String.Empty;
             string tmpFolder = "temp";

             foreach (FileAndDir fileName in filesList)
             {
                 sourceFN = "temp" + "\\" + Path.GetFileName(fileName.fileName);
                 destFN = outputDir + "\\" + Path.GetFileName(fileName.fileName);
                 
                 if(!File.Exists(destFN))
                 {
                 File.Move(sourceFN, destFN);
                 }
             }
        
        }

        //collect arhive or image files
        public List<FileAndDir> CollectFiles(string filesDir, string fileExtension)
        {
            List<FileAndDir> fileCollection = new List<FileAndDir>();
            

            if (Directory.Exists(filesDir))
            {
                // Process the list of files found in the directory.
                string[] fileEntries = Directory.GetFiles(filesDir, "*", SearchOption.AllDirectories);
                foreach (string fileName in fileEntries)
                {
                    if(fileExtension.Contains(Path.GetExtension(fileName)))
                    {
                        FileAndDir fnd = new FileAndDir();
                        fnd.fileName = fileName;
                        fnd.fileDir = filesDir;
                      fileCollection.Add(fnd);
                    }
                }
            }
             

            return fileCollection;
        }

        //convert current datetime to string
        static string DateToString()
        {
            string value = String.Empty;

            value = DateTime.Now.ToString("yyyyMMddHHmmss");

            return value;
        }
    }

  /*  enum FileExt
    { 
       ".jpg", 
        ".png",
       ".zip",
    ".rar"
    }*/

    public class FileAndDir
    {
        public string fileName { get; set; }
        public string fileDir { get; set; }
    }
}
