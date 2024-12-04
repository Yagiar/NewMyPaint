using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;

namespace NewMyPaint
{
    internal class MyTriangle : Figure
    {
        Polygon myTriangle;
        Point p1, p2, p3;
        public override Shape GetShape()
        {
            return myTriangle;
        }
        public override void Show(Canvas canvas)
        {
            myTriangle = new Polygon();
            p1.X = startPoint.X;
            p1.Y = endPoint.Y;
            p2.X = endPoint.X;
            p2.Y = endPoint.Y;
            p3.X = startPoint.X + (endPoint.X - startPoint.X) / 2;
            p3.Y = startPoint.Y;
            myTriangle.Points.Add(p1);
            myTriangle.Points.Add(p2);
            myTriangle.Points.Add(p3);
            myTriangle.StrokeThickness = 5;
            myTriangle.Stroke = Brushes.Black;
            myTriangle.Fill = Brushes.Black;
            canvas.Children.Add(myTriangle);
        }
    }
}
