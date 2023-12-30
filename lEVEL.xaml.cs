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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для lEVEL.xaml
    /// </summary>
    public partial class lEVEL : Window
    {
        int game;

        public int complexity;
        public lEVEL()
        {
            InitializeComponent();
            PreviewKeyDown += new KeyEventHandler(Back);

            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri("pngwing.com (34).png", UriKind.Relative));
            ll2.Background = imageBrush;
            ll2.BorderBrush = Brushes.Transparent;
        }

        /// <summary>
        /// Метод сохраняет в файл выбронную сложность (нормальная сложность) и открывает форму с выбранной игрой.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter streamWriter = new StreamWriter("Сложность.txt");
            streamWriter.WriteLine("0");
            streamWriter.Close();

            StreamReader sr = new StreamReader("Game.txt");
            game = Int32.Parse(sr.ReadToEnd());
            sr.Close();
            switch (game)
            {
                case 1:
                    ZMEYKA zMEYKA = new ZMEYKA();
                    zMEYKA.Show();
                    break;
                case 2:
                    Pin_pong pin_pong = new Pin_pong();
                    pin_pong.Show();
                    break;
                case 3:
                    Space_invaders space_invaders = new Space_invaders();
                    space_invaders.Show();
                    break;
                case 4:
                    Pac_man pac_Man = new Pac_man();
                    pac_Man.Show();
                    break;
                case 5:
                    Flappy_Bird flappy_Bird = new Flappy_Bird();
                    flappy_Bird.Show();
                    break;
            }

        }
        /// <summary>
        /// Метод сохраняет в файл выбронную сложность (высокая сложность) и открывает форму с выбранной игрой.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            StreamWriter streamWriter = new StreamWriter("Сложность.txt");
            streamWriter.WriteLine("1");
            streamWriter.Close();

            StreamReader sr = new StreamReader("Game.txt");
            game = Int32.Parse(sr.ReadToEnd());
            sr.Close();
            switch (game)
            {
                case 1:
                    ZMEYKA zMEYKA = new ZMEYKA();
                    zMEYKA.Show();
                    break;
                case 2:
                    Pin_pong pin_pong = new Pin_pong();
                    pin_pong.Show();
                    break;
                case 3:
                    Space_invaders space_invaders = new Space_invaders();
                    space_invaders.Show();
                    break;
                case 4:
                    Pac_man pac_Man = new Pac_man();
                    pac_Man.Show();
                    break;
                case 5:
                    Flappy_Bird flappy_Bird = new Flappy_Bird();
                    flappy_Bird.Show();
                    break;
            }

        }

        /// <summary>
        /// Метод открывает справку при нажатии на F1 и закрывает форму при нажатии на Esc.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MENU mainWindow = new MENU();
                mainWindow.Show();
                this.Close();
            }
            if (e.Key == Key.F1)
            {
                SPRAVKA sPRAVKA = new SPRAVKA();
                sPRAVKA.Show();
            }
        }
        /// <summary>
        /// Метод закрывает форму и открывает форму меню выбора игры.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lll_Click(object sender, RoutedEventArgs e)
        {
            MENU mainWindow1 = new MENU();
            mainWindow1.Show();
            this.Close();
        }
    }
}
