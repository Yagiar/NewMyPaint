using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NewMyPaint
{
    internal class MyCircle : Figure
    {
        double x, y;
        Ellipse myCircle;
        public double radius => Math.Sqrt((endPoint.X - startPoint.X) * (endPoint.X - startPoint.X) + (endPoint.Y - startPoint.Y) * (endPoint.Y - startPoint.Y));
        public override Shape GetShape()
        {
            return myCircle;
        }
        public override void Show(Canvas canvas)
        {
            myCircle = new Ellipse();
            x = startPoint.X;
            y = startPoint.Y;
            myCircle.Width = radius;
            myCircle.Height = radius;
            myCircle.StrokeThickness = 5;
            myCircle.Stroke = Brushes.Black;
            myCircle.Fill = Brushes.Black;
            Canvas.SetTop(myCircle, y);
            Canvas.SetLeft(myCircle, x);
            canvas.Children.Add(myCircle);
        }
    }
}