using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;
using System;

namespace NewMyPaint
{
    internal class GroupShape : Figure
    {
        private List<Figure> figures = new List<Figure>();

        public void Add(Figure figure)
        {
            figures.Add(figure);
            UpdateBounds(); // Обновляем границы при добавлении новой фигуры
        }

        public void Remove(Figure figure)
        {
            figures.Remove(figure);
            UpdateBounds(); // Обновляем границы при удалении фигуры
        }

        public override Shape GetShape()
        {
            // Для группы не возвращаем отдельную форму
            return null;
        }

        public override void Show(Canvas canvas)
        {
            foreach (var figure in figures)
            {
                figure.Show(canvas);
            }
        }

        public void Move(int dx, int dy)
        {
            // Перемещаем каждую фигуру на заданное смещение
            foreach (var figure in figures)
            {
                figure.startPoint.X += dx;
                figure.startPoint.Y += dy;
                figure.endPoint.X += dx;
                figure.endPoint.Y += dy;
            }
            UpdateBounds(); // Обновляем границы после перемещения
        }

        public void Resize(SelectType sel, int dx, int dy)
        {
            //// Центр группы
            //double centerX = startPoint.X + (endPoint.X - startPoint.X) / 2;
            //double centerY = startPoint.Y + (endPoint.Y - startPoint.Y) / 2;

            //// Масштабируем каждую фигуру относительно центра группы
            //foreach (var figure in figures)
            //{
            //    // Определяем текущие координаты центра фигуры
            //    double figureCenterX = figure.startPoint.X + (figure.endPoint.X - figure.startPoint.X) / 2;
            //    double figureCenterY = figure.startPoint.Y + (figure.endPoint.Y - figure.startPoint.Y) / 2;

            //    // Новые размеры фигуры
            //    double newWidth = (figure.endPoint.X - figure.startPoint.X) ;
            //    double newHeight = (figure.endPoint.Y - figure.startPoint.Y);

            //    // Новое положение верхнего левого угла фигуры
            //    figure.startPoint.X = centerX + (figureCenterX - centerX) - newWidth / 2;
            //    figure.startPoint.Y = centerY + (figureCenterY - centerY) -  newHeight / 2;

            //    // Новое положение нижнего правого угла фигуры
            //    figure.endPoint.X = figure.startPoint.X + newWidth;
            //    figure.endPoint.Y = figure.startPoint.Y + newHeight;
            //}
            foreach (var figure in figures)
            {
                if (sel == SelectType.TopLeft)
                {
                    figure.startPoint.X += dx;
                    figure.startPoint.Y += dy;
                }
                else if (sel == SelectType.TopRight)
                {
                    figure.endPoint.X += dx;
                    figure.startPoint.Y += dy;
                }
                else if (sel == SelectType.BottomLeft)
                {
                    figure.startPoint.X += dx;
                    figure.endPoint.Y += dy;
                }
                else if (sel == SelectType.BottomRight)
                {
                    figure.endPoint.X += dx;
                    figure.endPoint.Y += dy;
                }
            }
            UpdateBounds(); // Обновляем границы после изменения размеров
        }

        public List<Figure> GetFigures()
        {
            return figures;
        }
        public List<Figure> Ungroup()
        {
            return new List<Figure>(figures);
        }
        // Метод для обновления границ группы
        private void UpdateBounds()
        {
            if (figures.Count == 0)
            {
                startPoint = new Point(0, 0);
                endPoint = new Point(0, 0);
                return;
            }

            // Находим минимальные и максимальные координаты всех фигур
            double minX = figures.Min(f => Math.Min(f.startPoint.X, f.endPoint.X));
            double minY = figures.Min(f => Math.Min(f.startPoint.Y, f.endPoint.Y));
            double maxX = figures.Max(f => Math.Max(f.startPoint.X, f.endPoint.X));
            double maxY = figures.Max(f => Math.Max(f.startPoint.Y, f.endPoint.Y));

            // Устанавливаем startPoint и endPoint группы
            startPoint = new Point(minX, minY);
            endPoint = new Point(maxX, maxY);
        }
    }
}
