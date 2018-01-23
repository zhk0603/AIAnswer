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
            IOcrService svc;
            switch (type)
            {
                case "Tesseract":
                    svc = new TesseractSvc();
                    break;
                case "BaiDu":
                default:
                    svc = new BaiduOcrSvc();
                    break;
            }
            svc.Init();
            return svc;
        }
    }
}
