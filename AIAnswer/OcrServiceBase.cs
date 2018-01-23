using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIAnswer
{
    public abstract class OcrServiceBase : IOcrService
    {
        public abstract void Init();

        public Problem Process(Stream stream)
        {
            Console.WriteLine("开始OCR");
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            var res = ProcessCore(stream);
            sw.Stop();
            Console.WriteLine("OCR耗时：" + sw.ElapsedMilliseconds);
            return res;
        }

        protected abstract Problem ProcessCore(Stream stream);
    }
}
