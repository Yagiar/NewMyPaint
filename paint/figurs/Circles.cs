using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace paint
{
    class Circles : Fig
    {
        double x, y;
        Ellipse circle;
        TextBlock text;
        Brush my1 = null;
        public double R
        {
            get
            {
                return Math.Max(Math.Abs(point2.X - point1.X), Math.Abs(point2.Y - point1.Y)) ;
            }
        }
        public double X
        {
            get { return x; }
            set { x = value; }
        }
        public double Y
        {
            get { return y; }
            set { y = value; }
        }
        public override void Fill(Brush my)
        {
            circle.Fill = my;
        }
        public override Shape GetFigure()
        {
            return circle;
        }
        public override void Show(Canvas panel)
        {
            circle = new Ellipse();
            X = point1.X;
            Y = point1.Y;
            circle.Width = Math.Abs(point2.X - point1.X) * 2;
            circle.Height = Math.Abs(point2.Y - point1.Y) * 2;
            circle.Stroke = MyBrush;
            circle.Fill = my1;
            circle.StrokeThickness = th;
            Canvas.SetTop(circle, Y- Math.Abs(point2.Y - point1.Y));
            Canvas.SetLeft(circle, X- Math.Abs(point2.X - point1.X) );
            panel.Children.Add(circle);
            return;

        }
        public override void Rm(Canvas panel)
        {
            panel.Children.Remove(circle);
        }
    }
}
