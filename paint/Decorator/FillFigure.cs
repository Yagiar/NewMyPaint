using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace paint.Decorator
{
    internal class FillFigure:Fig
    {
        Fig _editFigure;
        public FillFigure(Fig editFigure = null) : base(editFigure)
        {
            if(editFigure != null)
                _editFigure = editFigure;
            
        }
        public override Fig GetFormattedFigure(Point mousePosition, Brush brush = null)
        {
            if (_editFigure is CollectionFigures figures)
            {
                foreach (var figure in figures.figures)
                {
                    figure.Fill(brush);
                    figure.FillBrush = brush;
                }
            }
            else
            {
                _editFigure.Fill(brush);
                FillBrush = brush;
            }
            return this;
        }
    }
}
