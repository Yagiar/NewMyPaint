using paint.command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows;
using paint.Strategy;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace paint
{
    internal class CreateStrategy : IStrategy
    {
        internal CollectionFig _collection;
        internal Factory _factory;
        internal Point _start;
        internal Fig _currentFigure;

        public CreateStrategy(CollectionFig collection, Factory factory)
        {
            _collection = collection;
            _factory = factory;
        }

        public void MouseDown(MouseEventArgs e)
        {
            // Начало создания фигуры
            _start = e.GetPosition(((MainWindow)Application.Current.MainWindow).canvas);
            _currentFigure = _factory.GetShape();
            _currentFigure.point1 = _start;
        }

        public void MouseMove(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Обновляем конечную точку фигуры
                var end = e.GetPosition(((MainWindow)Application.Current.MainWindow).canvas);
                _currentFigure.point2 = end;

                // Обновляем отображение на канвасе
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.canvas.Children.Clear();
                _collection.Draw(mainWindow.canvas);
                _currentFigure.Show(mainWindow.canvas);
            }
        }

        public void MouseUp(MouseEventArgs e,int T, Brush my)
        {
            // Завершаем создание фигуры
            var end = e.GetPosition(((MainWindow)Application.Current.MainWindow).canvas);
            _currentFigure.point2 = end;

            // Обновляем канвас
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var createCommand = new CreateCommand();
            createCommand.create(mainWindow.canvas, _collection, _factory, _start, end, T, my);
            ((MainWindow)Application.Current.MainWindow).commandManager.ExecuteCommand(createCommand);
            mainWindow.canvas.Children.Clear();
            _collection.Draw(mainWindow.canvas);

            // Сбрасываем временную фигуру
            _currentFigure = null;
        }
    }
}
