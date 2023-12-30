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


namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MENU.xaml
    /// </summary>
    public partial class MENU : Window
    {
        public MENU()
        {
            InitializeComponent();

            PreviewKeyDown += new KeyEventHandler(MainWindow_Close);

            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri("pngwing.com (34).png", UriKind.Relative));
            ll2.Background = imageBrush;
            ll2.BorderBrush = Brushes.Transparent;
        }
        /// <summary>
        /// Метод открывает справку при нажатии на F1 и закрывает форму при нажатии на Esc.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Close(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MainWindow lEVEL = new MainWindow();
                lEVEL.Show();
                this.Close();
            }
            if (e.Key == Key.F1)
            {
                SPRAVKA sPRAVKA = new SPRAVKA();
                sPRAVKA.Show();
            }
        }
        /// <summary>
        /// Метод обрабатывает выбор игры и сохраняет результат в файле для воспроизведения игры после выбора сложности.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter streamWriter = new StreamWriter("Game.txt");
            streamWriter.WriteLine(1);
            streamWriter.Close();

            lEVEL lEVEL = new lEVEL();
            lEVEL.Show();
            this.Close();
        }
        /// <summary>
        /// Метод обрабатывает выбор игры и сохраняет результат в файле для воспроизведения игры после выбора сложности.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StreamWriter streamWriter = new StreamWriter("Game.txt");
            streamWriter.WriteLine(2);
            streamWriter.Close();

            lEVEL lEVEL = new lEVEL();
            lEVEL.Show();
            this.Close();
        }
        /// <summary>
        /// Метод обрабатывает выбор игры и сохраняет результат в файле для воспроизведения игры после выбора сложности.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            StreamWriter streamWriter = new StreamWriter("Game.txt");
            streamWriter.WriteLine(3);
            streamWriter.Close();

            lEVEL lEVEL = new lEVEL();
            lEVEL.Show();
            this.Close();
        }
        /// <summary>
        /// Метод обрабатывает выбор игры и сохраняет результат в файле для воспроизведения игры после выбора сложности.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            StreamWriter streamWriter = new StreamWriter("Game.txt");
            streamWriter.WriteLine(4);
            streamWriter.Close();

            lEVEL lEVEL = new lEVEL();
            lEVEL.Show();
            this.Close();
        }
        /// <summary>
        /// Метод обрабатывает выбор игры и сохраняет результат в файле для воспроизведения игры после выбора сложности.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            StreamWriter streamWriter = new StreamWriter("Game.txt");
            streamWriter.WriteLine(5);
            streamWriter.Close();

            lEVEL lEVEL = new lEVEL();
            lEVEL.Show();
            this.Close();
        }
        /// <summary>
        /// Метод закрывает форму и открывает стартовое меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lll_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow1 = new MainWindow();
            mainWindow1.Show();
            this.Close();
        }
    }
}
