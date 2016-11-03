using System;
using MathWorks.MATLAB.NET.Arrays;
using Recognise;

namespace OcrTool
{
    static class Program
    {
        static void Main(string[] args)
        {

            var imageTextRecogniser = new TextRecogniser();
            var imagePath = args[0];

            if (string.IsNullOrEmpty(imagePath))
            {
                return;
            }

            MWArray[] result = imageTextRecogniser.Recognise(
                1, new MWCharArray(imagePath), new MWCharArray("English"));


            Console.WriteLine(result[0]);

        }
    }
}
