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
        private MainWindow window;
        public SelectStrategy(CollectionFig collection, MainWindow window)
        {
            _collection = collection;
            this.window = window;
        }

        public void MouseDown(MouseEventArgs e)
        {
            Point clickPoint = e.GetPosition(window.canvas);
            window.selectedFigure.Clear();
            window.canvas.Children.Clear();
            foreach (Fig figure in _collection.collection)
            {
                figure.updateOutline();
                figure.ShowOutline(figure.GetFigure());
                if (figure.Touch((int)clickPoint.X, (int)clickPoint.Y))
                {
                    _start = clickPoint;
                    window.selectedFigure.Add(figure);
                    ((MainWindow)Application.Current.MainWindow).canvas.Children.Add(figure.outline);
                    break;
                }
            }
        }


        public void MouseMove(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point currPos = e.GetPosition(window.canvas);
                foreach (Fig figure in window.selectedFigure)
                {   
                    window.canvas.Children.Clear();
                    if (figure.select==Fig.SelectType.Center)
                    {
                        MoveCommand command = new MoveCommand();
                        command.move(window.canvas, figure, _collection, new Point(currPos.X - _start.X, currPos.Y - _start.Y));
                        window.commandManager.ExecuteCommand(command);
                        
                    }
                    else
                    {
                        ScaleCommand command = new ScaleCommand();
                        command.move(window.canvas, figure, _collection, new Point(currPos.X - _start.X, currPos.Y - _start.Y));
                        window.commandManager.ExecuteCommand(command);
                    }
                    figure.updateOutline();
                    figure.ShowOutline(figure.GetFigure());
                    window.canvas.Children.Add(figure.outline);
                }
                _collection.Draw(window.canvas);
                _start = currPos;
            }
        }
        public void MouseUp(MouseEventArgs e,int T, Brush my)
        {
            Point _end = e.GetPosition(window.canvas);
            window.canvas.Children.Clear();
            _collection.Draw(window.canvas);
        }
    }
}
