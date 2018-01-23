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
        Problem Process(System.IO.Stream stream);
    }
}
