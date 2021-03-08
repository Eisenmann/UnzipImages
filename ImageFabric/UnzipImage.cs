using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace ImageFabric
{
   public class UnzipImage
    {

        public void Unzip(string fileName, string app7ZipPath, string zipManager)
        {
            string outputDir = "temp";
            zipManager = app7ZipPath;

            try {
                if (!File.Exists(app7ZipPath))
                    throw new Exception("Archive manager not found");
                if (!File.Exists(app7ZipPath))
                    throw new Exception("Archive not found");
                if (!Directory.Exists(outputDir))
                    throw new Exception("Output directory not found");

                ProcessStartInfo startInfo = new ProcessStartInfo();

                startInfo.FileName = zipManager;
                startInfo.Arguments = " e";
                startInfo.Arguments += " -y";
                startInfo.Arguments += " " + "\"" + fileName+"\"";
                startInfo.Arguments += " -o" + "\"" + outputDir + "\"";
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                int sevenZipExitCode = 0;
                using(Process sevenZip = Process.Start(startInfo))
                {
                    sevenZip.WaitForExit();
                    sevenZipExitCode = sevenZip.ExitCode;
                }

                if(sevenZipExitCode != 0 && sevenZipExitCode!= 1)
                {
                using(Process sevenZip = Process.Start(startInfo))
                {
                    Thread.Sleep(500);
                    sevenZip.WaitForExit();
                    switch(sevenZip.ExitCode)
                    {
                        case 0: return;
                        case 1: return;
                        case 2: throw new Exception("Fatal error");
                        case 7: throw new Exception("Exception in comman line");
                        case 8: throw new Exception("Low memory");
                        case 225: throw new Exception("User wnd operation");
                        default: throw new Exception("7Zip error code:" + sevenZip.ExitCode.ToString());
                    }
                }
                }

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        
    }
}
