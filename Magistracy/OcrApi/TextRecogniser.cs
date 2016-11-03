using System;
using System.Diagnostics;
using System.IO;

namespace OcrApi
{
    public class TextRecogniser
    {
        private const string toolPath = @"OcrTool\bin\Debug\OcrTool.exe";
        public string Recognise(string filePath)
        {
            var ocrTool = new Process
            {
                StartInfo =
                {
                    FileName = Path.Combine(RootDirectory,toolPath),
                    Arguments = filePath,
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                }
            };
            ocrTool.Start();

            string output = ocrTool.StandardOutput.ReadToEnd();
            ocrTool.WaitForExit();
          
            return output;
        }

        private string RootDirectory
        {
            get { return Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName; }
        }
    }
}
