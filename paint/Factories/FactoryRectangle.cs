using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace paint
{
    internal class FactoryRectangle : Factory
    {
        public override Fig GetShape()
        {
            return new Rectangles();
        }
    }
}
