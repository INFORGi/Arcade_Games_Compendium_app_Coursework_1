using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для Breakout.xaml
    /// </summary>
    public partial class Breakout : Window
    {
        int ui = 1;
        Random ran = new Random();
        DispatcherTimer gameTimer = new DispatcherTimer();
        List<Rectangle> itemstoremove = new List<Rectangle>();

        enum Naprovlenie { Left, Right, Stop };

        Naprovlenie naprovlenie = Naprovlenie.Stop;
        
        Rectangle player;
        Rectangle enemy;
        Rectangle ball;

        int widthPlayer = 150;
        int heightPlayer = 25;
        int widthEnemy = 25;
        int heightEnemy = 25;
        double ballSpeedX;
        int ballSpeedY;

        int speed;
        int score;
        int slognost;

        public Breakout()
        {
            InitializeComponent();

            StartGame();
            PreviewKeyDown += new KeyEventHandler(KeyDowne);
            gameTimer.Tick += MainActive;
        }

        private void StartGame()
        {
            ui = 1;
            score = 0;
            naprovlenie = Naprovlenie.Stop;
            Area.Children.Clear();
            Slo();
            DrawPlayer();
            DrawEnemy();
            DrawBall();
            gameTimer.Interval = TimeSpan.FromMilliseconds(10);
            gameTimer.Start();
        }

        private void KeyDowne(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A)
            {
                naprovlenie = Naprovlenie.Left;
            }
            if (e.Key == Key.D)
            {
                naprovlenie = Naprovlenie.Right;
            }
            if(e.Key == Key.Space)
            {
                Reyting();
                StartGame();
            }
            if(e.Key == Key.Escape)
            {
                gameTimer.Stop();
            }
            if((e.Key == Key.Enter))
            {
                gameTimer.Start();
            }
        }

        private void DrawPlayer()
        {
            player = new Rectangle()
            {
                Tag = "player",
                Width = widthPlayer,
                Height = heightPlayer,
                Fill = Brushes.AliceBlue,
            };

            Canvas.SetLeft(player, 750 - widthPlayer / 2);
            Canvas.SetTop(player, 720);
            Area.Children.Add(player);
        }
        private void DrawBall()
        {
            ball = new Rectangle()
            {
                Tag = "ball",
                Width = 25,
                Height = 25,
                Fill = Brushes.White,
                Stroke = Brushes.Black,
                RadiusX = 25,
                RadiusY = 25,
            };
            Canvas.SetLeft(ball, Area.Width / 2 - ball.Width / 2);
            Canvas.SetTop(ball, Area.Height - player.Height - ball.Height - 10);
            Area.Children.Add(ball);
        }
        private void DrawEnemy()
        {
            double numberOfEnemy = Area.Width / widthEnemy;
            double numberOfEnemy2 = (Area.Height / 4) / heightEnemy;

            for (int j = 0; j < numberOfEnemy2; j++)
            {
                for (int i = 0; i < numberOfEnemy; i++)
                {
                    enemy = new Rectangle()
                    {
                        Tag = "enemy",
                        Width = widthEnemy,
                        Height = heightEnemy,
                    };
                    switch (ran.Next(0, 9))
                    {
                        case 0:
                            enemy.Fill = (Brushes.Blue);
                            break;
                        case 1:
                            enemy.Fill = (Brushes.White);
                            break;
                        case 2:
                            enemy.Fill = (Brushes.Red);
                            break;
                        case 3:
                            enemy.Fill = (Brushes.Yellow);
                            break;
                        case 4:
                            enemy.Fill = (Brushes.Green);
                            break;
                        case 5:
                            enemy.Fill = (Brushes.Orange);
                            break;
                        case 6:
                            enemy.Fill = (Brushes.Violet);
                            break;
                        case 7:
                            enemy.Fill = (Brushes.Pink);
                            break;
                        case 8:
                            enemy.Fill = (Brushes.BlueViolet);
                            break;
                        case 9:
                            enemy.Fill = (Brushes.AliceBlue);
                            break;
                    }
                    Canvas.SetLeft(enemy, i * widthEnemy);
                    Canvas.SetTop(enemy, j * heightEnemy);
                    Area.Children.Add(enemy);
                }
            }
        }
        private void MainActive(object sender, EventArgs e)
        {
            this.Title = $"Счет {score}";

            foreach (var x in Area.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "player")
                {
                    switch (naprovlenie)
                    {
                        case Naprovlenie.Left:
                            if (Canvas.GetLeft(x) > 0) // Проверяем, не выходит ли платформа за левый край экрана
                            {
                                Canvas.SetLeft(x, Canvas.GetLeft(x) - speed);
                            }
                            else
                            {
                                naprovlenie = Naprovlenie.Right;
                            }
                            break;
                        case Naprovlenie.Right:
                            if (Canvas.GetLeft(x) + x.Width < Area.ActualWidth) // Проверяем, не выходит ли платформа за правый край экрана
                            {
                                Canvas.SetLeft(x, Canvas.GetLeft(x) + speed);
                            }
                            else
                            {
                                naprovlenie = Naprovlenie.Left;
                            }
                            break;
                    }
                }
                if((string)x.Tag == "ball")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) + ballSpeedX);
                    Canvas.SetTop(x, Canvas.GetTop(x) + ballSpeedY);

                    if (Canvas.GetTop(x) < 0)
                    {
                        ballSpeedY *= -1;
                    }
                    if (Canvas.GetLeft(x) > Area.ActualWidth - x.Width || Canvas.GetLeft(x) < 0)
                    {
                        ballSpeedX *= -1;
                    }

                    Rect hitBoxPlayer = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
                    Rect hitBoxBall = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (hitBoxPlayer.IntersectsWith(hitBoxBall))
                    {
                        // Вычисляем точку столкновения
                        double collisionPoint = (Canvas.GetLeft(x) + (x.Width / 2)) - (Canvas.GetLeft(player) + (player.Width / 2));

                        // Нормализуем точку столкновения
                        collisionPoint = collisionPoint / (player.Width / 2);

                        // Вычисляем угол отражения
                        double angle = collisionPoint * (Math.PI / 2);

                        // Изменяем направление мяча
                        ballSpeedX = speed * Math.Sin(angle);
                        ballSpeedY *= -1;
                    }
                    foreach (var y in Area.Children.OfType<Rectangle>().ToList())
                    {
                        if ((string)y.Tag == "enemy")
                        {
                            Rect hitBoxEnemy = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);

                            if (hitBoxBall.IntersectsWith(hitBoxEnemy))
                            {
                                // Вычисляем точку столкновения
                                double collisionPoint = (Canvas.GetLeft(x) + (x.Width / 2)) - (Canvas.GetLeft(y) + (y.Width / 2));

                                // Нормализуем точку столкновения
                                collisionPoint = collisionPoint / (y.Width / 2);

                                // Вычисляем угол отражения
                                double angle = collisionPoint * (Math.PI / 2);

                                // Изменяем направление мяча
                                ballSpeedX = speed * Math.Sin(angle);
                                ballSpeedY *= -1;
                                itemstoremove.Add(y);
                                score++;
                            }
                        }
                    }
                }
            }
            if (Canvas.GetTop(ball) >= Area.Height)
            {
                EndGame();
            }
            if (score >= ui * 488)
            {
                DrawEnemy();
                ui++;
            }
            foreach (Rectangle y in itemstoremove)
            {
                Area.Children.Remove(y);
            }
        }
        private bool CheckCollision(Rectangle rect1, Rectangle rect2)
        {
            return (Canvas.GetLeft(rect1) < Canvas.GetLeft(rect2) + rect2.Width &&
                    Canvas.GetLeft(rect1) + rect1.Width > Canvas.GetLeft(rect2) &&
                    Canvas.GetTop(rect1) < Canvas.GetTop(rect2) + rect2.Height &&
                    Canvas.GetTop(rect1) + rect1.Height > Canvas.GetTop(rect2));
        }

        private void EndGame()
        {
            gameTimer.Stop();
            Area.Children.Clear();
            MessageBox.Show("Ты проиграл!");
            Reyting();
        }
        private void Slo()
        {
            StreamReader se = new StreamReader("Сложность.txt");
            string line = se.ReadLine();
            int o = Int32.Parse(line);
            slognost = o;
            if (o == 0)
            {
                speed = 5;
                ballSpeedY = ran.Next(3, 3);
                ballSpeedX = 5;
                se.Close();
            }
            if (o == 1)
            {
                speed = 10;
                ballSpeedY = ran.Next(1, 5);
                ballSpeedX = 5;
                se.Close();
            }
        }
        public void Reyting()
        {
            if (slognost == 0)
            {
                StreamReader sr = new StreamReader("d.txt");
                string name = sr.ReadLine();
                sr.Close();

                List<string> lines = new List<string>(System.IO.File.ReadAllLines("Breakout.txt"));

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
                            if (Int32.Parse(name2[2]) < score)
                            {
                                lines[i] = $"{name} - {score}";
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
                    lines.Add($"{name} - {score}");
                }

                System.IO.File.WriteAllLines("Breakout.txt", lines.ToArray());
            }
            else
            {
                StreamReader sr = new StreamReader("d.txt");
                string name = sr.ReadLine();
                sr.Close();

                List<string> lines = new List<string>(System.IO.File.ReadAllLines("Breakout-сложно.txt"));

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
                            if (Int32.Parse(name2[2]) < score)
                            {
                                lines[i] = $"{name} - {score}";
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
                    lines.Add($"{name} - {score}");
                }

                System.IO.File.WriteAllLines("Breakout-сложно.txt", lines.ToArray());
            }

        }
    }
}
