using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.IO;
using Path = System.Windows.Shapes.Path;

namespace paint
{
    internal class CollectionFigures:Fig
    {
        internal List<Fig> figures = new List<Fig>();
        GeometryGroup geometryGroup = new GeometryGroup();
        public void Add(Fig fig)
        {
            if (!figures.Contains(fig))
            {
                figures.Add(fig);
                geometryGroup.Children.Add(fig.GetFigure().RenderedGeometry);
            }
        }
        public void clear()
        {
            figures.Clear();
            geometryGroup.Children.Clear();
        }
        public void Remove(Fig fig)
        {
            if (figures.Contains(fig))
            { 
                figures.Remove(fig);
                geometryGroup.Children.Remove(fig.GetFigure().RenderedGeometry);
            }
        }
        public override void ShowOutline(Shape shape)
        {
            if (outline == null)
            {
                outline = new Rectangle();
                int Width = 0;
                int Height = 0;
                int Top = Int32.MaxValue;
                int Left = Int32.MaxValue;
                foreach (var fig in figures)
                {
                    double x = Canvas.GetLeft(fig.GetFigure());
                    double y = Canvas.GetTop(fig.GetFigure());

                    // Получение ширины и высоты фигуры
                    double width = 0, height = 0;

                    width = fig.GetFigure().Width;
                    height = fig.GetFigure().Height;

                    // Определение границ
                    Left = (int)Math.Min(Left, x);
                    Top = (int)Math.Min(Top, y);
                    Width = (int)Math.Max(Width, x + width);
                    Height = (int)Math.Max(Height, y + height);
                }
                outline.Width = Width - Left;
                outline.Height = Height - Top;
                Canvas.SetTop(outline, Top);
                Canvas.SetLeft(outline, Left);
                DoubleCollection dashes = new DoubleCollection();
                dashes.Add(2); // длина штриха
                dashes.Add(2); // длина пробела
                outline.StrokeDashArray = dashes;
                outline.Stroke = Brushes.Gray;
                outline.StrokeThickness = 10;
                point1 = new Point(Left, Top);
                point2 = new Point(Left + Width, Top + Height);
            }
        }
        public override Fig GetFormattedFigure(Point mousePosition, Brush brush = null)
        {
            foreach (Fig f in figures)
            {
                if (f != null)
                {
                    return f.GetFormattedFigure(mousePosition);
                }
            }
            return this;
        }

        public override void updateOutline()
        {
            if (outline != null)
            {
                int Width = 0;
                int Height = 0;
                int Top = Int32.MaxValue;
                int Left = Int32.MaxValue;
                foreach (var fig in figures)
                {
                    double x = Canvas.GetLeft(fig.GetFigure());
                    double y = Canvas.GetTop(fig.GetFigure());

                    // Получение ширины и высоты фигуры
                    double width = 0, height = 0;

                    width = fig.GetFigure().Width;
                    height = fig.GetFigure().Height;

                    // Определение границ
                    Left = (int)Math.Min(Left, x);
                    Top = (int)Math.Min(Top, y);
                    Width = (int)Math.Max(Width, x + width);
                    Height = (int)Math.Max(Height, y + height);
                }
                outline.Width = Width - Left;
                outline.Height = Height - Top;
                Canvas.SetTop(outline, Top);
                Canvas.SetLeft(outline, Left);
                point1 = new Point(Left, Top);
                point2 = new Point(Left + Width, Top + Height);
            }
        }
        public override Shape GetFigure()
        {
            
            Path p = new Path
            {
                Data = geometryGroup,
                StrokeThickness = 10,
                Stroke = Brushes.Black,
                Width = point2.X - point1.X,
                Height = point2.Y - point1.Y,
            };
            Canvas.SetLeft(p,point1.X);
            Canvas.SetTop(p,point1.Y);
            //Console.WriteLine(point2.X - point1.X);
            //Console.WriteLine(point2.Y - point1.Y);
            return p;
        }
        public override void Show(Canvas panel)
        {
            foreach (Fig f in figures)
            {
                f.Show(panel);
            }
        }
        public override void Rm(Canvas panel)
        {
            foreach (Fig f in figures) 
            {
                f.Rm(panel); 
            }
        }
    }
}
