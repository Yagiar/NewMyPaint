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

        public GroupStrategy(CollectionFig collection)
        {
            _collection = collection;
        }

        public void MouseDown(MouseEventArgs e)
        {
            Point clickPoint = e.GetPosition(((MainWindow)Application.Current.MainWindow).canvas);
            foreach (Fig figure in _collection.collection)
            {
                figure.updateOutline();
                figure.ShowOutline(figure.GetFigure());
                if (figure.Touch((int)clickPoint.X, (int)clickPoint.Y))
                {
                    ((MainWindow)Application.Current.MainWindow).selectedFigure.Add(figure);
                    if(!((MainWindow)Application.Current.MainWindow).canvas.Children.Equals(figure.outline))
                        ((MainWindow)Application.Current.MainWindow).canvas.Children.Add(figure.outline);
                    return;
                }
            }
            _collection.Draw(((MainWindow)Application.Current.MainWindow).canvas);
        }
        public void MouseMove(MouseEventArgs e) { }
        public void MouseUp(MouseEventArgs e, int T, Brush my) { }
    }
}
