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
using System.Xml.Linq;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для REGISTR.xaml
    /// </summary>
    public partial class REGISTR : Window
    {
        public REGISTR()
        {
            InitializeComponent();

            PreviewKeyDown += new KeyEventHandler(MainWindow_Close);


            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri("pngwing.com (34).png", UriKind.Relative));
            ll2.Background = imageBrush;
            ll2.BorderBrush = Brushes.Transparent;
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
        /// Метод добавляет новых имена новых пользователей в файл.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = tx1.Text;
            int count = File.ReadLines("Пользователи.txt").Count();
            try
            {
                bool w = false;
                if (name != "")
                {
                    StreamReader sr = new StreamReader("Пользователи.txt");
                    for (int i = 0; i < count; i++)
                    {
                        string line = sr.ReadLine();
                        if(line != name)
                        {
                            w = true;
                        }
                        else
                        {
                            sr.Close();
                            throw new Exception("Пользователь с таким именем уже есть. Придумайте что-то по лучше.)");
                        }
                    }
                    sr.Close();
                    if (w)
                    {
                        StreamWriter sw = new StreamWriter("Пользователи.txt", true);
                        sw.WriteLine(name);
                        sw.Close();
                        VHOD v = new VHOD();
                        v.Show();
                        this.Close();
                    }
                }
                else
                {
                    throw new Exception("Заполните все поля");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
        /// <summary>
        /// Метод закрывает форму и открывает стартовое меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lll_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
