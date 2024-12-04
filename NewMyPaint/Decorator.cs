using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace NewMyPaint
{
    internal class Decorator : Figure
    {
        public Figure f { get; private set; }  // Фигура, которую оборачиваем
        private SelectType select = SelectType.None;

        public void Assign(Figure figure)
        {
            f = figure;  // Привязка декоратора к конкретной фигуре
        }        

        public override Shape GetShape()
        {
            return f.GetShape();
        }

        public override void Show(Canvas canvas)
        {
            f.Show(canvas);
        }

        // Изменение положения или размера в зависимости от точки касания
        public void Drag(int dx, int dy, Canvas canvas)
        {
            if (f is GroupShape group)
            {
                // Если это группа, перемещаем или изменяем размер всех фигур в группе
                if (select == SelectType.Center)
                {
                    group.Move(dx, dy);
                }
                else
                {
                    group.Resize(select, dx, dy);
                }
            }
            else
            {
                // Для одиночной фигуры перемещение и изменение размера
                if (select == SelectType.Center)
                {
                    f.startPoint.X += dx;
                    f.startPoint.Y += dy;
                    f.endPoint.X += dx;
                    f.endPoint.Y += dy;
                }
                else
                {
                    // Растягиваем фигуру по углам
                    if (select == SelectType.TopLeft)
                    {
                        f.startPoint.X += dx;
                        f.startPoint.Y += dy;
                    }
                    else if (select == SelectType.TopRight)
                    {
                        f.endPoint.X += dx;
                        f.startPoint.Y += dy;
                    }
                    else if (select == SelectType.BottomLeft)
                    {
                        f.startPoint.X += dx;
                        f.endPoint.Y += dy;
                    }
                    else if (select == SelectType.BottomRight)
                    {
                        f.endPoint.X += dx;
                        f.endPoint.Y += dy;
                    }
                }
            }
            Update(canvas);  // Обновляем фигуру после изменений
        }

        // Проверка, куда именно нажали: в центр или в угол
        public bool Touch(int tx, int ty)
        {
            const int tolerance = 40;
            double centerX = f.startPoint.X + (f.endPoint.X - f.startPoint.X) / 2;
            double centerY = f.startPoint.Y + (f.endPoint.Y - f.startPoint.Y) / 2;

            // Проверяем попадание в центр (для перемещения)
            if (Math.Abs(tx - centerX) < tolerance && Math.Abs(ty - centerY) < tolerance)
            {
                select = SelectType.Center;
                return true;
            }
            // Проверяем попадание в верхний левый угол (для изменения размера)
            if (Math.Abs(tx - f.startPoint.X) < tolerance && Math.Abs(ty - f.startPoint.Y) < tolerance)
            {
                select = SelectType.TopLeft;
                return true;
            }
            // Проверяем попадание в верхний правый угол
            if (Math.Abs(tx - f.endPoint.X) < tolerance && Math.Abs(ty - f.startPoint.Y) < tolerance)
            {
                select = SelectType.TopRight;
                return true;
            }
            // Проверяем попадание в нижний левый угол
            if (Math.Abs(tx - f.startPoint.X) < tolerance && Math.Abs(ty - f.endPoint.Y) < tolerance)
            {
                select = SelectType.BottomLeft;
                return true;
            }
            // Проверяем попадание в нижний правый угол
            if (Math.Abs(tx - f.endPoint.X) < tolerance && Math.Abs(ty - f.endPoint.Y) < tolerance)
            {
                select = SelectType.BottomRight;
                return true;
            }
            // Если ни один из вариантов не подходит
            select = SelectType.None;
            return false;
        }
        // Обновляем координаты фигуры после изменений
        public void Update(Canvas canvas)
        {
            // Здесь можно обновить фигуру, если декоратор ее изменил
            f.startPoint = new Point(f.startPoint.X, f.startPoint.Y);
            f.endPoint = new Point(f.endPoint.X, f.endPoint.Y);
            f.Show(canvas);
        }
    }

    // Перечисление для отслеживания, куда попали: в центр или в угол
    public enum SelectType
    {
        None,
        Center,
        TopLeft,
        BottomLeft,
        TopRight,
        BottomRight
    }
}
