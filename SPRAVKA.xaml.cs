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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для SPRAVKA.xaml
    /// </summary>
    public partial class SPRAVKA : Window
    {
        public SPRAVKA()
        {
            InitializeComponent();
            PreviewKeyDown += new KeyEventHandler(Close);
        }

        /// <summary>
        /// Метод закрывает форму при нажатии на кнопку Esc.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}
