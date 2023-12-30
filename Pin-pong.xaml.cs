using Moq;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xunit;
using static WpfApp1.ZMEYKA;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Pin_pong.xaml
    /// </summary>
    public partial class Pin_pong : Window
    {
        Random ran = new Random();
        Ellipse ball;
        Rectangle leftPaddle;

        System.Windows.Threading.DispatcherTimer gameTickTimer = new System.Windows.Threading.DispatcherTimer();

        public int ballSpeedX;
        public int ballSpeedY;
        double leftPaddleSpeed = 60;
        int timerInterval = 30;
        int slognost;
        public double f;
        public double f2;
        int currentScore = 0;
        string line;

        int index;

        public Pin_pong()
        {
            InitializeComponent();
            StreamReader se = new StreamReader("Сложность.txt");
            line = se.ReadLine();
            se.Close();
            PreviewKeyDown += new KeyEventHandler(Window_KeyDown);
            gameTickTimer.Tick += GameTickTimer_Tick;
            gameTickTimer.IsEnabled = true;
            this.Closing += Window_Closed;


            ImageBrush myBrush = new ImageBrush();

            myBrush.ImageSource = new BitmapImage(new Uri("pin-pong.jpg", UriKind.Relative));
            Area.Background = myBrush;
        }
        /// <summary>
        /// Метод постоянно обновляется, и приводит код в действие.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            Slo();
            StartGame();

            
        }

        /// <summary>
        /// Метод отвечает за запуск игры, отрисовку ракетки, генерации мяча.
        /// </summary>
        public void StartGame()
        {
            this.Activate();
            currentScore = 0;
            Area.Children.Clear();
            DrawBall();
            LeftDrawPaddle();
            gameTickTimer.IsEnabled = true;
        }
        /// <summary>
        /// Метод обновляет расположение мяча, ракетки, счета игрока.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GameTickTimer_Tick(object sender, EventArgs e)
        {
            this.Title = "Счет: " + currentScore;
            if (index == 1)
            {
                Canvas.SetLeft(ball, f);
                Canvas.SetTop(ball, f2);
                index = 0;
            }

            Canvas.SetLeft(ball, Canvas.GetLeft(ball) + ballSpeedX);
            Canvas.SetTop(ball, Canvas.GetTop(ball) + ballSpeedY);

            if (Canvas.GetTop(ball) < 0 || Canvas.GetTop(ball) > Area.ActualHeight - ball.Height)
            {
                ballSpeedY *= -1;
            }

            if (Canvas.GetLeft(ball) > Area.ActualWidth - ball.Width)
            {
                ballSpeedX *= -1;
            }
            if (Canvas.GetLeft(ball) < 0)
            {
                EndGame();
            }

            if (Canvas.GetLeft(ball) < leftPaddle.Width && Canvas.GetTop(ball) > Canvas.GetTop(leftPaddle) && Canvas.GetTop(ball) < Canvas.GetTop(leftPaddle) + leftPaddle.Height)
            {
                ballSpeedX *= -1;
                if (ballSpeedX < 50 & ballSpeedY < 50)
                {
                    ballSpeedY += 1;
                    ballSpeedX += 1;
                }
                currentScore++;
            }
        }
        /// <summary>
        /// Метод создает мяч.
        /// </summary>
        public void DrawBall()
        {
            ball = new Ellipse
            {
                Width = 30,
                Height = 30,
                Fill = Brushes.White,
                Stroke = new SolidColorBrush(Colors.Black)
            };

            Canvas.SetLeft(ball, 10);
            Canvas.SetTop(ball, 10);
            Area.Children.Add(ball);
        }
        /// <summary>
        /// Метод сздает ракетку.
        /// </summary>
        public void LeftDrawPaddle()
        {
            leftPaddle = new Rectangle
            {
                Width = 20,
                Height = 150,
                Fill = Brushes.Red,
                Stroke = new SolidColorBrush(Colors.Black)
            };
            Canvas.SetLeft(leftPaddle, 0);
            Canvas.SetTop(leftPaddle, 200);

            Area.Children.Add(leftPaddle);
        }

        /// <summary>
        /// Метод отвечает за окончание игры, вывод сообщения о проигрыше.
        /// </summary>
        private void EndGame()
        {
            gameTickTimer.IsEnabled = false;
            Reyting();
            currentScore = 0;
            MessageBox.Show("Проигрыш!\n\n В следующий раз посторайся отбить мячик)", "Нажми на пробел что бы начать сначала.");
        }
        /// <summary>
        /// Метод задает сложность игры.
        /// </summary>
        private void Slo()
        {

            int o = Int32.Parse(line);
            slognost = o;
            if (o == 0)
            {
                ballSpeedY = ran.Next(5,10);
                ballSpeedX = ran.Next(5, 10);
                timerInterval = Math.Max(ballSpeedX, (int)gameTickTimer.Interval.TotalMilliseconds - (currentScore * 2));
                gameTickTimer.Interval = TimeSpan.FromMilliseconds(timerInterval);
            }
            if (o == 1)
            {
                ballSpeedY = ran.Next(10, 25);
                ballSpeedX = ran.Next(10, 25);
                timerInterval = Math.Max(ballSpeedX, (int)gameTickTimer.Interval.TotalMilliseconds - (currentScore * 2));
                gameTickTimer.Interval = TimeSpan.FromMilliseconds(timerInterval);
            }
        }
        /// <summary>
        /// Метод обновляет счет игрока в игре Ping Pong.
        /// </summary>
        public void Reyting()
        {
            if (slognost==0)
            {
                StreamReader sr = new StreamReader("d.txt");
                string name = sr.ReadLine();
                sr.Close();

                List<string> lines = new List<string>(System.IO.File.ReadAllLines("Pin-pong.txt"));

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

                System.IO.File.WriteAllLines("Pin-pong.txt", lines.ToArray());
            }
            else
            {
                StreamReader sr = new StreamReader("d.txt");
                string name = sr.ReadLine();
                sr.Close();

                List<string> lines = new List<string>(System.IO.File.ReadAllLines("Pin-pong-сложно.txt"));

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

                System.IO.File.WriteAllLines("Pin-pong-сложно.txt", lines.ToArray());
            }

        }
        /// <summary>
        /// Метод смещает ракетку в зависимости от нажатия клавишь смещения.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W && Canvas.GetTop(leftPaddle) > 0)
            {
                Canvas.SetTop(leftPaddle, Canvas.GetTop(leftPaddle) - leftPaddleSpeed);
            }

            if (e.Key == Key.S && Canvas.GetTop(leftPaddle) < Area.ActualHeight - leftPaddle.Height)
            {
                Canvas.SetTop(leftPaddle, Canvas.GetTop(leftPaddle) + leftPaddleSpeed);
            }
            if (e.Key == Key.Space)
            {
                Slo();
                StartGame();
            }
            if (e.Key == Key.Escape)
            {
                gameTickTimer.IsEnabled = false;
                this.Title = "Пауза";
            }
            if (e.Key == Key.Enter)
            {
                this.Title = "Счет: " + currentScore;
                gameTickTimer.IsEnabled = true;
            }
            if (e.Key == Key.F1)
            {
                this.Title = "Пауза";
                gameTickTimer.IsEnabled = false;
                index = 1;
                f = Canvas.GetLeft(ball);
                f2 = Canvas.GetTop(ball);

                Pin_pongF1 pin_PongF1 = new Pin_pongF1();
                pin_PongF1.Show();
            }
        }
        /// <summary>
        /// Метод отключает таймер в случае закрытия формы с игрой.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            gameTickTimer.IsEnabled = false;
        }
    }
}

