using System;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace NewMyPaint
{
    internal class MyRectangle : Figure
    {
        Polygon myRectangle;
        Point p1, p2, p3, p4;
        public override Shape GetShape()
        {
            return myRectangle;
        }
        public override void Show(Canvas canvas)
        {
            myRectangle = new Polygon();
            p1.X = startPoint.X;
            p1.Y = startPoint.Y;
            p2.X = startPoint.X;
            p2.Y = endPoint.Y;
            p3.X = endPoint.X;
            p3.Y = endPoint.Y;
            p4.X = endPoint.X;
            p4.Y = startPoint.Y;
            myRectangle.Points.Add(p1);
            myRectangle.Points.Add(p2);
            myRectangle.Points.Add(p3);
            myRectangle.Points.Add(p4);
            myRectangle.StrokeThickness = 5;
            myRectangle.Stroke = Brushes.Black;
            myRectangle.Fill = Brushes.Black;
            canvas.Children.Add(myRectangle);
        }
    }
}
