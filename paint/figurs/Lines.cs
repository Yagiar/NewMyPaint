using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;




namespace paint
{
    class Lines : Fig
    {
        Line line;
        Circles c,c1;
        TextBlock text;
        public override Shape GetFigure()
        {
            return line;
        }
        public override void Show(Canvas panel)
        { 
            line = new Line();
            line.X1 = point1.X;
            line.Y1 = point1.Y;
            line.X2 = point2.X;
            line.Y2 = point2.Y;
            line.Stroke = MyBrush;
            line.StrokeThickness = th;
            Canvas.SetLeft(line, 0);
            Canvas.SetTop(line, 0);
            panel.Children.Add(line);
            return;

        }

        public override void Rm(Canvas panel)
        {
            panel.Children.Remove(line);

        }
    }
}