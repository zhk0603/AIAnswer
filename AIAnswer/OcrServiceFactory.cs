using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIAnswer
{
    public class OcrServiceFactory
    {
        public static IOcrService Create()
        {
            var type = System.Configuration.ConfigurationManager.AppSettings["OcrType"];
            switch (type)
            {
                case "Tesseract":
                    return new TesseractSvc();
                case "BaiDu":
                default:
                    return new BaiduOcrSvc();
            }
        }
    }
}
