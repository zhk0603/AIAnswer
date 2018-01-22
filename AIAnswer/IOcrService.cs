using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIAnswer
{
    public interface IOcrService
    {
        void Init();
        string Process(System.IO.Stream stream);
    }
}
