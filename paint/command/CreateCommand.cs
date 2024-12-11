using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace paint.command
{
    internal class CreateCommand : Command
    {
        Fig Fig { get; set; }
        Canvas canvas1;
        CollectionFig collection1;
        public override void Execute()
        {
            Fig.Show(canvas1);
            collection1.Add(Fig);
            return;
        }
        public void create(Canvas canvas, CollectionFig collection, Factory factory, Point p1, Point p2, int T, Brush my)
        {
            Fig = factory.GetShape();
            Fig.point1.X = p1.X;
            Fig.point1.Y = p1.Y;
            Fig.point2.X = p2.X;
            Fig.point2.Y = p2.Y;
            Fig.MyBrush = my;
            Fig.th = T;
            canvas1 = canvas;
            collection1 = collection;
        }
        public override void undo()
        {
            Fig.Rm(canvas1);
            collection1.Remove(Fig);
            return ;
        }
    }
}
