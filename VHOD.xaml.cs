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
    /// Логика взаимодействия для VHOD.xaml
    /// </summary>
    public partial class VHOD : Window
    {
        public VHOD()
        {
            InitializeComponent();
            PreviewKeyDown += new KeyEventHandler(MainWindow_Close);

            lb1.MouseLeftButtonDown += (s, e) =>
            {
                REGISTR rEGISTR = new REGISTR();
                rEGISTR.Show();
                this.Close();
            };

            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri("pngwing.com (34).png", UriKind.Relative));
            lll.Background = imageBrush;
            lll.BorderBrush = Brushes.Transparent;
        }

        
        /// <summary>
        /// Метод проверяет есть ли введеный username  в файле. Если есть то открывается форма с выбором игры, если нет то предлагает зарегестрироваться, если поле пустое выводит сообщение-предупреждение.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StreamReader sr = new StreamReader("Пользователи.txt");
                string name = tx1.Text;

                StreamWriter sw2 = new StreamWriter("d.txt");
                sw2.WriteLine(name);
                sw2.Close();

                if (name != "")
                {
                    bool index = false;
                    int count = File.ReadLines("Пользователи.txt").Count();
                    for (int i = 0; i < count; i++)
                    {
                        string user_name = sr.ReadLine();
                        if (user_name == name)
                        {
                            index = true; 
                            break;
                        }
                    }
                    if (index)
                    {
                        MENU m = new MENU();
                        m.Show();
                        this.Close();
                    }
                    else
                    {
                        sr.Close();
                        throw new Exception("Такого пользовательского имени нет");
                    }
                }
                else
                {
                    sr.Close();
                    throw new Exception("Заполните все поля");
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                PreviewKeyDown += new KeyEventHandler(MainWindow_Close);
            }
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
        /// Метод открывает справку на кнопку F1.
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
