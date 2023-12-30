﻿using System;
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
    /// Логика взаимодействия для Pac_manF1.xaml
    /// </summary>
    public partial class Pac_manF1 : Window
    {
        public Pac_manF1()
        {
            InitializeComponent();
            PreviewKeyDown += new KeyEventHandler(fd);
            this.Closing += Window_Closed;
        }

        /// <summary>
        /// Метод закрывает форму при нажатии на кнопку Esc.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void fd(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
        /// <summary>
        /// Метод выводит на экран сообщение после закрытия формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBox.Show("Для продолжения нажмите Enter");
        }
    }
}
