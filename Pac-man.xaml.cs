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
using System.Windows.Threading;
using System.Xml.Linq;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Pac_man.xaml
    /// </summary>
    public partial class Pac_man : Window
    {
        int mnogitel;
        Random ran =new Random();
        DispatcherTimer gameTimer = new DispatcherTimer();
        bool goLeft;
        bool goRight;
        bool goDown;
        bool goUp;
        bool noLeft;
        bool noRight;
        bool noDown;
        bool noUp;
        int speed;
        Rect pacmanHitBox;
        int score = 0;

        /// <summary>
        /// Метод задает патерн поведения призрака в зависимости от выбранной сложноси.
        /// </summary>
        private void Window_ContentRendered()
        {
            if (slognost == 0)
            {
                foreach (var x in Area.Children.OfType<Rectangle>())
                {
                    if ((string)x.Tag == "ghost1")
                    {
                        double pacManLeft = Canvas.GetLeft(player);
                        double pacManTop = Canvas.GetTop(player);

                        double ghostLeft = Canvas.GetLeft(x);
                        double ghostTop = Canvas.GetTop(x);

                        double directionX = pacManLeft - ghostLeft;
                        double directionY = pacManTop - ghostTop;

                        double length = Math.Sqrt(directionX * directionX + directionY * directionY);
                        directionX /= length;
                        directionY /= length;

                        double nextLeft = ghostLeft + directionX * 10;
                        double nextTop = ghostTop + directionY * 10;

                        int direction = ran.Next(4);

                        switch (direction)
                        {
                            case 0:
                                nextLeft -= 10;
                                break;
                            case 1:
                                nextLeft += 10;
                                break;
                            case 2:
                                nextTop -= 10;
                                break;
                            case 3:
                                nextTop += 10;
                                break;
                        }

                        bool collision = false;

                        foreach (var y in Area.Children.OfType<Rectangle>())
                        {
                            if ((string)y.Tag == "wall")
                            {
                                if (nextLeft < Canvas.GetLeft(y) + y.Width && nextLeft + x.Width > Canvas.GetLeft(y) &&
                                    nextTop < Canvas.GetTop(y) + y.Height && nextTop + x.Height > Canvas.GetTop(y))
                                {
                                    collision = true;
                                    break;
                                }
                            }
                        }
                        if (nextLeft < 0 || nextTop < 0 || nextLeft + x.Width > Area.ActualWidth || nextTop + x.Height > Area.ActualHeight)
                        {
                            collision = true;
                        }
                        if (!collision)
                        {
                            Canvas.SetLeft(x, nextLeft);
                            Canvas.SetTop(x, nextTop);
                        }
                    }
                }
                foreach (var x in Area.Children.OfType<Rectangle>())
                {
                    if ((string)x.Tag == "ghost3")
                    {
                        double pacManLeft = Canvas.GetLeft(player);
                        double pacManTop = Canvas.GetTop(player);

                        double pacManDirectionX = 0;
                        double pacManDirectionY = 0;


                        double targetLeft = pacManLeft + pacManDirectionX * 4 * 10;
                        double targetTop = pacManTop + pacManDirectionY * 4 * 10;

                        double ghostLeft = Canvas.GetLeft(x);
                        double ghostTop = Canvas.GetTop(x);


                        double directionX = targetLeft - ghostLeft;
                        double directionY = targetTop - ghostTop;


                        double length = Math.Sqrt(directionX * directionX + directionY * directionY);
                        directionX /= length;
                        directionY /= length;


                        double nextLeft = ghostLeft + directionX * 10;
                        double nextTop = ghostTop + directionY * 10;

                        bool collision = false;

                        foreach (var y in Area.Children.OfType<Rectangle>())
                        {
                            if ((string)y.Tag == "wall")
                            {

                                if (nextLeft < Canvas.GetLeft(y) + y.Width && nextLeft + x.Width > Canvas.GetLeft(y) &&
                                    nextTop < Canvas.GetTop(y) + y.Height && nextTop + x.Height > Canvas.GetTop(y))
                                {
                                    collision = true;
                                    break;
                                }
                            }
                        }


                        if (nextLeft < 0 || nextTop < 0 || nextLeft + x.Width > Area.ActualWidth || nextTop + x.Height > Area.ActualHeight)
                        {
                            collision = true;
                        }
                        if (!collision)
                        {
                            Canvas.SetLeft(x, nextLeft);
                            Canvas.SetTop(x, nextTop);
                        }
                    }
                }
                foreach (var x in Area.Children.OfType<Rectangle>())
                {
                    if ((string)x.Tag == "ghost2")
                    {
                        double pacManLeft = Canvas.GetLeft(player); 
                        double pacManTop = Canvas.GetTop(player);

                        double ghostLeft = Canvas.GetLeft(x);
                        double ghostTop = Canvas.GetTop(x);

                        double distance = Math.Sqrt((pacManLeft - ghostLeft) * (pacManLeft - ghostLeft) + (pacManTop - ghostTop) * (pacManTop - ghostTop));

                        if (distance > 200)
                        {
                            double directionX = pacManLeft - ghostLeft;
                            double directionY = pacManTop - ghostTop;

                            double length = Math.Sqrt(directionX * directionX + directionY * directionY);
                            directionX /= length;
                            directionY /= length;

                            double nextLeft = ghostLeft + directionX * 10;
                            double nextTop = ghostTop + directionY * 10;

                            bool collision = false;

                            foreach (var y in Area.Children.OfType<Rectangle>())
                            {
                                if ((string)y.Tag == "wall")
                                {
                                    if (nextLeft < Canvas.GetLeft(y) + y.Width && nextLeft + x.Width > Canvas.GetLeft(y) &&
                                        nextTop < Canvas.GetTop(y) + y.Height && nextTop + x.Height > Canvas.GetTop(y))
                                    {
                                        collision = true;
                                        break;
                                    }
                                }
                            }
                            if (nextLeft < 0 || nextTop < 0 || nextLeft + x.Width > Area.ActualWidth || nextTop + x.Height > Area.ActualHeight)
                            {
                                collision = true;
                            }

                            if (!collision)
                            {
                                Canvas.SetLeft(x, nextLeft);
                                Canvas.SetTop(x, nextTop);
                            }
                        }
                        else
                        {
                            int direction = ran.Next(4);
                            double nextLeft = Canvas.GetLeft(x);
                            double nextTop = Canvas.GetTop(x);

                            switch (direction)
                            {
                                case 0:
                                    nextLeft -= 10;
                                    break;
                                case 1:
                                    nextLeft += 10;
                                    break;
                                case 2:
                                    nextTop -= 10;
                                    break;
                                case 3:
                                    nextTop += 10;
                                    break;
                            }

                            bool collision = false;

                            foreach (var y in Area.Children.OfType<Rectangle>())
                            {
                                if ((string)y.Tag == "wall")
                                {

                                    if (nextLeft < Canvas.GetLeft(y) + y.Width && nextLeft + x.Width > Canvas.GetLeft(y) && nextTop < Canvas.GetTop(y) + y.Height && nextTop + x.Height > Canvas.GetTop(y))
                                    {
                                        collision = true;
                                        break;
                                    }
                                }
                            }

                            if (nextLeft < 0 || nextTop < 0 || nextLeft + x.Width > Area.ActualWidth || nextTop + x.Height > Area.ActualHeight)
                            {
                                collision = true;
                            }

                            if (!collision)
                            {
                                switch (direction)
                                {
                                    case 0:
                                        Canvas.SetLeft(x, nextLeft);
                                        break;
                                    case 1:
                                        Canvas.SetLeft(x, nextLeft);
                                        break;
                                    case 2:
                                        Canvas.SetTop(x, nextTop);
                                        break;
                                    case 3:
                                        Canvas.SetTop(x, nextTop);
                                        break;
                                }
                            }
                        }
                    }
                }
                foreach (var x in Area.Children.OfType<Rectangle>())
                {
                    if ((string)x.Tag == "ghost4")
                    {
                        double pacManLeft = Canvas.GetLeft(player); // Получаем текущее положение Pac-Man
                        double pacManTop = Canvas.GetTop(player);

                        // Вычисляем направление движения Pac-Man
                        double pacManDirectionX = 0;
                        double pacManDirectionY = 0;
                        // Здесь вам нужно установить pacManDirectionX и pacManDirectionY в зависимости от текущего направления движения Pac-Man

                        // Вычисляем целевую точку впереди от Pac-Man
                        double targetLeft = pacManLeft + pacManDirectionX * 4 * 10; // Например, на 4 клетки вперед
                        double targetTop = pacManTop + pacManDirectionY * 4 * 10;

                        // Вычисляем точку, которая находится на том же расстоянии и в том же направлении от целевой точки
                        double inkyTargetLeft = targetLeft + pacManDirectionX * 4 * 10;
                        double inkyTargetTop = targetTop + pacManDirectionY * 4 * 10;

                        double ghostLeft = Canvas.GetLeft(x);
                        double ghostTop = Canvas.GetTop(x);

                        // Вычисляем направление к целевой точке Inky
                        double directionX = inkyTargetLeft - ghostLeft;
                        double directionY = inkyTargetTop - ghostTop;

                        // Нормализуем направление
                        double length = Math.Sqrt(directionX * directionX + directionY * directionY);
                        directionX /= length;
                        directionY /= length;

                        // Вычисляем следующее положение призрака
                        double nextLeft = ghostLeft + directionX * 10;
                        double nextTop = ghostTop + directionY * 10;

                        // Проверяем столкновения со стенами и границами игровой области, как и раньше...
                        bool collision = false;

                        foreach (var y in Area.Children.OfType<Rectangle>())
                        {
                            if ((string)y.Tag == "wall")
                            {
                                // Проверяем, пересекается ли предполагаемое следующее положение призрака со стеной
                                if (nextLeft < Canvas.GetLeft(y) + y.Width && nextLeft + x.Width > Canvas.GetLeft(y) &&
                                    nextTop < Canvas.GetTop(y) + y.Height && nextTop + x.Height > Canvas.GetTop(y))
                                {
                                    collision = true;
                                    break;
                                }
                            }
                        }
                        // Если нет столкновения, двигаем призрака
                        if (!collision)
                        {
                            Canvas.SetLeft(x, nextLeft);
                            Canvas.SetTop(x, nextTop);
                        }
                    }
                }

            }
            if (slognost == 1)
            {
                foreach (var x in Area.Children.OfType<Rectangle>())
                {
                    if ((string)x.Tag == "ghost1"|| (string)x.Tag == "ghost2" || (string)x.Tag == "ghost3" || (string)x.Tag == "ghost4")
                    {
                        double pacManLeft = Canvas.GetLeft(player);
                        double pacManTop = Canvas.GetTop(player);

                        double ghostLeft = Canvas.GetLeft(x);
                        double ghostTop = Canvas.GetTop(x);

                        double directionX = pacManLeft - ghostLeft;
                        double directionY = pacManTop - ghostTop;

                        double length = Math.Sqrt(directionX * directionX + directionY * directionY);
                        directionX /= length;
                        directionY /= length;

                        double nextLeft = ghostLeft + directionX * 10;
                        double nextTop = ghostTop + directionY * 10;

                        int direction = ran.Next(4);

                        switch (direction)
                        {
                            case 0:
                                nextLeft -= 10;
                                break;
                            case 1:
                                nextLeft += 10;
                                break;
                            case 2:
                                nextTop -= 10;
                                break;
                            case 3:
                                nextTop += 10;
                                break;
                        }

                        bool collision = false;

                        foreach (var y in Area.Children.OfType<Rectangle>())
                        {
                            if ((string)y.Tag == "wall")
                            {
                                if (nextLeft < Canvas.GetLeft(y) + y.Width && nextLeft + x.Width > Canvas.GetLeft(y) &&
                                    nextTop < Canvas.GetTop(y) + y.Height && nextTop + x.Height > Canvas.GetTop(y))
                                {
                                    collision = true;
                                    break;
                                }
                            }
                        }
                        if (nextLeft < 0 || nextTop < 0 || nextLeft + x.Width > Area.ActualWidth || nextTop + x.Height > Area.ActualHeight)
                        {
                            collision = true;
                        }
                        if (!collision)
                        {
                            Canvas.SetLeft(x, nextLeft);
                            Canvas.SetTop(x, nextTop);
                        }
                    }
                }
            }
        }

        public Pac_man()
        {
            InitializeComponent();
            Slo();
            StartGame();
            gameTimer.Tick += GameLoop;
            this.Closing += Window_Closed;
        }

        /// <summary>
        /// Метод отвечает за смену направления игрока.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A && noLeft == false)
            {
                goRight = goUp = goDown = false;
                noRight = noUp = noDown = false;
                goLeft = true;
                player.RenderTransform = new RotateTransform(-180, player.Width / 2, player.Height / 2);
            }
            if (e.Key == Key.D && noRight == false)
            {
                noLeft = noUp = noDown = false;
                goLeft = goUp = goDown = false;
                goRight = true;
                player.RenderTransform = new RotateTransform(0, player.Width / 2, player.Height / 2);
            }
            if (e.Key == Key.W && noUp == false)
            {
                noRight = noDown = noLeft = false; 
                goRight = goDown = goLeft = false; 
                goUp = true; 
                player.RenderTransform = new RotateTransform(-90, player.Width / 2, player.Height / 2); 
            }
            if (e.Key == Key.S && noDown == false)
            {
                noUp = noLeft = noRight = false;
                goUp = goLeft = goRight = false;
                goDown = true;
                player.RenderTransform = new RotateTransform(90, player.Width / 2, player.Height / 2);
            }
            if(e.Key == Key.Escape)
            {
                gameTimer.Stop();
            }
            if(e.Key == Key.Enter)
            {
                gameTimer.Start();
            }
            if(e.Key == Key.Space)
            {
                foreach (var x in Area.Children.OfType<Rectangle>())
                {
                    if ((string)x.Tag == "coin")
                    {
                        x.Visibility = Visibility.Visible;
                    }
                }
                Reyting();
                StartGame();
            }
            if (e.Key == Key.F1)
            {
                gameTimer.IsEnabled = false;
                Pac_manF1 s = new Pac_manF1();
                s.Show();
            }
        }
        /// <summary>
        /// Метод задет внешний облик игроку и призракам, а так же задает их расположение.
        /// </summary>
        private void StartGame()
        {
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri("pacman-png-18.png", UriKind.Relative));
            player.Fill = imageBrush;

            ImageBrush imageBrush1 = new ImageBrush();
            imageBrush1.ImageSource = new BitmapImage(new Uri("pngwing.com (4).png", UriKind.Relative));
            prizrak.Fill = imageBrush1;

            ImageBrush imageBrush2 = new ImageBrush();
            imageBrush2.ImageSource = new BitmapImage(new Uri("pngwing.com (3).png", UriKind.Relative));
            prizrak2.Fill = imageBrush2;

            ImageBrush imageBrush3 = new ImageBrush();
            imageBrush3.ImageSource = new BitmapImage(new Uri("pngwing.com (4) копия.png", UriKind.Relative));
            prizrak3.Fill = imageBrush3;

            ImageBrush imageBrush4 = new ImageBrush();
            imageBrush4.ImageSource = new BitmapImage(new Uri("pngwing.com (2).png", UriKind.Relative));
            prizrak4.Fill = imageBrush4;

            Canvas.SetLeft(player, Area.ActualWidth / 2);
            Canvas.SetTop(player, 0);

            score = 0;
            Canvas.SetLeft(prizrak2, ran.Next(0, 1265));
            Canvas.SetTop(prizrak2, 490);
            Canvas.SetLeft(prizrak3, ran.Next(0, 1265));
            Canvas.SetTop(prizrak3, 490);
            Canvas.SetLeft(prizrak, ran.Next(0, 1265));
            Canvas.SetTop(prizrak, 490);
            Canvas.SetLeft(prizrak4, ran.Next(0, 1265));
            Canvas.SetTop(prizrak4, 490);
            Area.Focus();
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();
        }

        /// <summary>
        /// Метод обновляет расположение игрока, призраков. Обновляет счет.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameLoop(object sender, EventArgs e)
        {
            mnogitel++;
            if(goRight)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + speed);
            }
            if (goLeft)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - speed);
            }
            if (goUp)
            {
                Canvas.SetTop(player, Canvas.GetTop(player) - speed);
            }
            if (goDown)
            {
                Canvas.SetTop(player, Canvas.GetTop(player) + speed);
            }

            if (goDown && Canvas.GetTop(player) + 10 + player.Height > Area.ActualHeight)
            {
                noDown = true;
                goDown = false;
            }
            if (goUp && Canvas.GetTop(player) < 1)
            {
                noUp = true;
                goUp = false;
            }
            if (goLeft && Canvas.GetLeft(player) - 10 < 1)
            {
                noLeft = true;
                goLeft = false;
            }
            if (goRight && Canvas.GetLeft(player) + 10 + player.Width > Area.ActualWidth)
            {
                noRight = true;
                goRight = false;
            }

            pacmanHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
            foreach (var x in Area.Children.OfType<Rectangle>())
            {
                Rect hitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height); 
                if ((string)x.Tag == "wall")
                {
                    if (goLeft == true && pacmanHitBox.IntersectsWith(hitBox))
                    {
                        Canvas.SetLeft(player, Canvas.GetLeft(player) + 10);
                        noLeft = true;
                        goLeft = false;
                    }
                    if (goRight == true && pacmanHitBox.IntersectsWith(hitBox))
                    {
                        Canvas.SetLeft(player, Canvas.GetLeft(player) - 10);
                        noRight = true;
                        goRight = false;
                    }
                    if (goDown == true && pacmanHitBox.IntersectsWith(hitBox))
                    {
                        Canvas.SetTop(player, Canvas.GetTop(player) - 10);
                        noDown = true;
                        goDown = false;
                    }
                    if (goUp == true && pacmanHitBox.IntersectsWith(hitBox))
                    {
                        Canvas.SetTop(player, Canvas.GetTop(player) + 10);
                        noUp = true;
                        goUp = false;
                    }
                }
                if ((string)x.Tag == "ghost1"|| (string)x.Tag == "ghost2" || (string)x.Tag == "ghost3" || (string)x.Tag == "ghost4")
                {
                    if (pacmanHitBox.IntersectsWith(hitBox))
                    {
                        EndGame("Тебя поймал призрак. Press F");
                    }
                }
                if ((string)x.Tag == "coin")
                {
                    if (pacmanHitBox.IntersectsWith(hitBox) && x.Visibility == Visibility.Visible)
                    {
                        x.Visibility = Visibility.Hidden;
                        score++;
                        this.Title = $"Счет: {score.ToString()}";
                    }
                }
            }
            if (score == 294)//294
            {
                EndGame("Ты победил");
            }

                Window_ContentRendered();
        }

        /// <summary>
        /// Метод срабатывает после окончания игры.
        /// </summary>
        /// <param name="message"></param>
        private void EndGame(string message)
        {
            gameTimer.Stop();
            Reyting();
            MessageBox.Show(message);
        }

        int slognost;
        /// <summary>
        /// Метод задает сложность игры.
        /// </summary>
        private void Slo()
        {
            StreamReader se = new StreamReader("Сложность.txt");
            string line = se.ReadLine();
            int o = Int32.Parse(line);
            slognost = o;
            if (o == 0)
            {
                speed = 8;
                se.Close();
            }
            if (o == 1)
            {
                speed = 10;
                se.Close();
            }
        }
        /// <summary>
        /// Метод обновляет счет игрока в игре Pac Man.
        /// </summary>
        public void Reyting()
        {
            if (slognost == 0)
            {
                StreamReader sr = new StreamReader("d.txt");
                string name = sr.ReadLine();
                sr.Close();

                List<string> lines = new List<string>(System.IO.File.ReadAllLines("Pac man.txt"));

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

                System.IO.File.WriteAllLines("Pac man.txt", lines.ToArray());
            }
            else
            {
                StreamReader sr = new StreamReader("d.txt");
                string name = sr.ReadLine();
                sr.Close();

                List<string> lines = new List<string>(System.IO.File.ReadAllLines("Pac man-сложно.txt"));

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

                System.IO.File.WriteAllLines("Pac man-сложно.txt", lines.ToArray());
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
        }
    }
}
