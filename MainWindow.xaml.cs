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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using WpfApp1;
using System.Data;
using System.Media;

/*#################################################################*/
/*# Название программы: "Сборник аркадных игр"                    #*/
/*#                                                               #*/
/*# Назначение программы: Осуществление работы разработанных игр. #*/
/*#                                                               #*/
/*# Разработчик: студент группы ПР-330/б Шульгин С.С.             #*/
/*#                                                               #*/
/*# Версия: 1.78                                                  #*/
/*#################################################################*/


namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWinddow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SoundPlayer player = new SoundPlayer("WWW.wav");
        public MainWindow()
        {
            InitializeComponent();

            PreviewKeyDown += new KeyEventHandler(MainWindow_Close);
            PreviewKeyDown += new KeyEventHandler(F);

            if (Setting.Index.ind == 0)
            {
                player.PlayLooping();
            }
            else
            {
                player.Stop();
            }


        }

        /// <summary>
        /// Закрывеает стартовое окно при нажатии Esc.
        /// </summary>
        private void MainWindow_Close(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
        /// <summary>
        /// Метод открывает справку.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void F(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.F1)
            {
                SPRAVKA sPRAVKA = new SPRAVKA();
                sPRAVKA.Show();
            }
        }
        /// <summary>
        /// Метод открывает форму Setting.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Set(object sender, RoutedEventArgs e)
        {
            Setting s = new Setting();
            s.Show();
            this.Close();
        }
        /// <summary>
        /// Метод открывает форму с регистрацией.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void S(object sender, RoutedEventArgs e)
        {
            REGISTR r = new REGISTR();
            r.Show();
            this.Close();
        }
        /// <summary>
        /// Метод Закрывает приложение.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void St(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Метод открывает форму для авторизации/входа.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void e(object sender, RoutedEventArgs e)
        {
            VHOD v = new VHOD();
            v.Show();
            this.Close();
        }
        /// <summary>
        /// Метод открывает справку.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ds(object sender, RoutedEventArgs e)
        {
            SPRAVKA sPRAVKA = new SPRAVKA();
            sPRAVKA.Show();
        }
        /// <summary>
        /// Метод открывает форму с рейтингом.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reyting(object sender, RoutedEventArgs e)
        {
            Рейтинг reytings = new Рейтинг();
            reytings.Show();
            this.Close();
        }
    }
}
