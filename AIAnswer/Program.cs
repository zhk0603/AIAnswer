using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIAnswer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!AdbHelper.CheckConnect())
            {
                Console.WriteLine("手机尚未连接");
                return;
            }

            var ocrSvc = OcrServiceFactory.Create();

            while (true)
            {
                Console.WriteLine("按任意键开始");
                Console.ReadKey();
                try
                {
                    using (var screensStream = AdbHelper.GetMobileScreens())
                    {
                        using (var stream = ImageHelper.CaptureImage(screensStream, 0, 490, 1080, 1280))
                        {
                            var tmpStream = File.Open(@"C:\Users\Administrator\Desktop\images\tmp.jpeg", FileMode.Open);
                            var problem = ocrSvc.Process(tmpStream);
                            Console.WriteLine(problem);

                            ProblemAnalyzer.Analyzer(problem);

                            foreach (var item in problem.Answer.OrderByDescending(x => x.Pmi))
                            {
                                Console.WriteLine($"    {item.Order}. {item.Title} PMI:{item.Pmi}");
                            }
                            tmpStream.Close();
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
