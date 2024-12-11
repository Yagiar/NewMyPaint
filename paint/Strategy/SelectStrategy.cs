using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;
using paint.command;
using paint.Decorator;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace paint.Strategy
{
    internal class SelectStrategy : IStrategy
    {
        private readonly CollectionFig _collection;
        private Point _start;

        public SelectStrategy(CollectionFig collection)
        {
            _collection = collection;
        }

        public void MouseDown(MouseEventArgs e)
        {
            Point clickPoint = e.GetPosition(((MainWindow)Application.Current.MainWindow).canvas);
            ((MainWindow)Application.Current.MainWindow).selectedFigure.Clear();
            ((MainWindow)Application.Current.MainWindow).canvas.Children.Clear();
            foreach (Fig figure in _collection.collection)
            {
                figure.updateOutline();
                figure.ShowOutline(figure.GetFigure());
                if (figure.Touch((int)clickPoint.X, (int)clickPoint.Y))
                {
                    _start = clickPoint;
                    ((MainWindow)Application.Current.MainWindow).selectedFigure.Add(figure);
                    ((MainWindow)Application.Current.MainWindow).canvas.Children.Add(figure.outline);
                    break;
                }
            }
        }


        public void MouseMove(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point currPos = e.GetPosition(((MainWindow)Application.Current.MainWindow).canvas);
                foreach (Fig figure in ((MainWindow)Application.Current.MainWindow).selectedFigure)
                {
                    ((MainWindow)Application.Current.MainWindow).canvas.Children.Clear();
                    if (figure.select==Fig.SelectType.Center)
                    {
                        MoveCommand command = new MoveCommand();
                        command.move(((MainWindow)Application.Current.MainWindow).canvas, figure, _collection, new Point(currPos.X - _start.X, currPos.Y - _start.Y));
                        ((MainWindow)Application.Current.MainWindow).commandManager.ExecuteCommand(command);
                        
                    }
                    else
                    {
                        ScaleCommand command = new ScaleCommand();
                        command.move(((MainWindow)Application.Current.MainWindow).canvas, figure, _collection, new Point(currPos.X - _start.X, currPos.Y - _start.Y));
                        ((MainWindow)Application.Current.MainWindow).commandManager.ExecuteCommand(command);
                    }
                    figure.updateOutline();
                    figure.ShowOutline(figure.GetFigure());
                    ((MainWindow)Application.Current.MainWindow).canvas.Children.Add(figure.outline);
                }
                _collection.Draw(((MainWindow)Application.Current.MainWindow).canvas);
                _start = currPos;
            }
        }
        public void MouseUp(MouseEventArgs e,int T, Brush my)
        {
            Point _end = e.GetPosition(((MainWindow)Application.Current.MainWindow).canvas);
            ((MainWindow)Application.Current.MainWindow).canvas.Children.Clear();
            _collection.Draw(((MainWindow)Application.Current.MainWindow).canvas);
        }
    }
}
