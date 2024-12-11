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
        class Rectangles : Fig
        {
            Polygon r;
            Point p1, p2, p3, p4;
            TextBlock text;
            public override void Show(Canvas panel)
            {
                if (r == null)
                {
                    r = new Polygon();
                    r.Width = Math.Abs(point1.X - point2.X);
                    r.Height = Math.Abs(point1.Y - point2.Y);
                    Canvas.SetLeft(r, Math.Min(point1.X, point2.X));
                    Canvas.SetTop(r, Math.Min(point1.Y, point2.Y));
                    p1.X = 0 + th / 2;
                    p1.Y = 0 + th / 2;
                    p2.X = 0 + th / 2;
                    p2.Y = Math.Abs(point1.Y - point2.Y) - th / 2;
                    p3.X = Math.Abs(point1.X - point2.X) - th / 2;
                    p3.Y = Math.Abs(point1.Y - point2.Y) - th / 2;
                    p4.X = Math.Abs(point1.X - point2.X) - th / 2;
                    p4.Y = 0 + th / 2;
                    r.Points.Add(p1);
                    r.Points.Add(p2);
                    r.Points.Add(p3);
                    r.Points.Add(p4);
                    r.StrokeThickness = th; 
                    r.Stroke = MyBrush;
                }
                panel.Children.Add(r);
                return;

            }
            public override Shape GetFigure()
            {
                return r;
            }
            public override void Rm(Canvas panel)
            {
                panel.Children.Remove(r);
            }

        }
    }
