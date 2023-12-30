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
    /// Логика взаимодействия для Рейтинг.xaml
    /// </summary>
    public partial class Рейтинг : Window
    {
        public Рейтинг()
        {
            InitializeComponent();


            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri("pngwing.com (34).png", UriKind.Relative));
            ll2.Background = imageBrush;
            ll2.BorderBrush = Brushes.Transparent;
        }
        int level;
        string[] highLevel = { "Flappy-сложно.txt", "Pac man-сложно.txt", "Pin-pong-сложно.txt", "Space_invaders-сложно.txt", "Змейка-сложно.txt" };
        string[] highLevel2 = { "Flappy Bird", "Pac Man", "Ping Pong", "Space invaders", "Snake" };
        string[] lowLevel = { "Flappy.txt", "Pac man.txt", "Pin-pong.txt", "Space_invaders.txt", "Змейка.txt" };
        string[] lowLevel2 = { "Flappy Bird", "Pac Man", "Ping Pong", "Space invaders", "Snake" };




        /// <summary>
        /// Метод отвечающий, за вывод рейтинга в отсортированном виде, в лист бокс из файла.
        /// </summary>
        /// <param name="a"> Эта аеременная отвечает за выбор файла, из которого будут браться записи.</param>
        public void Vivod(string a)
        {
            list1.Items.Clear();
            List<string> lines = new List<string>(System.IO.File.ReadAllLines(a));

            List<string> userName = new List<string>();
            List<int> userScore = new List<int>();

            foreach (string line in lines)
            {
                string[] split = line.Split('-');
                userName.Add(split[0]);
                userScore.Add(Int32.Parse(split[1]));
            }

            List<KeyValuePair<string, int>> scoreList = new List<KeyValuePair<string, int>>();
            for (int i = 0; i < userName.Count; i++)
            {
                scoreList.Add(new KeyValuePair<string, int>(userName[i], userScore[i]));
            }

            scoreList.Sort((x, y) => y.Value.CompareTo(x.Value));

            for (int i = 0; i < Math.Min(10, scoreList.Count); i++)
            {
                list1.Items.Add(scoreList[i].Key + " - " + scoreList[i].Value);
            }
        }


        /// <summary>
        /// Это системный метод, он обрабатывает нажатие на комбобокс айтемс.
        /// </summary>
        private void cb0_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            int selectedIndex = cb0.SelectedIndex;
            if (level == 0)
            {
                if (selectedIndex == -1)
                {
                    selectedIndex = 0;
                }
                Vivod(lowLevel[selectedIndex]);
            }
            else if (level == 1)
            {
                if (selectedIndex == -1)
                {
                    selectedIndex = 1;
                }
                Vivod(highLevel[selectedIndex]);
            }
        }

        /// <summary>
        /// Метод обрабатывает нажатие на клавиатуру. При нажатие на кнопку Esc и кнопку F1 срабатывает код в этом методе.
        /// </summary>
        private void Reyting_KeuDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.Show();
            }
            if (e.Key == Key.F1)
            {
                SPRAVKA sPRAVKA = new SPRAVKA();
                sPRAVKA.Show();
            }
        }

        /// <summary>
        /// Выбор сложности игры.
        /// </summary>
        private void cb0_SelecfdtionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (bci1.IsSelected)
            {
                cb0.Items.Clear();
                lb1.Content = "Нормальная сложность";
                list1.Items.Clear();
                level = 0;

                for (int i = 0; i < lowLevel.Length; i++)
                {
                    int index = lowLevel[i].Length;
                    cb0.Items.Add(lowLevel2[i]);
                }
            }
            if (bci2.IsSelected)
            {
                cb0.Items.Clear();
                lb1.Content = "Высокая сложность";
                list1.Items.Clear();
                level = 1;

                for (int i = 0; i < highLevel.Length; i++)
                {
                    int index = highLevel[i].Length;

                    cb0.Items.Add(highLevel2[i]);
                }
            }

        }

        /// <summary>
        /// Метод отрабатывает нажатие на кнопку. Отвечает за закрытие формы.
        /// </summary>
        private void lll_Click(object sender, RoutedEventArgs e)
        {

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
