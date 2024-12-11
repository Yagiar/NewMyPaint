using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace paint.Decorator
{
    internal class ScaleFigure : Fig
    {
        Fig _editFigure;
        public ScaleFigure(Fig editFigure = null) : base(editFigure)
        {
            _editFigure = editFigure;
        }
        public override Fig GetFormattedFigure(Point mousePosition, Brush brush = null)
        {
            if (_editFigure is CollectionFigures figures)
            {
                foreach (var figure in figures.figures)
                {
                    figure.select = _editFigure.select;
                    Drag((int)mousePosition.X, (int)mousePosition.Y, figure);
                }
            }
            else
            {
                Drag((int)mousePosition.X, (int)mousePosition.Y, this);
            }
           
            return this;
        }


        private void updatePoint(Fig figs)
        {
            if (figs.GetFigure() is Polygon polygon)
            {
                // Получаем текущие размеры полигона
                double originalWidth = polygon.ActualWidth;
                double originalHeight = polygon.ActualHeight;

                // Вычисляем коэффициенты масштабирования по X и Y
                double scaleX = figs.GetFigure().Width / originalWidth;
                double scaleY = figs.GetFigure().Height / originalHeight;

                // Создаем новую коллекцию точек с пересчитанными координатами
                PointCollection newPoints = new PointCollection();

                foreach (Point point in polygon.Points)
                {
                    // Масштабируем каждую точку
                    double newX = point.X * scaleX;
                    double newY = point.Y * scaleY;

                    // Добавляем новую точку в коллекцию
                    newPoints.Add(new Point(newX, newY));
                }

                // Заменяем старые точки полигона на новые
                polygon.Points = newPoints;

            }
        }
        // Изменение положения или размера в зависимости от точки касания
        public void Drag(int dx, int dy, Fig currentFigure)
        {

            double minWidth = 10; // Минимальная ширина
            double minHeight = 10; // Минимальная высота

            switch (currentFigure.select)
            {
                case SelectType.TopLeft:
                    // Изменение ширины и высоты, сохраняя нижний правый угол на месте
                    currentFigure.GetFigure().Width = Math.Max(minWidth, currentFigure.GetFigure().Width - dx);
                    currentFigure.GetFigure().Height = Math.Max(minHeight, currentFigure.GetFigure().Height - dy);
                    // Перемещение фигуры для компенсации изменения размера
                    Canvas.SetLeft(currentFigure.GetFigure(), Canvas.GetLeft(currentFigure.GetFigure()) + dx);
                    Canvas.SetTop(currentFigure.GetFigure(), Canvas.GetTop(currentFigure.GetFigure()) + dy);
                    break;

                case SelectType.TopRight:
                    currentFigure.GetFigure().Width = Math.Max(minWidth, currentFigure.GetFigure().Width + dx);
                    currentFigure.GetFigure().Height = Math.Max(minHeight, currentFigure.GetFigure().Height - dy);
                    Canvas.SetTop(currentFigure.GetFigure(), Canvas.GetTop(currentFigure.GetFigure()) + dy);
                    break;

                case SelectType.BottomLeft:
                    currentFigure.GetFigure().Width = Math.Max(minWidth, currentFigure.GetFigure().Width - dx);
                    currentFigure.GetFigure().Height = Math.Max(minHeight, currentFigure.GetFigure().Height + dy);
                    Canvas.SetLeft(currentFigure.GetFigure(), Canvas.GetLeft(currentFigure.GetFigure()) + dx);
                    break;

                case SelectType.BottomRight:
                    currentFigure.GetFigure().Width = Math.Max(minWidth, currentFigure.GetFigure().Width + dx);
                    currentFigure.GetFigure().Height = Math.Max(minHeight, currentFigure.GetFigure().Height + dy);
                    break;

                    // Добавьте другие варианты SelectType, если необходимо
            }
            
            point1 = new Point(Canvas.GetLeft(currentFigure.GetFigure()), Canvas.GetTop(currentFigure.GetFigure()));
            point2 = new Point(Canvas.GetLeft(currentFigure.GetFigure()) + GetFigure().Width, Canvas.GetTop(currentFigure.GetFigure()) + GetFigure().Height);
            _editFigure.updateOutline();
            updatePoint(currentFigure);

            //if (select == SelectType.TopLeft)
            //{
            //    currentFigure.point1.X += dx;
            //    currentFigure.point1.Y += dy;
            //}
            //else if (select == SelectType.TopRight)
            //{
            //    currentFigure.point2.X += dx;
            //    currentFigure.point1.Y += dy;
            //}
            //else if (select == SelectType.BottomLeft)
            //{
            //    currentFigure.point1.X += dx;
            //    currentFigure.point2.Y += dy;
            //}
            //else if (select == SelectType.BottomRight)
            //{
            //    currentFigure.point2.X += dx;
            //    currentFigure.point2.Y += dy;
            //}
            //currentFigure.point1 = new Point(currentFigure.point1.X, currentFigure.point1.Y);
            //currentFigure.point2 = new Point(currentFigure.point2.X, currentFigure.point2.Y);


            //if (currentFigure.select == SelectType.TopLeft)
            //{
            //   Canvas.SetLeft(currentFigure.GetFigure(), Canvas.GetLeft(currentFigure.GetFigure()) - dx);
            //   Canvas.SetTop(currentFigure.GetFigure(), Canvas.GetTop(currentFigure.GetFigure()) - dx);
            //}
            //else if (currentFigure.select == SelectType.TopRight)
            //{
            //    currentFigure.GetFigure().Width += dx;
            //    Canvas.SetTop(currentFigure.GetFigure(), Canvas.GetTop(currentFigure.GetFigure()) - dx);
            //}
            //else if (currentFigure.select == SelectType.BottomLeft)
            //{
            //    Canvas.SetLeft(currentFigure.GetFigure(), Canvas.GetLeft(currentFigure.GetFigure()) - dx);
            //    currentFigure.GetFigure().Height += dy;
            //}
            //else if (currentFigure.select == SelectType.BottomRight)
            //{
            //    currentFigure.GetFigure().Width += dx;
            //    currentFigure.GetFigure().Height += dy;
            //}


            //if (currentFigure.select == SelectType.TopLeft)
            //{
            //    currentFigure.GetFigure().Width -= dx;
            //    currentFigure.GetFigure().Height -= dy;
            //}
            //else if (currentFigure.select == SelectType.TopRight)
            //{
            //    currentFigure.GetFigure().Width += dx;
            //    currentFigure.GetFigure().Height -= dy;
            //}
            //else if (currentFigure.select == SelectType.BottomLeft)
            //{
            //    currentFigure.GetFigure().Width -= dx;
            //    currentFigure.GetFigure().Height += dy;
            //}
            //else if (currentFigure.select == SelectType.BottomRight)
            //{
            //    currentFigure.GetFigure().Width += dx;
            //    currentFigure.GetFigure().Height += dy;
            //}
        }
        }
}

