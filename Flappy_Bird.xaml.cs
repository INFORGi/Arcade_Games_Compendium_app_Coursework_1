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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Flappy_Bird.xaml
    /// </summary>
    public partial class Flappy_Bird : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        DispatcherTimer gameTimer2 = new DispatcherTimer();
        Random ran = new Random();

        List<Rectangle> itemstoremove = new List<Rectangle>();
        Rectangle flappyBird;
        Rectangle topPipe;
        Rectangle bottomPipe;
        Rectangle scoree;

        Rect flappyHitBox;
        Rect pipeHitBox;
        Rect scorePlus;

        int width = 150;
        int height = 450;
        int factor = 250;
        double speed;
        int random;
        int score;
        double speedPlayer;
        double gravity;
        int slognost;

        public bool movementFlappy = false;
        bool clear = false;
        private bool gameOver = false;


        public Flappy_Bird()
        {
            InitializeComponent();
            string[] ima = new string[] { "FlappyB1.png", "FlappyB2.png", "FlappyB3.png" };
            int im = ran.Next(0,3);

            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(ima[im], UriKind.Relative));

            Area.Background = myBrush;

            Slo();
            gameTimer.Tick += GameLoop;
            gameTimer2.Tick += MakingPipes;
            StartGame();
            this.Closing += Window_Closed;
        }
        /// <summary>
        /// Метод отвечает за совершение маленького рывка в верх.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_KeyisDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.W)
            {
                movementFlappy = true;
            }
            if(e.Key == Key.Space)
            {
                clear = false;
                StartGame();
            }
            if( e.Key == Key.Enter)
            {
                if (clear != true)
                {
                    gameTimer.Start();
                    gameTimer2.Start();
                }
            }
            if(e.Key == Key.Escape)
            {
                gameTimer.Stop();
                gameTimer2.Stop();
                Reyting();
            }
            if (e.Key == Key.F1)
            {
                gameTimer.Stop();
                gameTimer2.Stop();
                Flappy_BirdF1 flappy_BirdF1 = new Flappy_BirdF1();
                flappy_BirdF1.Show();
            }
        }
        /// <summary>
        /// Метод отвечает за падение игрока после того, как кнопка W будет отжата.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_KeyisUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                movementFlappy = false;
            }
        }
        /// <summary>
        /// Метод отвечает за запуск игры.
        /// </summary>
        public void StartGame()
        {
            gameOver = false;
            movementFlappy = false;
            Area.Children.Clear();
            score = 0;
            DrawFlappy();
            TopPipe();
            Score();
            BottomPipe();
            gameTimer.Interval = TimeSpan.FromMilliseconds(30);
            gameTimer2.Interval = TimeSpan.FromSeconds(2 / speed);
            gameTimer.Stop();
            gameTimer2.Stop();
        }
        /// <summary>
        /// Метод обновляет счет, расположение игрока и труб.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameLoop(object sender, EventArgs e)
        {
            this.Title = $"Счет: {score}";
            foreach (var x in Area.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "MovementPipes")
                {
                    pipeHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - 10 * speed);

                    if (Canvas.GetLeft(x) < -300)
                    {
                        itemstoremove.Add(x);
                    }
                }
                if ((string)x.Tag == "MovementPipesScore")
                {
                    scorePlus = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - 10 * speed);

                    if (Canvas.GetLeft(x) < -300)
                    {
                        itemstoremove.Add(x);
                    }
                }

                if ((string)x.Tag == "Player")
                {
                    flappyHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (movementFlappy)
                    {
                        Canvas.SetTop(x, Canvas.GetTop(x) - 10 * speedPlayer);
                    }
                    else
                    {
                        Canvas.SetTop(x, Canvas.GetTop(x) + 10 * gravity);
                    }

                    if (!gameOver && (Canvas.GetTop(x) <= 0 || Canvas.GetTop(x) >= Area.ActualHeight))
                    {
                        EndGame();
                    }
                }
                if (!gameOver && flappyHitBox.IntersectsWith(pipeHitBox))
                {
                    EndGame();
                }
                if (flappyHitBox.IntersectsWith(scorePlus))
                {
                    score++;
                }
            }

            foreach (Rectangle y in itemstoremove)
            {
                Area.Children.Remove(y);
            }
        }

        /// <summary>
        /// Метод срабатывает после окончания игры.
        /// </summary>
        public void EndGame()
        {
            gameOver = true;
            gameTimer.Stop();
            gameTimer2.Stop();
            Reyting();
            clear = true;
            MessageBox.Show("Ты проиграл. Попробуй снова.");
        }
        /// <summary>
        /// Метод создает игрока.
        /// </summary>
        private void DrawFlappy()
        {
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri("flappybird.png", UriKind.RelativeOrAbsolute));
            flappyBird = new Rectangle()
            {
                Tag = "Player",
                Width = 75,
                Height = 75,
                Fill = myBrush,
            };
            myBrush.Stretch = Stretch.Fill;
            Canvas.SetTop(flappyBird, 350);
            Canvas.SetLeft(flappyBird, 100);
            Area.Children.Add(flappyBird);
        }
        /// <summary>
        /// Метод создает верхнюю трубу.
        /// </summary>
        private void TopPipe()
        {
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri("flappy2.png", UriKind.Relative));
            random = ran.Next(-440, 0);
            topPipe = new Rectangle()
            {
                Tag = "MovementPipes",
                Width = width,
                Height = height,
                Fill = myBrush,
            };
            myBrush.Stretch = Stretch.Fill;
            Canvas.SetLeft(topPipe, 1400);
            Canvas.SetTop(topPipe, random);
            Area.Children.Add(topPipe);
        }
        /// <summary>
        /// Метод создает нижнюю трубу.
        /// </summary>
        private void BottomPipe()
        {
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri("flappy.png", UriKind.Relative));
            bottomPipe = new Rectangle()
            {
                Tag = "MovementPipes",
                Width = width,
                Height = height,
                Fill = myBrush,
            };
            myBrush.Stretch = Stretch.Fill;
            Canvas.SetLeft(bottomPipe, 1400);
            Canvas.SetTop(bottomPipe, random + height + factor);
            Area.Children.Add(bottomPipe);
        }
        /// <summary>
        /// Метод создает участок в котором игроку будут присуждаться очки.
        /// </summary>
        private void Score()
        {
            scoree = new Rectangle()
            {
                Tag = "MovementPipesScore",
                Width = 0.05,
                Height = 300,
                Fill = Brushes.Transparent

            };

            Canvas.SetLeft(scoree, 1400);
            Canvas.SetTop(scoree, random + height);
            Area.Children.Add(scoree);
        }
        /// <summary>
        /// Метод создает трубы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MakingPipes(object sender, EventArgs e)
        {
            TopPipe();
            Score();
            BottomPipe();
        }
        /// <summary>
        /// Метод задает сложность игры.
        /// </summary>
        public void Slo()
        {
            StreamReader se = new StreamReader("Сложность.txt");
            string line = se.ReadLine();
            int o = Int32.Parse(line);
            slognost = o;
            if (o == 0)
            {
                speed = 1;
                speedPlayer = 2;
                gravity = 1;
                se.Close();
            }
            if (o == 1)
            {
                speed = 1.9;
                speedPlayer = 1.5;
                gravity = 1.5;
                se.Close();
            }
        }
        /// <summary>
        /// Метод обновляет счет игрока в игре Flappy Bird.
        /// </summary>
        public void Reyting()
        {
            if (slognost == 0)
            {
                StreamReader sr = new StreamReader("d.txt");
                string name = sr.ReadLine();
                sr.Close();

                List<string> lines = new List<string>(System.IO.File.ReadAllLines("Flappy.txt"));

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

                System.IO.File.WriteAllLines("Flappy.txt", lines.ToArray());
            }
            else
            {
                StreamReader sr = new StreamReader("d.txt");
                string name = sr.ReadLine();
                sr.Close();

                List<string> lines = new List<string>(System.IO.File.ReadAllLines("Flappy-сложно.txt"));

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

                System.IO.File.WriteAllLines("Flappy-сложно.txt", lines.ToArray());
            }

        }
        /// <summary>
        /// Метод отключает таймер в случае закрытия формы с игрой.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            gameTimer.Stop();
            gameTimer2.Stop();
        }
    }
}
