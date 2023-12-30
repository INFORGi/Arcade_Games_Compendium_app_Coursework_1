using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Frogerr.xaml
    /// </summary>
    public partial class Frogerr : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        Random ran = new Random();

        Rectangle frog;
        Rectangle car;

        int frogSize = 50;
        int countCar = 8;
        public Frogerr()
        {
            InitializeComponent();
            StartGame();
            gameTimer.Tick += CarsMove;
        }

        private void StartGame()
        {
            DrawFrog();
            DrawCars();
            gameTimer.Interval = TimeSpan.FromMilliseconds(90);
            gameTimer.Start();
        }

        private void DrawFrog()
        {
            frog = new Rectangle()
            {
                Width = frogSize,
                Height = frogSize,
                Fill = Brushes.Green
            };

            Canvas.SetLeft(frog,  1200/2);
            Canvas.SetTop(frog, 700-frogSize*2);
            Area.Children.Add(frog);
        }

        //private void DrawCars()
        //{
        //    for (int i = 0; i < countCar; i++)
        //    {
        //        car = new Rectangle()
        //        {
        //            Tag = "car",
        //            Width = frogSize * 2,
        //            Height = frogSize,
        //            Fill = Brushes.Red
        //        };

        //        Canvas.SetTop(car, 10 + i * (frogSize + 20));
        //        Canvas.SetLeft(car, -frogSize * 2);
        //        Area.Children.Add(car);
        //    }
        //}
        private void DrawCars()
        {
            for (int i = 0; i < countCar; i++)
            {
                car = new Rectangle()
                {
                    Tag = "car",
                    Width = frogSize * 2,
                    Height = frogSize,
                    Fill = Brushes.Red
                };
                // Вычисляем случайную позицию по вертикали
                int posY = ran.Next(0, (int)Area.Height - frogSize-frogSize);
                // Устанавливаем позицию прямоугольника
                Canvas.SetTop(car, posY);
                Canvas.SetLeft(car, ran.Next(-300, -60));
                Area.Children.Add(car);
            }
        }

        private void Key_Down(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.A) 
            {
                Canvas.SetLeft(frog, Canvas.GetLeft(frog) - frogSize);
            }
            if(e.Key == Key.W) 
            { 
                Canvas.SetTop(frog,Canvas.GetTop(frog) - frogSize);
            }
            if(e.Key == Key.D) 
            {
                Canvas.SetLeft(frog, Canvas.GetLeft(frog) + frogSize);
            }
            if(e.Key == Key.S) 
            {
                Canvas.SetTop(frog, Canvas.GetTop(frog) + frogSize);
            }
        }

        private void CarsMove(object sender, EventArgs e)
        {
            foreach(var x in Area.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "car")
                {
                    if(Canvas.GetLeft(x) <= Area.ActualWidth)
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) + frogSize);
                    }
                    if(Canvas.GetLeft(x) > Area.ActualWidth)
                    {
                        Canvas.SetLeft(car, ran.Next(-300, -60));
                        Canvas.SetTop(x, Canvas.GetTop(x) + frogSize);
                    }
                }
            }
        }
    }
}
