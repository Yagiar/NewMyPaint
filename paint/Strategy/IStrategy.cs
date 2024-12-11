using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace paint.Strategy
{
    internal interface IStrategy
    {
        void MouseMove(MouseEventArgs e);
        void MouseDown(MouseEventArgs e);
        void MouseUp(MouseEventArgs e, int T, Brush my);
    }
}
