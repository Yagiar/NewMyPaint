using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMyPaint
{
    public interface Command
    {
        void Execute();
        void Undo();
    }
}
