using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIAnswer
{
    class Program
    {
        static void Main(string[] args)
        {
            var ocrSvc = OcrServiceFactory.Create();
            ocrSvc.Init();

            while (true)
            {
                Console.WriteLine("按任意键开始");
                Console.ReadKey();

                using (var screensStream = AdbHelper.GetMobileScreens())
                {
                    using (var stream = ImageHelper.CaptureImage(screensStream, 0, 490, 1080, 1280))
                    {
                        var problem = ocrSvc.Process(stream);
                        Console.WriteLine(problem);

                        ProblemAnalyzer.Analyzer(problem);

                        foreach (var item in problem.Answer.OrderByDescending(x => x.Pmi))
                        {
                            Console.WriteLine($"    {item.Order}. {item.Title} PMI:{item.Pmi}");
                        }
                    }
                }
            }
        }
    }
}
