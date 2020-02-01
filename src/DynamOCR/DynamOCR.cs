using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace DynamOCR
{
    public class DynamOCR
    {
        private DynamOCR() { }
        public static Dictionary<string,object> ReadImage(string filePath)
        {
            //access the dynamo dll path (typically appdata bin folder), then construct the location of the tessdata folder
            string dllPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string extraFolder = dllPath.Replace("bin\\DynamOCR.dll", "extra\\tessdata");

            TesseractEngine tesseractEngine = new TesseractEngine(extraFolder, "eng");

            var pix = PixConverter.ToPix(new System.Drawing.Bitmap(filePath));
            var result = tesseractEngine.Process(pix);

            string confidence = result.GetMeanConfidence().ToString();
            string text = result.GetText();

            //returns the outputs
            var outInfo = new Dictionary<string, object>
                {
                    { "confidence", confidence},
                    { "text", text}
                };
            return outInfo;

        }
    }
}
