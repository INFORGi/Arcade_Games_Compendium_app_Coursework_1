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
    /// Логика взаимодействия для Pravilazmea.xaml
    /// </summary>
    public partial class Pravilazmea : Window
    {
        public Pravilazmea()
        {
            InitializeComponent();
        }

        private void J(object sender, RoutedEventArgs e)
        {
            ZMEYKA zMEYKA = new ZMEYKA();
            zMEYKA.Show();

            this.Close();

        }
    }
}
