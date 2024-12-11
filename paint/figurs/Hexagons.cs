using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows; 

namespace paint
{
    class Hexagons : Fig
    {
        Polygon hex;
        Point p1,p2,p3,p4,p5,p6;
        TextBlock text;
        public override Shape GetFigure()
        {
            return hex;
        }
        public override void Show(Canvas panel)
        {
            if (hex == null)
            {
                double x = point2.X - point1.X;
                double y = point2.Y - point1.Y;
                hex = new Polygon();
                p1.X = point1.X + x / 2;
                p1.Y = point1.Y;
                p2.X = point1.X;
                p2.Y = point1.Y + y / 3;
                p3.X = point1.X;
                p3.Y = point1.Y + y / 3 * 2;
                p4.X = point1.X + x / 2;
                p4.Y = point2.Y;
                p5.X = point2.X;
                p5.Y = point1.Y + y / 3 * 2;
                p6.X = point2.X;
                p6.Y = point1.Y + y / 3;
                hex.Points.Add(p1);
                hex.Points.Add(p2);
                hex.Points.Add(p3);
                hex.Points.Add(p4);
                hex.Points.Add(p5);
                hex.Points.Add(p6);
                hex.StrokeThickness = th;
                hex.Stroke = MyBrush;
                hex.Width = hex.Points.Max(p => p.X) + th;
                hex.Height = hex.Points.Max(p => p.Y) + th;
                Canvas.SetLeft(hex, 0);
                Canvas.SetTop(hex, 0);
            }
            panel.Children.Add(hex);
            return;

        }
        public override void Rm(Canvas panel)
        {
            panel.Children.Remove(hex);
        }

    }
}
