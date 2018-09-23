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

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            createPos();

        }

        public void createPos()
        {
            string[] data = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
           
            int[,] array = new int[9, 9];
            int n = 3;
            for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < 9; i++)
                {
                    array[j, i] = (i * n + i / n + j) % (n * n) + 1;
                }
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {

                    Button button = new Button();
                    button.Name = "btn" + Convert.ToString(i+j);
                    button.Content = Convert.ToString(array[i,j]);
            
                    button.Background = new SolidColorBrush(Colors.Black) { Opacity = 0.2 };
                    gridBut.Children.Add(button);
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                }
            }



            
        }

    }
}
