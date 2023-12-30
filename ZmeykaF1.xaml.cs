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
using System.Windows.Threading;
using System.IO;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для ZmeykaF1.xaml
    /// </summary>
    public partial class ZmeykaF1 : Window
    {
        
        public ZmeykaF1()
        {
            InitializeComponent();

            PreviewKeyDown += new KeyEventHandler(Windos_KeyDown);

            this.Closing += Window_Closed;
        }

        /// <summary>
        /// Метод выводит сообщение на экран перед закрытием формы.
        /// </summary>
        private void Window_Closed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBox.Show("Для продолжения нажмите Enter");
        }
        /// <summary>
        /// Метод закрывает форму при нажатии на кнопку F1.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Windos_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}
