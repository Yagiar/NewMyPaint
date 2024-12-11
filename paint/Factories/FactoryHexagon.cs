using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace paint
{
    internal class FactoryHexagon : Factory
    {
        public override Fig GetShape()
        {
            return new Hexagons();
        }
    }
}
