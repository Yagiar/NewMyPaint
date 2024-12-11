using paint.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace paint.command
{
    internal class FillCommand : Command
    {
        Brush prev;
        Brush new_brush;
        Fig BaseFig;
        Canvas canvas1;
        CollectionFig collection1;
        public override void Execute()
        {
            BaseFig.Rm(canvas1);
            collection1.Remove(BaseFig);
            Fig formattedfigure = new FillFigure(BaseFig);
            formattedfigure.GetFormattedFigure(new Point(), new_brush);
            collection1.Add(formattedfigure);
            formattedfigure.Show(canvas1);
            return;
        }
        public void fill(Canvas canvas, Fig Fig, CollectionFig collection, Brush my)
        {
            canvas1 = canvas;
            collection1 = collection;
            BaseFig = Fig;
            prev = Fig.FillBrush;
            new_brush= my;
        }
        public override void undo()
        {
            BaseFig.Rm(canvas1);
            collection1.Remove(BaseFig);
            Fig formattedfigure = new FillFigure(BaseFig);
            if (prev is SolidColorBrush solidBrush)
            {
                if(solidBrush.Color.A == 0)
                { 
                    prev = Brushes.Transparent; 
                }
            }
            formattedfigure.GetFormattedFigure(new Point(), prev);
            formattedfigure.Show(canvas1);
            collection1.Add(formattedfigure);
            return;
        }
    }
}
