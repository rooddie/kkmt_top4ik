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
using System.Media;
namespace WpfApp4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        int st = 1; /// Счетчик шагов

        /// <summary>
        /// Старт игры
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            //Музыка на фоне
            SoundPlayer sp = new SoundPlayer("Resources/8bit.wav");
            sp.Play();

            //массив пятнашек
            string[] data = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", " " };

            //Перемешивание массива 
            var random = new Random(DateTime.Now.Millisecond);
            data = data.OrderBy(x => random.Next()).ToArray();
            

            //Кнопки скрыты, пока не будет нажата кнопка "Начать"   
            for (int i = 0; i< data.Length;i++)
            {
                Button ButHide = (Button)this.FindName("Button" + Convert.ToString(i + 1));
                ButHide.Visibility = Visibility.Hidden;
            }
            
            //Все объекты скрыты, пока не будет нажата кнопка старт
            label.Visibility = Visibility.Hidden;
            kolvo.Visibility = Visibility.Hidden;
            label1.Visibility = Visibility.Hidden;

            ButVis(data);//Пустая ячейка

            // Запись в кнопки перемешаного массивa
            for (int i = 0; i < data.Length; i++)
            {
                Button btn = (Button)this.FindName("Button" + Convert.ToString(i + 1));
                btn.Content = data[i];
            }
            
        }
        
        /// <summary>
        /// Свап ячеек
        /// </summary>
        /// <param name="a">Кнопка1</param>
        /// <param name="b">Кнопка2</param>
        public void swap(Button a, Button b) 
        {
            string temp;
            temp = Convert.ToString(a.Content);
            a.Content = Convert.ToString(b.Content);
            b.Content = Convert.ToString(temp);

            //Если кнопка пустая то скрыть ее
            if (Convert.ToString(a.Content) == " ")
            {
                a.Visibility = Visibility.Hidden;
            }
            else a.Visibility = Visibility.Visible;
            if (Convert.ToString(b.Content) == " ")
            {
                b.Visibility = Visibility.Hidden;
            }
            else b.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Сдвиги ячеек для каждой из 16ти кнопок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToString(Button2.Content) == " ")
            {
                swap(Button1, Button2);
            }
            else if (Convert.ToString(Button3.Content) == " ")
            {
                swap(Button2, Button3);
                swap(Button1, Button2);
            }
            else if (Convert.ToString(Button4.Content) == " ")
            {
                swap(Button3, Button4);
                swap(Button2, Button3);
                swap(Button1, Button2);
            }
            else if (Convert.ToString(Button5.Content) == " ")
            {
                swap(Button1, Button5);
            }
            else if (Convert.ToString(Button9.Content) == " ")
            {
                swap(Button9, Button5);
                swap(Button5, Button1);
            }
            else if (Convert.ToString(Button13.Content) == " ")
            {
                swap(Button13, Button9);
                swap(Button9, Button5);
                swap(Button5, Button1);
            }
            label1.Content = Convert.ToString(st++);
        }
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToString(Button1.Content) == " ")
            {
                swap(Button2, Button1);
            }
            else if (Convert.ToString(Button3.Content) == " ")
            {
                swap(Button2, Button3);
            }
            else if (Convert.ToString(Button6.Content) == " ")
            {
                swap(Button2, Button6);
            }
            else if (Convert.ToString(Button4.Content) == " ")
            {
                swap(Button4, Button3);
                swap(Button3, Button2);
            }
            else if (Convert.ToString(Button10.Content) == " ")
            {
                swap(Button10, Button6);
                swap(Button6, Button2);
            }
            else if (Convert.ToString(Button14.Content) == " ")
            {
                swap(Button14, Button10);
                swap(Button10, Button6);
                swap(Button6, Button2);
            }
            label1.Content = Convert.ToString(st++);;
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToString(Button2.Content) == " ")
            {
                swap(Button3, Button2);
            }
            else if (Convert.ToString(Button4.Content) == " ")
            {
                swap(Button3, Button4);
            }
            else if (Convert.ToString(Button7.Content) == " ")
            {
                swap(Button3, Button7);
            }
            if (Convert.ToString(Button1.Content) == " ")
            {
                swap(Button1, Button2);
                swap(Button2, Button3);
            }
            if (Convert.ToString(Button11.Content) == " ")
            {
                swap(Button11, Button7);
                swap(Button7, Button3);
            }
            if (Convert.ToString(Button15.Content) == " ")
            {
                swap(Button15, Button11);
                swap(Button11, Button7);
                swap(Button7, Button3);
            }
            label1.Content = Convert.ToString(st++);;
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToString(Button3.Content) == " ")
            {
                swap(Button4, Button3);
            }
            else if (Convert.ToString(Button8.Content) == " ")
            {
                swap(Button4, Button8);
            }
            else if (Convert.ToString(Button2.Content) == " ")
            {
                swap(Button2, Button3);
                swap(Button3, Button4);
            }
            else if (Convert.ToString(Button1.Content) == " ")
            {
                swap(Button1, Button2);
                swap(Button2, Button3);
                swap(Button3, Button4);
            }
            else if (Convert.ToString(Button12.Content) == " ")
            {
                swap(Button12, Button8);
                swap(Button8, Button4);
            }
            else if (Convert.ToString(Button16.Content) == " ")
            {
                swap(Button16, Button12);
                swap(Button12, Button8);
                swap(Button8, Button4);
            }
            label1.Content = Convert.ToString(st++);;
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToString(Button1.Content) == " ")
            {
                swap(Button5, Button1);
            }
            else if (Convert.ToString(Button6.Content) == " ")
            {
                swap(Button5, Button6);
            }
            else if (Convert.ToString(Button9.Content) == " ")
            {
                swap(Button5, Button9);
            }
            else if (Convert.ToString(Button13.Content) == " ")
            {
                swap(Button13, Button9);
                swap(Button9, Button5);
            }
            else if (Convert.ToString(Button7.Content) == " ")
            {
                swap(Button7, Button6);
                swap(Button6, Button5);
            }
            else if (Convert.ToString(Button8.Content) == " ")
            {
                swap(Button8, Button7);
                swap(Button7, Button6);
                swap(Button6, Button5);
            }
            label1.Content = Convert.ToString(st++);;
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToString(Button2.Content) == " ")
            {
                swap(Button6, Button2);
            }
            else if (Convert.ToString(Button7.Content) == " ")
            {
                swap(Button6, Button7);
            }
            else if (Convert.ToString(Button10.Content) == " ")
            {
                swap(Button6, Button10);
            }
            else if(Convert.ToString(Button5.Content) == " ")
            {
                swap(Button6, Button5);
            }
            else if (Convert.ToString(Button8.Content) == " ")
            {
                swap(Button8, Button7);
                swap(Button7, Button6);
            }
            else if (Convert.ToString(Button14.Content) == " ")
            {
                swap(Button14, Button10);
                swap(Button10, Button6);
            }
            label1.Content = Convert.ToString(st++);;
        }

        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToString(Button3.Content) == " ")
            {
                swap(Button7, Button3);
            }
            else if (Convert.ToString(Button8.Content) == " ")
            {
                swap(Button7, Button8);
            }
            else if (Convert.ToString(Button11.Content) == " ")
            {
                swap(Button7, Button11);
            }
            else if (Convert.ToString(Button6.Content) == " ")
            {
                swap(Button7, Button6);
            }
            else if (Convert.ToString(Button5.Content) == " ")
            {
                swap(Button5, Button6);
                swap(Button6, Button7);
            }
            else if (Convert.ToString(Button15.Content) == " ")
            {
                swap(Button15, Button11);
                swap(Button11, Button7);
            }
            label1.Content = Convert.ToString(st++);;
        }

        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToString(Button4.Content) == " ")
            {
                swap(Button8, Button4);
            }
            else if (Convert.ToString(Button7.Content) == " ")
            {
                swap(Button8, Button7);
            }
            else if (Convert.ToString(Button12.Content) == " ")
            {
                swap(Button8, Button12);
            }
            else if (Convert.ToString(Button16.Content) == " ")
            {
                swap(Button16, Button12);
                swap(Button12, Button8);
            }
            else if (Convert.ToString(Button6.Content) == " ")
            {
                swap(Button6, Button7);
                swap(Button7, Button8);
            }
            else if (Convert.ToString(Button5.Content) == " ")
            {
                swap(Button5, Button6);
                swap(Button6, Button7);
                swap(Button7, Button8);
            }
            label1.Content = Convert.ToString(st++);;
        }

        private void Button9_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToString(Button5.Content) == " ")
            {
                swap(Button9, Button5);
            }
            else if (Convert.ToString(Button10.Content) == " ")
            {
                swap(Button9, Button10);
            }
            else if (Convert.ToString(Button13.Content) == " ")
            {
                swap(Button9, Button13);
            }
            else if (Convert.ToString(Button1.Content) == " ")
            {
                swap(Button1, Button5);
                swap(Button5, Button9);
            }
            else if (Convert.ToString(Button11.Content) == " ")
            {
                swap(Button11, Button10);
                swap(Button10, Button9);
            }
            else if (Convert.ToString(Button12.Content) == " ")
            {
                swap(Button12, Button11);
                swap(Button11, Button10);
                swap(Button10, Button9);
            }
            label1.Content = Convert.ToString(st++);;

        }

        private void Button10_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToString(Button6.Content) == " ")
            {
                swap(Button10, Button6);
            }
            else if (Convert.ToString(Button11.Content) == " ")
            {
                swap(Button10, Button11);
            }
            else if (Convert.ToString(Button14.Content) == " ")
            {
                swap(Button10, Button14);
            }
            else if (Convert.ToString(Button9.Content) == " ")
            {
                swap(Button10, Button9);
            }
            else if (Convert.ToString(Button2.Content) == " ")
            {
                swap(Button2, Button6);
                swap(Button6, Button10);
            }
            else if (Convert.ToString(Button12.Content) == " ")
            {
                swap(Button12, Button11);
                swap(Button11, Button10);
            }
            label1.Content = Convert.ToString(st++);;
        }

        private void Button11_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToString(Button7.Content) == " ")
            {
                swap(Button11, Button7);
            }
            else if (Convert.ToString(Button12.Content) == " ")
            {
                swap(Button11, Button12);
            }
            else if (Convert.ToString(Button15.Content) == " ")
            {
                swap(Button11, Button15);
            }
            else if (Convert.ToString(Button10.Content) == " ")
            {
                swap(Button11, Button10);
            }
            else if (Convert.ToString(Button9.Content) == " ")
            {
                swap(Button9, Button10);
                swap(Button10, Button11);
            }
            else if (Convert.ToString(Button3.Content) == " ")
            {
                swap(Button3, Button7);
                swap(Button7, Button11);
            }
            label1.Content = Convert.ToString(st++);;
        }

        private void Button12_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToString(Button8.Content) == " ")
            {
                swap(Button12, Button8);
            }
            else if (Convert.ToString(Button11.Content) == " ")
            {
                swap(Button12, Button11);
            }
            else if (Convert.ToString(Button16.Content) == " ")
            {
                swap(Button12, Button16);
            }
            else if (Convert.ToString(Button4.Content) == " ")
            {
                swap(Button4, Button8);
                swap(Button8, Button12);
            }
            else if (Convert.ToString(Button9.Content) == " ")
            {
                swap(Button9, Button10);
                swap(Button10, Button11);
                swap(Button11, Button12);
            }
            else if (Convert.ToString(Button10.Content) == " ")
            {
                swap(Button10, Button11);
                swap(Button11, Button12);
            }
            label1.Content = Convert.ToString(st++);;
        }

        private void Button13_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToString(Button9.Content) == " ")
            {
                swap(Button13, Button9);
            }
            else if (Convert.ToString(Button14.Content) == " ")
            {
                swap(Button13, Button14);
            }
            else if (Convert.ToString(Button5.Content) == " ")
            {
                swap(Button5, Button9);
                swap(Button9, Button13);
            }
            else if (Convert.ToString(Button1.Content) == " ")
            {
                swap(Button1, Button5);
                swap(Button5, Button9);
                swap(Button9, Button13);
            }
            else if (Convert.ToString(Button15.Content) == " ")
            {
                swap(Button15, Button14);
                swap(Button14, Button13);
            }
            else if (Convert.ToString(Button16.Content) == " ")
            {
                swap(Button16, Button15);
                swap(Button15, Button14);
                swap(Button14, Button13);
            }
            label1.Content = Convert.ToString(st++);;
        }

        private void Button14_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToString(Button13.Content) == " ")
            {
                swap(Button14, Button13);
            }
            else if (Convert.ToString(Button10.Content) == " ")
            {
                swap(Button14, Button10);
            }
            else if (Convert.ToString(Button15.Content) == " ")
            {
                swap(Button14, Button15);
            }
            else if (Convert.ToString(Button2.Content) == " ")
            {
                swap(Button2, Button6);
                swap(Button6, Button10);
                swap(Button10, Button14);
            }
            else if (Convert.ToString(Button6.Content) == " ")
            {
                swap(Button6, Button10);
                swap(Button10, Button14);
            }
            else if (Convert.ToString(Button16.Content) == " ")
            {
                swap(Button16, Button15);
                swap(Button15, Button14);
            }
            label1.Content = Convert.ToString(st++);;
        }
        private void Button15_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToString(Button14.Content) == " ")
            {
                swap(Button15, Button14);
            }
            else if (Convert.ToString(Button11.Content) == " ")
            {
                swap(Button15, Button11);
            }
            else if (Convert.ToString(Button16.Content) == " ")
            {
                swap(Button15, Button16);
            }
            else if (Convert.ToString(Button3.Content) == " ")
            {
                swap(Button3, Button7);
                swap(Button7, Button11);
                swap(Button11, Button15);
            }
            else if (Convert.ToString(Button7.Content) == " ")
            {
                swap(Button7, Button11);
                swap(Button11, Button15);
            }
            else if (Convert.ToString(Button13.Content) == " ")
            {
                swap(Button13, Button14);
                swap(Button14, Button15);
            }
            label1.Content = Convert.ToString(st++);;
        }

        private void Button16_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToString(Button12.Content) == " ")
            {
                swap(Button16, Button12);
            }
            else if (Convert.ToString(Button15.Content) == " ")
            {
                swap(Button16, Button15);
            }
            else if (Convert.ToString(Button8.Content) == " ")
            {
                swap(Button8, Button12);
                swap(Button12, Button16);
            }
            else if (Convert.ToString(Button14.Content) == " ")
            {
                swap(Button14, Button15);
                swap(Button15, Button16);
            }
            else if (Convert.ToString(Button13.Content) == " ")
            {
                swap(Button13, Button14);
                swap(Button14, Button15);
                swap(Button15, Button16);
            }
            else if (Convert.ToString(Button4.Content) == " ")
            {
                swap(Button4, Button8);
                swap(Button8, Button12);
                swap(Button12, Button16);
            }
            label1.Content = Convert.ToString(st++);;
        }

        /// <summary>
        /// Функция скрывает пустую кнопку
        /// </summary>
        /// <param name="data">Массив контента кнопок</param>
        private void ButVis(string[] data)
        {
            int num = Array.IndexOf(data, " ") - 1;
            Button button = (Button)this.FindName("Button" + Convert.ToString(num));
            button.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Поиск пустой кнопки и ее скрытие
        /// </summary>
        private void SearchZ()
        {

            for (int i = 1; i < 17; i++)
            {
                Button btn = (Button)this.FindName("Button" + Convert.ToString(i));
                if (Convert.ToString(btn.Content) == " ")
                    btn.Visibility = Visibility.Hidden;
                else
                    btn.Visibility = Visibility.Visible;
            }
            

        }

        /// <summary>
        /// Клик по кнопке старт
        /// Отображает поле кнопок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_Start(object sender, RoutedEventArgs e)
        {

            SearchZ();

            label.Visibility = Visibility.Visible;
            kolvo.Visibility = Visibility.Visible;
            label1.Visibility = Visibility.Visible;
            SB.Visibility = Visibility.Hidden;
            StartLabel.Visibility = Visibility.Hidden;
           
        }

      
    }

}
