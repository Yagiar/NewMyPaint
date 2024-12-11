using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace paint
{
    internal abstract class Command
    {
        public Fig Figs { get; set; }
        public virtual void Execute()
        {
            return;
        }
        public virtual void undo()
        {
            return;
        }
    }
}
