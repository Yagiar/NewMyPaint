using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using paint.Decorator;
using paint.command;
using paint.Adapter;
using Path = System.IO.Path;
using paint.Strategy;

namespace paint
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool flag;
        bool isDragging;
        bool isScaling  ;
        int IND;
        int T = 10;
        Point p1, p2;
        Brush my = Brushes.Black;
        Brush my1;
        Fig fig;
        Factory factory;
        int b;
        private Polyline pen;
        private Shape selectedShape;
        private Rectangle outline;
        internal List<Fig> selectedFigure;
        CollectionFig collection = new CollectionFig();
        CollectionFigures Collaje = new CollectionFigures();
        Fig formattedfigure;
        internal CommandManager commandManager = new CommandManager();
        IStrategy currentSt;

        public MainWindow()
        {
            selectedFigure = new List<Fig>();
            InitializeComponent();
        }

        async void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            p2 = e.GetPosition(canvas);

            label.Content = ("Координаты: ", p1.ToString(), " ", p2.ToString());
            currentSt?.MouseMove(e);
        }

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            p1 = e.GetPosition(canvas);
            currentSt?.MouseDown(e);
            canvas.CaptureMouse();

        }
        private void canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            currentSt?.MouseUp(e,T,my);
            canvas.ReleaseMouseCapture();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            IND = 2;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            T = T + 1;
            TH.Text = Convert.ToString(T);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (T > 0)
            {
                T = T - 1;
                TH.Text = Convert.ToString(T);
            }
        }


        private void ToggleButton_Checked_6(object sender, RoutedEventArgs e)
        {
            my = Brushes.White;
            if (selectedFigure != null)
            {
                foreach (Fig f in selectedFigure)
                {
                    FillCommand fillCommand = new FillCommand();
                    fillCommand.fill(canvas, f, collection, (Brush)my);
                    commandManager.ExecuteCommand(fillCommand);
                }
                
            }
        }

        private void ToggleButton_Checked_7(object sender, RoutedEventArgs e)
        {
            my = Brushes.Black;
            if (selectedFigure != null)
            {
                foreach (Fig f in selectedFigure)
                {
                    FillCommand fillCommand = new FillCommand();
                    fillCommand.fill(canvas, f, collection, (Brush)my);
                    commandManager.ExecuteCommand(fillCommand);
                }
            }
        }

        private void ToggleButton_Checked_8(object sender, RoutedEventArgs e)
        {
            my = Brushes.Green;
            if (selectedFigure != null)
            {
                foreach (Fig f in selectedFigure)
                {
                    FillCommand fillCommand = new FillCommand();
                    fillCommand.fill(canvas, f, collection, (Brush)my);
                    commandManager.ExecuteCommand(fillCommand);
                }
            }
        }

        private void ToggleButton_Checked_9(object sender, RoutedEventArgs e)
        {
            my = Brushes.Yellow;
            if (selectedFigure != null)
            {
                foreach (Fig f in selectedFigure)
                {
                    FillCommand fillCommand = new FillCommand();
                    fillCommand.fill(canvas, f, collection, (Brush)my);
                    commandManager.ExecuteCommand(fillCommand);
                }
            }
        }

        private void ToggleButton_Checked_10(object sender, RoutedEventArgs e)
        {
            my = Brushes.Red;
            if (selectedFigure != null)
            {
                foreach (Fig f in selectedFigure)
                {
                    FillCommand fillCommand = new FillCommand();
                    fillCommand.fill(canvas, f, collection, (Brush)my);
                    commandManager.ExecuteCommand(fillCommand);
                }
            }
        }

        private void ToggleButton_Checked_13(object sender, RoutedEventArgs e)
        {
            my = Brushes.Blue;
            if (selectedFigure != null)
            {
                foreach (Fig f in selectedFigure)
                {
                    FillCommand fillCommand = new FillCommand();
                    fillCommand.fill(canvas, f, collection, (Brush)my);
                    commandManager.ExecuteCommand(fillCommand);
                }
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            currentSt = new CreateStrategy(collection, new FactoryLine(), this);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            currentSt = new CreateStrategy(collection, new FactoryCircle(), this);
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            currentSt = new CreateStrategy(collection, new FactoryRectangle(), this);
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            currentSt = new CreateStrategy(collection, new FactoryTriangle(), this);
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            currentSt = new CreateStrategy(collection, new FactoryHexagon(), this);
        }

        private void TH_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key== Key.Enter)
            {
                T = Convert.ToInt32(TH.Text);
            }
            
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            collection.clear();
        }

        //private void ch_Checked(object sender, RoutedEventArgs e)
        //{
        //    fig.Fill(my);
        //}

        //private void ch_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    fig.Fill(my1);
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveimg = new Microsoft.Win32.SaveFileDialog();
            saveimg.DefaultExt = ".PNG";
            saveimg.Filter = "PNG Image (*.png)|*.png|" +
                    "JPEG Image (*.jpg, *.jpeg)|*.jpg;*.jpeg|" +
                    "Bitmap Image (*.bmp)|*.bmp|" +
                    "GIF Image (*.gif)|*.gif";
            if (saveimg.ShowDialog() == true)
            {
                // Получаем расширение файла
                string extension = Path.GetExtension(saveimg.FileName)?.ToLower();
                // Убираем точку из расширения (например, ".png" → "png")
                string format = extension?.TrimStart('.');
                // Проверяем наличие формата
                if (string.IsNullOrEmpty(format))
                {
                    throw new ArgumentException("Could not determine the file format from the selected file.");
                }

                SaveManager saveManager = new SaveManager(format);
                saveManager.SaveImage(saveimg.FileName, canvas);
            }
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl))
                currentSt = new GroupStrategy(collection, this);
            else
                currentSt = new SelectStrategy(collection, this);
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            collection.Draw(canvas);
        }


        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if(Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.Z))
            {
                commandManager.undoCommand();
            }

            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.Y))
            {
                commandManager.UndoUndoCommand();
            }
            if (selectedFigure!=null)
            {
                //selectedFigure = GetShapeUnderMouse(p1);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            IND = 1;
        }

    }
    


}
