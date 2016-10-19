using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PathsTester
{
    class Program
    {
        private const string R_Scripts = @"TextMining\R_Scripts\";
        static void Main(string[] args)
        {
            string cyrillicText = "РљСЂР°СЃРЅРѕРІ1.txt";
            System.Text.UTF8Encoding encodingUnicode = new System.Text.UTF8Encoding();
            byte[] cyrillicTextByte = encodingUnicode.GetBytes(cyrillicText);
            Console.WriteLine(encodingUnicode.GetString(cyrillicTextByte));
            var a = GetEncoding(@"D:\GitReps\Personal\Magistracy\AudioNetwork\Content\TextMining\1122\Краснов1.txt");
        }

        public static Encoding GetEncoding(string filename)
        {
            // Read the BOM
            var bom = new byte[4];
            using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 4);
            }

            // Analyze the BOM
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
            if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode; //UTF-16LE
            if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return Encoding.UTF32;
            return Encoding.ASCII;
        }
    }
}
