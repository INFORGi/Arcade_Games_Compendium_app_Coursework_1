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
    /// Логика взаимодействия для Setting.xaml
    /// </summary>
    public partial class Setting : Window
    {
        public Setting()
        {
            InitializeComponent();
            PreviewKeyDown += new KeyEventHandler(MainWindow_Close);

            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri("pngwing.com (34).png", UriKind.Relative));
            lll.Background = imageBrush;
            lll.BorderBrush = Brushes.Transparent;
        }
        /// <summary>
        /// Метод закрывает форму при нажатии на кнопку Esc.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Close(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }
        }

        /// <summary>
        /// Метод открывает справку.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Key_Downe(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                SPRAVKA sPRAVKA = new SPRAVKA();
                sPRAVKA.Show();
            }
        }

        public static class Index
        {
            public static int ind = 1;

        }

        int s = 1;

        /// <summary>
        /// Метод отвечает за отключение музыки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rb1_Click(object sender, RoutedEventArgs e)
        {
            s = 1;
            
        }
        /// <summary>
        /// Метод отвечает за включение музыки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rb2_Click(object sender, RoutedEventArgs e)
        {
            s = 0;
        }

        /// <summary>
        /// Метод применяет заданные настройки включить/выключить музыку.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddd(object sender, RoutedEventArgs e)
        {
            if (s == 1)
            {
                Index.ind = 1;
            }
            if(s == 0)
            {
                Index.ind = 0;
            }
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        /// <summary>
        /// Метод закрывает форму и открывает стартовое меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lll_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }
    }
}
