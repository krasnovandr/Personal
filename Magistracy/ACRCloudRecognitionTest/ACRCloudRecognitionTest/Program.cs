/*
   @author qinxue.pan E-mail: xue@acrcloud.com
   @version 1.0.0
   @create 2015.10.01 
 
Copyright 2015 ACRCloud Recognizer v1.0.0

This module can recognize ACRCloud by most of audio/video file. 
        Audio: mp3, wav, m4a, flac, aac, amr, ape, ogg ...
        Video: mp4, mkv, wmv, flv, ts, avi ...
  
 */

using System;
using System.Collections.Generic;
using System.IO;
using MusicRecognition.Services;

namespace MusicRecognition
{
    
    //class Program
    //{
    //    static void Main() { }
    //}
    class Program
    {
        //metainfos https://docs.acrcloud.com/metadata
        static void Main(string[] args)
        {
            var config = new Dictionary<string, object>();
            config.Add("host", "ap-southeast-1.api.acrcloud.com");
            // Replace "XXXXXXXX" below with your project's access_key and access_secret
            config.Add("access_key", "f958570586f34fb73c685ce1cbfaa805");
            config.Add("access_secret", "4kMjaDTGYZo5gBW47zqansDKgNptLzHiwpIu76Ry");
            config.Add("timeout", 10); // seconds

            /**
              *   
              *  recognize by file path of (Formatter: Audio/Video)
              *     Audio: mp3, wav, m4a, flac, aac, amr, ape, ogg ...
              *     Video: mp4, mkv, wmv, flv, ts, avi ...
              *     
              * 
             **/

            ACRCloudRecognizer re = new ACRCloudRecognizer(config);

            // It will skip 80 seconds from the beginning of test.mp3.
            string result = re.RecognizeByFile(@"D:\output.wav", 0);
            Console.WriteLine(result);

            /**
              *   
              *  recognize by buffer of (Formatter: Audio/Video)
              *     Audio: mp3, wav, m4a, flac, aac, amr, ape, ogg ...
              *     Video: mp4, mkv, wmv, flv, ts, avi ...
              *     
              * 
              **/
            using (FileStream fs = new FileStream(@"D:\1.wav", FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    byte[] datas = reader.ReadBytes((int)fs.Length);
                    // It will skip 80 seconds from the beginning of datas.
                    result = re.RecognizeByFileBuffer(datas, datas.Length, 0);
                    Console.WriteLine(result);
                }
            }

            //ACRCloudExtrTool acrTool = new ACRCloudExtrTool();
            //byte[] fp = acrTool.CreateFingerprintByFile("test.mp3", 80, 12, false);
            //Console.WriteLine(fp.Length);

            //byte[] audioBuffer = acrTool.DecodeAudioByFile("test.mp3", 80, 0);
            //Console.WriteLine(audioBuffer.Length);

            /*using (FileStream fs = new FileStream(@"test.mp3", FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    byte[] datas = reader.ReadBytes((int)fs.Length);
                    byte[] fpt = acrTool.CreateFingerprintByFileBuffer(datas, datas.Length, 80, 12, false);                
                    Console.WriteLine(fpt.Length);
                }
            }*/

            Console.ReadLine();
        }
    }
}
