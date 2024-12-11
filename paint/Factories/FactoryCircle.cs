using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;

namespace paint
{
    internal class FactoryCircle : Factory
    {
        public override Fig GetShape()
        {
            return new Circles();
        }
    }
}
