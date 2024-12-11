using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace paint
{
    abstract class Factory
    {
        public virtual Fig GetShape()
        { return null; }
    }
}
