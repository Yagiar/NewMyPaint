using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;

namespace paint.Strategy
{
    internal class GroupStrategy : IStrategy
    {
        private readonly CollectionFig _collection;
        private Point _start;
        private MainWindow window;
        public GroupStrategy(CollectionFig collection, MainWindow window)
        {
            _collection = collection;
            this.window = window;
        }

        public void MouseDown(MouseEventArgs e)
        {
            Point clickPoint = e.GetPosition(window.canvas);
            foreach (Fig figure in _collection.collection)
            {
                figure.updateOutline();
                figure.ShowOutline(figure.GetFigure());
                if (figure.Touch((int)clickPoint.X, (int)clickPoint.Y))
                {
                    window.selectedFigure.Add(figure);
                    if(!window.canvas.Children.Equals(figure.outline))
                        window.canvas.Children.Add(figure.outline);
                    return;
                }
            }
            _collection.Draw(window.canvas);
        }
        public void MouseMove(MouseEventArgs e) { }
        public void MouseUp(MouseEventArgs e, int T, Brush my) { }
    }
}
