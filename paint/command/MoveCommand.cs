using paint.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace paint.command
{
    internal class MoveCommand : Command
    {
        Point prev;
        Point new_Point;
        Fig BaseFig;
        Canvas canvas1;
        CollectionFig collection1;
        public override void Execute()
        {
            Figs = new MoveFigure(BaseFig);
            Figs.GetFormattedFigure(new Point(new_Point.X, new_Point.Y));
            return;
        }
        public void move(Canvas canvas, Fig Fig, CollectionFig collection, Point p)
        {
            canvas1 = canvas;
            collection1 = collection;
            BaseFig = Fig;
            prev = new Point(0-p.X,0-p.Y);
            new_Point = p;
        }
        public override void undo()
        {
            Figs = new MoveFigure(BaseFig);
            Figs.GetFormattedFigure(new Point(prev.X, prev.Y));
            BaseFig = Figs;
            return;
        }
    }
}
