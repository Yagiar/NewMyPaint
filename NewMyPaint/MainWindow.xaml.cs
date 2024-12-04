using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NewMyPaint
{
    public partial class MainWindow : Window
    {
        Factory factory;
        Figure figure;
        Point start, end;
        FiguresCollection collection = new FiguresCollection();
        private List<Figure> selectedFigures = new List<Figure>();
        private Decorator decorator;
        private CommandManager cmdManager = new CommandManager();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void can_MouseMove(object sender, MouseEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) 
            { }
            else
            {
                Point currPos = e.GetPosition(can);
                coords.Content = currPos.ToString();
                if (decorator != null && e.LeftButton == MouseButtonState.Pressed)
                {
                    decorator.Drag((int)(currPos.X - start.X), (int)(currPos.Y - start.Y), can);
                    can.Children.Clear();
                    collection.Draw(can);
                    DrawDashedBorder(decorator.f);
                    start = currPos;
                }
            }            
            //else if (factory != null && e.LeftButton == MouseButtonState.Pressed)
            //{
            //    can.Children.Clear();
            //    collection.Draw(can);
            //    figure.startPoint.X = start.X;
            //    figure.startPoint.Y = start.Y;
            //    figure.endPoint.X = currPos.X;
            //    figure.endPoint.Y = currPos.Y;
            //    figure.Show(can);
            //}
        }
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point clickPoint = e.GetPosition(can);
            start = clickPoint;
            if (factory != null)
            {
                figure = factory.GetShape();
                start = e.GetPosition(can);
                return;
            }
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                decorator = new Decorator();
                foreach (Figure fig in collection.GetFigures())
                {
                    decorator.Assign(fig);
                    if (decorator.Touch((int)clickPoint.X, (int)clickPoint.Y))
                    {
                        selectedFigures.Add(fig);
                        DrawDashedBorder(decorator.f);
                        return;
                    }
                }
            }
            else
            {
                selectedFigures.Clear();
                decorator = new Decorator();
                foreach (Figure fig in collection.GetFigures())
                {
                    decorator.Assign(fig);
                    if (decorator.Touch((int)clickPoint.X, (int)clickPoint.Y))
                    {
                        start = clickPoint;
                        DrawDashedBorder(decorator.f);
                        selectedFigures.Add(fig);
                        return;
                    }
                }
            }
        }
        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                return;
            }
            else if (decorator != null)
            {
                // Логика перемещения фигур
                end = e.GetPosition(can);
                decorator.Drag((int)(end.X - start.X), (int)(end.Y - start.Y), can);
                can.Children.Clear();
                collection.Draw(can);
                decorator = null;
            }
            else if (figure != null)
            {
                // Завершение создания фигуры
                end = e.GetPosition(can);
                // Создаем команду для добавления фигуры и выполняем её через CommandManager
                var createCommand = new CreateCommand(collection, factory, start, end);
                cmdManager.ExecuteCommand(createCommand);
                // Обновление канвы для отображения новой фигуры
                can.Children.Clear();
                collection.Draw(can);
            }
        }

        private void RectangleButton_Click(object sender, RoutedEventArgs e)
        {
            factory = new RectangleFactory();
        }
        private void TriangleButton_Click(object sender, RoutedEventArgs e)
        {
            factory = new TriangleFactory();
        }
        private void CircleButton_Click(object sender, RoutedEventArgs e)
        {
            factory = new CircleFactory();
        }
        private async void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            can.Children.Clear();
            await Task.Delay(500);
            collection.Draw(can);
        }
        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            figure = null;
            factory = null;
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            can.Children.Clear();
            collection.Clear();
        }

        private void GroupButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedFigures.Count > 1)
            {
                var groupCommand = new GroupCommand(collection, selectedFigures);
                cmdManager.ExecuteCommand(groupCommand);
                selectedFigures.Clear();
                can.Children.Clear();
                collection.Draw(can);
                decorator = null;
            }
        }
        private void UnGroupButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedFigures.Count == 1 && selectedFigures[0] is GroupShape)
            {
                Figure group = selectedFigures[0];
                var ungroupCommand = new UngroupCommand(collection, group);
                cmdManager.ExecuteCommand(ungroupCommand);
                selectedFigures.Clear();
                can.Children.Clear();
                collection.Draw(can);
                decorator = null;
            }
        }       

        private void DrawDashedBorder(Figure figure)
        {
            // Создаем линию для каждой стороны фигуры
            Line leftLine = new Line
            {
                X1 = figure.startPoint.X,
                Y1 = figure.startPoint.Y,
                X2 = figure.startPoint.X,
                Y2 = figure.endPoint.Y,
                Stroke = Brushes.Gray,
                StrokeThickness = 5,
                StrokeDashArray = new DoubleCollection { 2, 2 } // Разрывная линия
            };

            Line rightLine = new Line
            {
                X1 = figure.endPoint.X,
                Y1 = figure.startPoint.Y,
                X2 = figure.endPoint.X,
                Y2 = figure.endPoint.Y,
                Stroke = Brushes.Gray,
                StrokeThickness = 5,
                StrokeDashArray = new DoubleCollection { 2, 2 } // Разрывная линия
            };

            Line topLine = new Line
            {
                X1 = figure.startPoint.X,
                Y1 = figure.startPoint.Y,
                X2 = figure.endPoint.X,
                Y2 = figure.startPoint.Y,
                Stroke = Brushes.Gray,
                StrokeThickness = 5,
                StrokeDashArray = new DoubleCollection { 2, 2 } // Разрывная линия
            };

            Line bottomLine = new Line
            {
                X1 = figure.startPoint.X,
                Y1 = figure.endPoint.Y,
                X2 = figure.endPoint.X,
                Y2 = figure.endPoint.Y,
                Stroke = Brushes.Gray,
                StrokeThickness = 5,
                StrokeDashArray = new DoubleCollection { 2, 2 } // Разрывная линия
            };

            // Добавляем линии на канвас
            can.Children.Add(leftLine);
            can.Children.Add(rightLine);
            can.Children.Add(topLine);
            can.Children.Add(bottomLine);
        }
        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            UndoLastAction();
        }
        private void UndoLastAction()
        {
            cmdManager.UndoCommand();
            can.Children.Clear();
            collection.Draw(can);
        }
    }
}