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
        private MainWindow window;
        public CreateStrategy(CollectionFig collection, Factory factory, MainWindow window)
        {
            _collection = collection;
            _factory = factory;
            this.window = window;
        }

        public void MouseDown(MouseEventArgs e)
        {
            // Начало создания фигуры
            _start = e.GetPosition(window.canvas);
            _currentFigure = _factory.GetShape();
            _currentFigure.point1 = _start;
        }

        public void MouseMove(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Обновляем конечную точку фигуры
                var end = e.GetPosition(window.canvas);
                _currentFigure.point2 = end;

                window.canvas.Children.Clear();
                _collection.Draw(window.canvas);
                _currentFigure.Show(window.canvas);
            }
        }

        public void MouseUp(MouseEventArgs e,int T, Brush my)
        {
            // Завершаем создание фигуры
            var end = e.GetPosition(window.canvas);
            _currentFigure.point2 = end;

            // Обновляем канвас
            var createCommand = new CreateCommand();
            createCommand.create(window.canvas, _collection, _factory, _start, end, T, my);
            window.commandManager.ExecuteCommand(createCommand);
            window.canvas.Children.Clear();
            _collection.Draw(window.canvas);

            // Сбрасываем временную фигуру
            _currentFigure = null;
        }
    }
}
