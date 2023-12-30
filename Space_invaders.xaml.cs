using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using static WpfApp1.Setting;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Space_invaders.xaml
    /// </summary>
    public partial class Space_invaders : Window
    {
        Random ran = new Random();

        bool goLeft = false;
        bool goRight = false;

        List<Rectangle> itemstoremove = new List<Rectangle>();

        Rectangle player1;

        int bulletTimer;
        int bulletTimerLimit;

        int totalEnemeis;
        int nu;


        int provercaTotalEnemeis;

        int currentScore;
        int slognost;
        int limit;
        string line;
        int rest = 6;

        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public int enemySpeed = 6;
        public Space_invaders()
        {
            InitializeComponent();

            StartGAme();
            dispatcherTimer.Tick += gameEngine;

            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri("1643037785_11-abrakadabra-fun-p-chernoe-nebo-so-zvezdami-fon-16.jpg", UriKind.Relative));

            Area.Background = myBrush;
            this.Closing += Window_Closed;
        }

        /// <summary>
        /// Метод генерирует корабль игрока.
        /// </summary>
        private void DrawKorabl()
        {
            ImageBrush myBrush2 = new ImageBrush();
            myBrush2.ImageSource = new BitmapImage(new Uri("pngwing.com.png", UriKind.Relative));
            player1 = new Rectangle()
            {
                Fill = myBrush2,
                Width = 50,
                Height = 50,
                Stroke =Brushes.Snow
            };
            Canvas.SetLeft(player1, Area.Width / 2 - player1.Width);
            Canvas.SetTop(player1, Area.Height - player1.Height - 10);
            Area.Children.Add(player1);
        }

        /// <summary>
        /// Метод обрабатывает нажати на клавиши для предвижения игрока.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_KeyisDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.A)
            {
                goLeft = true;
            }
            if (e.Key == Key.D)
            {
                goRight = true;
            }
            if (e.Key == Key.Space)
            {
                Reyting();
                currentScore = 0;
                Area.Children.Clear();
                StartGAme();
                dispatcherTimer.IsEnabled = true;
            }
            if(e.Key == Key.Escape)
            {
                dispatcherTimer.IsEnabled = false;
            }
            if (e.Key == Key.Enter)
            {
                dispatcherTimer.IsEnabled = true;
            }
            if(e.Key == Key.F1)
            {
                dispatcherTimer.IsEnabled = false;
                Space_InvadersF_ s = new Space_InvadersF_();
                s.Show();
            }
        }
        /// <summary>
        /// Метод обрабатывает нажати на клавиши для предвижения и стрельбы игрока.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_KeyIsUp(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.A)
            {
                goLeft = false;
            }
            if (e.Key == Key.D)
            {
                goRight = false;
            }

            if (e.Key == Key.W)
            {

                itemstoremove.Clear();

                Rectangle newBullet = new Rectangle
                {
                    Tag = "bullet",
                    Height = 20,
                    Width = 5,
                    Fill = Brushes.White,
                    Stroke = Brushes.Red
                };
                Canvas.SetTop(newBullet, Canvas.GetTop(player1) - newBullet.Height);
                Canvas.SetLeft(newBullet, Canvas.GetLeft(player1) + player1.Width / 2);
                Area.Children.Add(newBullet);
            }
        }
        /// <summary>
        /// Метод создает вражеские снаряды.
        /// </summary>
        /// <param name="x">Вектор по оси х.</param>
        /// <param name="y">вектор по оси у.</param>
        private void enemyBulletMaker(double x, double y)
        {
            Rectangle newEnemyBullet = new Rectangle
            {
                Tag = "enemyBullet",
                Height = 40,
                Width = 15,
                Fill = Brushes.Yellow,
                Stroke = Brushes.Black,
                StrokeThickness = 5
            };
            Canvas.SetTop(newEnemyBullet, y);
            Canvas.SetLeft(newEnemyBullet, x);
            Area.Children.Add(newEnemyBullet);
        }
        /// <summary>
        /// Метод создает вражеские коробли.
        /// </summary>
        private void makeEnemies()
        {
            int left = 0;
            nu = limit;
            totalEnemeis = limit;
            provercaTotalEnemeis = limit;
            for (int i = 0; i < limit; i++)
            {
                ImageBrush myBrush3 = new ImageBrush();
                myBrush3.ImageSource = new BitmapImage(new Uri("pngwing.com (1).png", UriKind.Relative));
                Rectangle newEnemy = new Rectangle
                {
                    Tag = "enemy",
                    Height = 45,
                    Width = 45,
                    Fill = myBrush3,
                };
                Canvas.SetTop(newEnemy, 10);
                Canvas.SetLeft(newEnemy, left);
                Area.Children.Add(newEnemy);
                left -= 60;

            }
        }
        /// <summary>
        /// Метод задает стартовые значения для переменных и отвечает за начало игры/создание новой волны.
        /// </summary>
       public void StartGAme()
        {
            StreamReader se = new StreamReader("Сложность.txt");
            line = se.ReadLine();
            se.Close();
            int o = Int32.Parse(line);
            slognost = o;
            ///
            foreach(var x in Area.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "enemy")
                {
                    itemstoremove.Add(x);
                }
            }
            ///

            if (o == 0)
            {
                limit = ran.Next(30, 40);

                goLeft = false;
                goRight = false;
                enemySpeed = rest;
                bulletTimerLimit = 90;
                dispatcherTimer.Interval = TimeSpan.FromMilliseconds(20);
                DrawKorabl();
                makeEnemies();
                dispatcherTimer.IsEnabled = true;
                rest += 1;
            }
            if (o == 1)
            {
                limit = ran.Next(35, 50);
                goLeft = false;
                goRight = false;
                enemySpeed = rest;
                bulletTimerLimit = 90;
                dispatcherTimer.Interval = TimeSpan.FromMilliseconds(20);
                DrawKorabl();
                makeEnemies();
                dispatcherTimer.IsEnabled = true;
                rest += 3;
            }
        }
        /// <summary>
        /// Метод который проверяет снаряды, врагов, игрока. Обновляет очки игрока и т.д.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameEngine(object sender, EventArgs e)
        {
            Rect player = new Rect(Canvas.GetLeft(player1), Canvas.GetTop(player1), player1.Width, player1.Height);
            this.Title = "Счет: " + currentScore;

            if (goLeft && Canvas.GetLeft(player1) > 0)
            {
                Canvas.SetLeft(player1, Canvas.GetLeft(player1) - 10);
            }
            else if (goRight && Canvas.GetLeft(player1) + 80 < Area.Width)
            {
                Canvas.SetLeft(player1, Canvas.GetLeft(player1) + 10);
            }
            bulletTimer -= 3;
            if (bulletTimer < 0)
            {
                enemyBulletMaker((Canvas.GetLeft(player1) + 20), 10);
                bulletTimer = bulletTimerLimit;
            }
            if(slognost == 0)
            {
                if (totalEnemeis < nu / 2)
                {
                    enemySpeed = 20;
                }
            }
            if(slognost == 1)
            {
                if (totalEnemeis < nu / 2)
                {
                    enemySpeed = 60;
                }
            }
            foreach (var x in Area.Children.OfType<Rectangle>())
            {
                if (x is Rectangle && (string)x.Tag == "bullet")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) - 20);
                    Rect bullet = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (Canvas.GetTop(x) < 10)
                    {
                        itemstoremove.Add(x);
                    }
                    foreach (var y in Area.Children.OfType<Rectangle>())
                    {
                        if (y is Rectangle && (string)y.Tag == "enemy")
                        {
                            Rect enemy = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);
                            if (bullet.IntersectsWith(enemy))
                            {
                                itemstoremove.Add(x);
                                itemstoremove.Add(y);
                                totalEnemeis -= 1;
                                currentScore++;
                            }
                        }
                    }
                }
                if (x is Rectangle && (string)x.Tag == "enemy")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) + enemySpeed);
                    if (Canvas.GetLeft(x) > Area.Width)
                    {
                        Canvas.SetLeft(x, -80);
                        Canvas.SetTop(x, Canvas.GetTop(x) + (x.Height + 10));
                    }
                    Rect enemy = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (player.IntersectsWith(enemy))
                    {
                        dispatcherTimer.IsEnabled = false;
                        dispatcherTimer.Interval = TimeSpan.FromMilliseconds(20);
                        Reyting();
                        currentScore = 0;
                        MessageBox.Show("Ты проиграл");
                    }
                }
                if (x is Rectangle && (string)x.Tag == "enemyBullet")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + 10);
                    if (Canvas.GetTop(x) > Area.Height)
                    {
                        itemstoremove.Add(x);
                    }
                    Rect enemyBullets = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (enemyBullets.IntersectsWith(player))
                    {
                        dispatcherTimer.IsEnabled = false;
                        dispatcherTimer.Interval = TimeSpan.FromMilliseconds(20);
                        Reyting();
                        MessageBox.Show("Ты проиграл");
                        currentScore = 0;
                        Area.Children.Clear();
                        break;
                    }
                }
            }
            foreach (Rectangle y in itemstoremove)
            {
                Area.Children.Remove(y);
            }
            //if (totalEnemeis == 0)
            //{
            //    dispatcherTimer.IsEnabled = false;
            //    Area.Children.Clear();
            //    StartGAme();
            //}

            int ds = 0;
            foreach (var x in Area.Children.OfType<Rectangle>())
            {
                if (x is Rectangle && (string)x.Tag == "enemy") 
                {
                    ds++;
                }
            }
            if (ds == 0)
            {
                dispatcherTimer.IsEnabled = false;
                Area.Children.Clear();
                StartGAme();
            }
            ds = 0;
        }

        /// <summary>
        /// Метод обновляет счет игрока в игре Space Invaders.
        /// </summary>
        public void Reyting()
        {
            if (slognost == 0)
            {
                StreamReader sr = new StreamReader("d.txt");
                string name = sr.ReadLine();
                sr.Close();

                List<string> lines = new List<string>(System.IO.File.ReadAllLines("Space_invaders.txt"));

                bool im = false;
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].IndexOf(name) != -1)
                    {
                        im = true;
                        break;
                    }
                }

                if (im)
                {
                    for (int i = 0; i < lines.Count; i++)
                    {
                        string xyr = lines[i];
                        string[] name2 = xyr.Split(' ');
                        if (name2[0] == name)
                        {
                            if (Int32.Parse(name2[2]) < currentScore)
                            {
                                lines[i] = $"{name} - {currentScore}";
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
                else
                {
                    lines.Add($"{name} - {currentScore}");
                }

                System.IO.File.WriteAllLines("Space_invaders.txt", lines.ToArray());
            }
            else
            {
                StreamReader sr = new StreamReader("d.txt");
                string name = sr.ReadLine();
                sr.Close();

                List<string> lines = new List<string>(System.IO.File.ReadAllLines("Space_invaders-сложно.txt"));

                bool im = false;
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].IndexOf(name) != -1)
                    {
                        im = true;
                        break;
                    }
                }

                if (im)
                {
                    for (int i = 0; i < lines.Count; i++)
                    {
                        string xyr = lines[i];
                        string[] name2 = xyr.Split(' ');
                        if (name2[0] == name)
                        {
                            if (Int32.Parse(name2[2]) < currentScore)
                            {
                                lines[i] = $"{name} - {currentScore}";
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
                else
                {
                    lines.Add($"{name} - {currentScore}");
                }

                System.IO.File.WriteAllLines("Space_invaders-сложно.txt", lines.ToArray());
            }
        }


        /// <summary>
        /// Метод отключает таймер в случае закрытия формы с игрой.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            dispatcherTimer.IsEnabled = false;
        }


        /// <summary>
        /// Изолированный класс, созданн для проведения unit-тестов.
        /// </summary>
        public class SpaceInvaders
        {
            public int slogn;
            public int enemySpeed;
            public int bulletTimerLimit;
            public void StartGame(int o)
            {
                slogn = o;
                if (o == 0)
                {

                    enemySpeed = 6;
                    bulletTimerLimit = 90;


                }
                if (o == 1)
                {
                    enemySpeed = 10;
                    bulletTimerLimit = 90;
                }
            }

            List<string> lines = new List<string>() { "DS - 34", "a - 101", "джинглибомбом - 87" };
            List<string> lines2 = new List<string>() { "a - 218", "Gegsag - 98" };
            public string[] k;
            public void Reyting(int o,string name,int currentScore)
            {
                slogn = o;
                if (slogn == 0)
                {
                    bool im = false;
                    for (int i = 0; i < lines.Count; i++)
                    {
                        if (lines[i].IndexOf(name) != -1)
                        {
                            im = true;
                            break;
                        }
                    }

                    if (im)
                    {
                        for (int i = 0; i < lines.Count; i++)
                        {
                            string xyr = lines[i];
                            string[] name2 = xyr.Split(' ');
                            if (name2[0] == name)
                            {
                                if (Int32.Parse(name2[2]) < currentScore)
                                {
                                    lines[i] = $"{name} - {currentScore}";
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        lines.Add($"{name} - {currentScore}");
                    }

                    k = lines.ToArray();
                }
                if(slogn == 1)
                {
                    bool im = false;
                    for (int i = 0; i < lines2.Count; i++)
                    {
                        if (lines2[i].IndexOf(name) != -1)
                        {
                            im = true;
                            break;
                        }
                    }

                    if (im)
                    {
                        for (int i = 0; i < lines2.Count; i++)
                        {
                            string xyr = lines2[i];
                            string[] name2 = xyr.Split(' ');
                            if (name2[0] == name)
                            {
                                if (Int32.Parse(name2[2]) < currentScore)
                                {
                                    lines2[i] = $"{name} - {currentScore}";
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        lines2.Add($"{name} - {currentScore}");
                    }

                    k = lines2.ToArray();
                }
            }
        }
    }
}
