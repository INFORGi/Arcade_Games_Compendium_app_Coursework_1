using System;
using System.Collections.Generic;
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
using System.IO;
using static WpfApp1.ZMEYKA;
using Microsoft.SqlServer.Server;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для ZMEYKA.xaml
    /// </summary>
    public partial class ZMEYKA : Window
    {
        Random rnd = new Random();
        public ZMEYKA()
        {
            InitializeComponent();

            gameTickTimer.Tick += GameTickTimer_Tick;
            PreviewKeyDown += new KeyEventHandler(Stop);
            this.Closing += Window_Closed;
        }

        int SnakeSquareSize = 30;
        int SnakeStartLength = 3;
        int SnakeStartSpeed;
        int SnakeSpeedThreshold = 100;



        public SolidColorBrush snakeBodyBrush = Brushes.DarkGreen;
        private SolidColorBrush snakeHeadBrush = Brushes.Orange;
        private List<SnakePart> snakeParts = new List<SnakePart>();

        public enum SnakeDirection { Left, Right, Up, Down };
        private SnakeDirection snakeDirection = SnakeDirection.Right;
        int snakeLength;

        public System.Windows.Threading.DispatcherTimer gameTickTimer = new System.Windows.Threading.DispatcherTimer();

        private UIElement snakeFood = null;
        public SolidColorBrush foodBrush = Brushes.GreenYellow;

        public int currentScore = 0;

        /// <summary>
        /// Метод постоянно обновляется, и приводит код в действие.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            Slo();
            StartNewGame();
        }

        int slognost;
        /// <summary>
        /// Метод определения сложности игры.
        /// </summary>
        private void Slo()
        {
            StreamReader se = new StreamReader("Сложность.txt");
            string line = se.ReadLine();
            int o = Int32.Parse(line);
            slognost = o;
            if (o == 0)
            {
                SnakeStartSpeed = 250;
                se.Close();
            }
            if (o == 1)
            {
                SnakeStartSpeed = 100;
                se.Close();
            }
        }
        /// <summary>
        /// Класс, в котором хранятся сведениея о блоке "Голова", о расположении головы и сколько сегментов в змейке.
        /// </summary>
        public class SnakePart
        {
            public UIElement UiElement { get; set; }

            public Point Position { get; set; }

            public bool IsHead { get; set; }
            
        }

        /// <summary>
        /// Метод отвечает за создание тела змеи в начале игры.
        /// </summary>
        private void DrawSnake()
        {
            foreach (SnakePart snakePart in snakeParts)
            {
                if (snakePart.UiElement == null)
                {
                    snakePart.UiElement = new Rectangle()
                    {
                        Width = SnakeSquareSize,
                        Height = SnakeSquareSize,
                        Fill = (snakePart.IsHead ? snakeHeadBrush : snakeBodyBrush)
                    };
                    GameArea.Children.Add(snakePart.UiElement);
                    Canvas.SetTop(snakePart.UiElement, snakePart.Position.Y);
                    Canvas.SetLeft(snakePart.UiElement, snakePart.Position.X);
                }
            }
        }

        /// <summary>
        /// Метод отвечает за передвижение змеи.
        /// </summary>
        private void MoveSnake()
        {
            while (snakeParts.Count >= snakeLength)
            {
                    GameArea.Children.Remove(snakeParts[0].UiElement);
                    snakeParts.RemoveAt(0);

            }
            foreach (SnakePart snakePart in snakeParts)
            {
                (snakePart.UiElement as Rectangle).Fill = snakeBodyBrush;
                snakePart.IsHead = false;
            }
            SnakePart snakeHead = snakeParts[snakeParts.Count - 1];
            double nextX = snakeHead.Position.X;
            double nextY = snakeHead.Position.Y;

            switch (snakeDirection)
            {
                case SnakeDirection.Left:
                    nextX -= SnakeSquareSize;
                    break;
                case SnakeDirection.Right:
                    nextX += SnakeSquareSize;
                    break;
                case SnakeDirection.Up:
                    nextY -= SnakeSquareSize;
                    break;
                case SnakeDirection.Down:
                    nextY += SnakeSquareSize;
                    break;
            }

            snakeParts.Add(new SnakePart()
            {
                Position = new Point(nextX, nextY),
                IsHead = true
            });
            DrawSnake();
            DrawSnake();
            DoCollisionCheck();
        }

        /// <summary>
        /// Таймер, который обновляет метод MoveSnake.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameTickTimer_Tick(object sender, EventArgs e)
        {
            MoveSnake();
        }

        /// <summary>
        /// Метод отвечает за запуск игры, отрисовку тела змеи, генерации яблок и т.д.
        /// </summary>
        private void StartNewGame()
        {
            ImageBrush myBrush = new ImageBrush();

            myBrush.ImageSource = new BitmapImage(new Uri("1678247455_bogatyr-club-p-pikselnaya-trava-vid-sverkhu-foni-instagra-6.png", UriKind.Relative));
            GameArea.Background = myBrush;
            foreach (SnakePart snakeBodyPart in snakeParts)
            {
                if (snakeBodyPart.UiElement != null)
                    GameArea.Children.Remove(snakeBodyPart.UiElement);
            }
            snakeParts.Clear();
            if (snakeFood != null)
            {
                GameArea.Children.Remove(snakeFood);
            }
            currentScore = 0;
            snakeLength = SnakeStartLength;
            snakeDirection = SnakeDirection.Right;
            snakeParts.Add(new SnakePart() { Position = new Point(SnakeSquareSize * 5, SnakeSquareSize * 5) });
            gameTickTimer.Interval = TimeSpan.FromMilliseconds(SnakeStartSpeed);
            DrawSnake();
            DrawSnakeFood();
            UpdateGameStatus();     
            gameTickTimer.IsEnabled = true;
        }

        /// <summary>
        /// Метод определяет расположение еды на экране.
        /// </summary>
        private Point GetNextFoodPosition()
        {
            int maxX = (int)(GameArea.ActualWidth / SnakeSquareSize);
            int maxY = (int)(GameArea.ActualHeight / SnakeSquareSize);
            int foodX = rnd.Next(0, maxX) * SnakeSquareSize;
            int foodY = rnd.Next(0, maxY) * SnakeSquareSize;

            foreach (SnakePart snakePart in snakeParts)
            {
                if ((snakePart.Position.X == foodX) && (snakePart.Position.Y == foodY))
                    return GetNextFoodPosition();
            }

            return new Point(foodX, foodY);
        }

        /// <summary>
        /// Метод создает еду для змеи.
        /// </summary>
        private void DrawSnakeFood()
        {
            Point foodPosition = GetNextFoodPosition();
            snakeFood = new Ellipse()
            {
                Width = SnakeSquareSize,
                Height = SnakeSquareSize,
                Fill = foodBrush
            };
            GameArea.Children.Add(snakeFood);
            Canvas.SetTop(snakeFood, foodPosition.Y);
            Canvas.SetLeft(snakeFood, foodPosition.X);
        }

        /// <summary>
        /// Метод отвечает за обработку нажатия клавиши на клавиатуре, для смены напровления движения змеи.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            SnakeDirection originalSnakeDirection = snakeDirection;
            switch (e.Key)
            {
                case Key.W:
                    if (snakeDirection != SnakeDirection.Down)
                        snakeDirection = SnakeDirection.Up;
                    break;
                case Key.S:
                    if (snakeDirection != SnakeDirection.Up)
                        snakeDirection = SnakeDirection.Down;
                    break;
                case Key.A:
                    if (snakeDirection != SnakeDirection.Right)
                        snakeDirection = SnakeDirection.Left;
                    break;
                case Key.D:
                    if (snakeDirection != SnakeDirection.Left)
                        snakeDirection = SnakeDirection.Right;
                    break;
                case Key.Space:
                    StartNewGame();
                    break;
            }
            if (snakeDirection != originalSnakeDirection)
                MoveSnake();
        }

        /// <summary>
        /// Метод проверяет врезалась змея во что-то или нет, съела еду или нет и т.д.
        /// </summary>
        private void DoCollisionCheck()
        {
            SnakePart snakeHead = snakeParts[snakeParts.Count - 1];

            if ((snakeHead.Position.X == Canvas.GetLeft(snakeFood)) && (snakeHead.Position.Y == Canvas.GetTop(snakeFood)))
            {
                EatSnakeFood();
                return;
            }

            if ((snakeHead.Position.Y < 0) || (snakeHead.Position.Y >= GameArea.ActualHeight) ||
            (snakeHead.Position.X < 0) || (snakeHead.Position.X >= GameArea.ActualWidth))
            {
                EndGame();
            }

            foreach (SnakePart snakeBodyPart in snakeParts.Take(snakeParts.Count - 1))
            {
                if ((snakeHead.Position.X == snakeBodyPart.Position.X) && (snakeHead.Position.Y == snakeBodyPart.Position.Y))
                    EndGame();
            }
        }

        public int timerInterval;

        /// <summary>
        /// Метод обрабатывает поедание еды, прибовление очков и размера.
        /// </summary>
        private void EatSnakeFood()
        {
            snakeLength++;
            currentScore++;
            timerInterval = Math.Max(SnakeSpeedThreshold, (int)gameTickTimer.Interval.TotalMilliseconds - (currentScore * 2));
            gameTickTimer.Interval = TimeSpan.FromMilliseconds(timerInterval);
            GameArea.Children.Remove(snakeFood);
            DrawSnakeFood();
            UpdateGameStatus();
        }

        /// <summary>
        /// Метод обновляет выводимый счет.
        /// </summary>
        private void UpdateGameStatus()
        {
            this.Title = "Счет: " + currentScore;
        }

        /// <summary>
        /// Метод останавливает таймер, выводит сообщение. Отвечает за прекращение игры.
        /// </summary>
        private void EndGame()
        {
            gameTickTimer.IsEnabled = false;
            MessageBox.Show("Проигрыш!\n\n В следующий раз посторайся ни во что не врезаться)", "Нажми на пробел что бы начать сначала.");
            Reyting();
        }

        /// <summary>
        /// Метод обновляет счет игрока в игре Snake.
        /// </summary>
        public void Reyting()
        {
            if (slognost ==0)
            {
                StreamReader sr = new StreamReader("d.txt");
                string name = sr.ReadLine();
                sr.Close();

                List<string> lines = new List<string>(System.IO.File.ReadAllLines("Змейка.txt"));

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

                System.IO.File.WriteAllLines("Змейка.txt", lines.ToArray());
            }
            else
            {
                StreamReader sr = new StreamReader("d.txt");
                string name = sr.ReadLine();
                sr.Close();

                List<string> lines = new List<string>(System.IO.File.ReadAllLines("Змейка-сложно.txt"));

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

                System.IO.File.WriteAllLines("Змейка-сложно.txt", lines.ToArray());
            }

        }
        /// <summary>
        /// Метод обрабатывет нажатие на Esc, Enter и F1.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Stop(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                gameTickTimer.IsEnabled = false;
                this.Title = "Пауза";
            }
            if (e.Key == Key.Enter)
            {
                gameTickTimer.IsEnabled = true;
                this.Title = "Счет: " + currentScore;
            }
            if(e.Key == Key.F1)
            {
                this.Title = "Пауза";
                gameTickTimer.IsEnabled = false;
                ZmeykaF1 zmeykaF1 = new ZmeykaF1();
                zmeykaF1.Show();
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
