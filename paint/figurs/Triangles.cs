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
    class Triangles : Fig
    {
        Polygon t;
        Point p1, p2, p3;
        TextBlock text;
        public override Shape GetFigure()
        {
            return t;
        }
        public override void Show(Canvas panel)
        {
            if (t == null)
            {
                t = new Polygon();

                if (point1.Y - point2.Y > 0)
                {
                    t.Width = Math.Abs(point2.X - point1.X);
                    t.Height = Math.Abs(point2.Y - point1.Y);
                    Canvas.SetLeft(t, Math.Min(point1.X, point2.X));
                    Canvas.SetTop(t, Math.Min(point1.Y, point2.Y));
                    p1.X = 0 + th;
                    p1.Y = 0 + th / 2;
                    p2.X = (point1.X - point2.X) - th;
                    p2.Y = 0 + th / 2;
                    p3.X = 0 + ((point1.X - point2.X)) / 2 - th / 2;
                    p3.Y = 0 + (point1.Y - point2.Y) - th / 2;
                }
                else
                {
                    t.Width = Math.Abs(point1.X - point2.X);
                    t.Height = Math.Abs(point1.Y - point2.Y);
                    Canvas.SetLeft(t, Math.Min(point1.X, point2.X));
                    Canvas.SetTop(t, Math.Min(point1.Y, point2.Y));
                    p1.X = 0 + th;
                    p1.Y = (point2.Y - point1.Y) - th;
                    p2.X = (point2.X - point1.X) - th;
                    p2.Y = (point2.Y - point1.Y) - th;
                    p3.X = 0 + ((point2.X - point1.X)) / 2 - th / 2;
                    p3.Y = 0 + th;
                }

                t.Points.Add(p1);
                t.Points.Add(p2);
                t.Points.Add(p3);
                t.StrokeThickness = th;
                t.Stroke = MyBrush;
            }
            panel.Children.Add(t);
            return;

        }
        public override void Rm(Canvas panel)
        {
            panel.Children.Remove(t);
        }
       
    }
}
