using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace paint.Decorator
{
    internal class MoveFigure : Fig
    {
        Fig _editFigure;
        public MoveFigure(Fig editFigure = null) : base(editFigure)
        {
            if (editFigure != null)
                _editFigure = editFigure;
        }
        public override Fig GetFormattedFigure(Point mousePosition, Brush brush = null)
        {
            if (_editFigure is CollectionFigures figures)
            {
                foreach (var figure in figures.figures)
                {
                    double newLeft = Canvas.GetLeft(figure.GetFigure()) + mousePosition.X;
                    double newTop = Canvas.GetTop(figure.GetFigure()) + mousePosition.Y;
                    Canvas.SetLeft(figure.GetFigure(), newLeft);
                    Canvas.SetTop(figure.GetFigure(), newTop);
                    point1 = new Point(newLeft, newTop);
                    point2 = new Point(newLeft + figure.GetFigure().Width, newTop + figure.GetFigure().Height);
                }
            }
            else
            {
                double newLeft = Canvas.GetLeft(GetFigure()) + mousePosition.X;
                double newTop = Canvas.GetTop(GetFigure()) + mousePosition.Y;
                Canvas.SetLeft(GetFigure(), newLeft);
                Canvas.SetTop(GetFigure(), newTop);
                point1 = new Point(newLeft, newTop);
                point2 = new Point(newLeft + GetFigure().Width, newTop + GetFigure().Height);
            }
            _editFigure.updateOutline();
            return this;
        }
    }
}
