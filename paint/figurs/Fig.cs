using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using paint.Decorator;
using System.Data.SqlTypes;

namespace paint
{
    public abstract class Fig
    {
        public Point point1 = new Point();
        public Point point2 = new Point();
        public int th;
        Brush my;
        private readonly Fig _fig;
        internal SelectType select = SelectType.None;
        bool showOutline = false;
        public Rectangle outline;
        public Brush FillBrush;

        public virtual void ShowOutline(Shape shape)
        {
            if (outline == null)
            {
                outline = new Rectangle();
                outline.Width = shape.Width;
                outline.Height = shape.Height;
                Canvas.SetTop(outline, Canvas.GetTop(shape));
                Canvas.SetLeft(outline, Canvas.GetLeft(shape));
                DoubleCollection dashes = new DoubleCollection();
                dashes.Add(2); // длина штриха
                dashes.Add(2); // длина пробела
                outline.StrokeDashArray = dashes;
                outline.Stroke = Brushes.Gray;
                outline.StrokeThickness = 10;
            }
        }
        public enum SelectType
        {
            None,
            Center,
            TopLeft,
            BottomLeft,
            TopRight,
            BottomRight
        }
        public Fig(Fig fig = null)
        {
            if (fig != null)
            {
                _fig = fig;
                point1 = fig.point1;
                point2 = fig.point2;
                th = fig.th;
                my = fig.my;
                select = fig.select;
                outline = fig.outline;
                FillBrush = fig.FillBrush;
            }
            else
                _fig = this;
        }
        public virtual Fig GetFormattedFigure( Point mousePosition, Brush brush = null)
        {
            if (_fig != null)
            {
                return _fig.GetFormattedFigure(mousePosition);
            }
            return this;
        }

        public virtual void updateOutline()
        {
            if (outline != null)
            {
                outline.Width = GetFigure().Width;
                outline.Height = GetFigure().Height;
                Canvas.SetLeft(outline, Canvas.GetLeft(GetFigure()));
                Canvas.SetTop(outline, Canvas.GetTop(GetFigure()));
            }
        }
        public bool Touch(int tx, int ty)
        {
            const int tolerance = 20;
            double left = Canvas.GetLeft(GetFigure());
            double right = left + GetFigure().Width;
            double top = Canvas.GetTop(GetFigure());
            double bottom = top + GetFigure().Height;

            // Центр прямоугольника
            double centerX = left + (right - left) / 2;
            double centerY = top + (bottom - top) / 2;

            

            // Проверяем попадание в верхний левый угол
            if (Math.Abs(tx - left) < tolerance && Math.Abs(ty - top) < tolerance)
            {
                select = SelectType.TopLeft;
                return true;
            }

            // Проверяем попадание в верхний правый угол
            if (Math.Abs(tx - right) < tolerance && Math.Abs(ty - top) < tolerance)
            {
                select = SelectType.TopRight;
                return true;
            }

            // Проверяем попадание в нижний левый угол
            if (Math.Abs(tx - left) < tolerance && Math.Abs(ty - bottom) < tolerance)
            {
                select = SelectType.BottomLeft;
                return true;
            }

            // Проверяем попадание в нижний правый угол
            if (Math.Abs(tx - right) < tolerance && Math.Abs(ty - bottom) < tolerance)
            {
                select = SelectType.BottomRight;
                return true;
            }

            //Проверяем попадание в центр
            if (tx>=Math.Min(point1.X, point2.X) && tx <= Math.Max(point1.X, point2.X) && ty >= Math.Min(point1.Y, point2.Y) && ty <= Math.Max(point1.Y, point2.Y))
            {
                select = SelectType.Center;
                return true;
            }
            // Если ни один из вариантов не подходит
            select = SelectType.None;
            return false;
        }

        public virtual void Fill(Brush my)
        {
            _fig.Fill(my);
            FillBrush = my;
        }
        public Brush MyBrush
        {
            get { return my; }
            set { my = value; }
        }
        public virtual Shape GetFigure()
        {
            return _fig.GetFigure();
        }
        public virtual void Show(Canvas panel)
        {
            _fig.Show(panel);
        }
        public void draw(Canvas panel)
        {
            Show(panel);
            Rm(panel);
        }
        public virtual void Rm(Canvas panel)
        {
            _fig.Rm(panel);
        }
    }
}
