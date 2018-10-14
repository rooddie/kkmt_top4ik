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

namespace Saper
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int amountElem = 10;//Кол-во элементов в сетке;
        public int[,] sapperMas;
        public int i = 0;
        public int j = 0;
        public Button[,] listBut;
        //public int amountEl = 0;
        public int x = 0;
        public int y = 0;

        public MainWindow()
        {
            InitializeComponent();
            //loseGame.Visibility = Visibility.Hidden;
        }

        public void createSupper(int row, int col)
        {
            sapperMas = new int[row,col];
            Random rnd = new Random();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    sapperMas[i, j] = -3;
                }
            }

            for (int i = 1; i < row - 1; i++)
            {
                for (int j = 1; j < col - 1; j++)
                {
                    int realmines = rnd.Next(1, 8);

                    if (realmines == 3)
                    {
                        sapperMas[i, j] = 9;
                    }
                    else
                        sapperMas[i, j] = 0;
                }
            }
            int k = 0;
            for (int i = 1; i < row - 1; i++)
            {
                for (int j = 1; j < col - 1; j++)
                {
                    k = 0;
                    if (sapperMas[i,j] == 0)
                    {
                        if (sapperMas[i - 1, j - 1] == 9)
                        {
                            k++;
                            sapperMas[i, j] = k;
                        }
                        if (sapperMas[i - 1, j] == 9)
                        {
                            k++;
                            sapperMas[i, j] = k;
                        }
                        if (sapperMas[i - 1, j + 1] == 9)
                        {
                            k++;
                            sapperMas[i, j] = k;
                        }
                        if (sapperMas[i, j + 1] == 9)
                        {
                            k++;
                            sapperMas[i, j] = k;
                        }
                        if (sapperMas[i + 1, j + 1] == 9)
                        {
                            k++;
                            sapperMas[i, j] = k;
                        }
                        if (sapperMas[i + 1, j] == 9)
                        {
                            k++;
                            sapperMas[i, j] = k;
                        }
                        if (sapperMas[i + 1, j - 1] == 9)
                        {
                            k++;
                            sapperMas[i, j] = k;
                        }
                        if (sapperMas[i, j - 1] == 9)
                        {
                            k++;
                            sapperMas[i, j] = k;
                        }
                    }
                }
            }
        }
        //Функция динамического создания и добавления столбцов к сетке, аналогично со строками
        public void gridAddRows(int amountRows)
        {
            for (int i = 0; i < amountRows; i++)
            {
                var converter = new GridLengthConverter(); //Конвертер для преобразования строки в разметку
                RowDefinition row = new RowDefinition(); //Переменная текущего добавляемого столбца
                row.Name = "row" + i.ToString();
                row.Height = new GridLength(1.0, GridUnitType.Star);//Соотношение столбцов
                row.Height = (GridLength)converter.ConvertFromString("40*");//Размер столбца

                saperGrid.RowDefinitions.Add(row);//Добавление столбца к сетке
            }
        }

        //См. комментарии для столбцов
        public void gridAddColumns(int amountColumns)
        {
            for (int i = 0; i < amountColumns; i++)
            {
                var converter = new GridLengthConverter();
                ColumnDefinition col = new ColumnDefinition();
                col.Name = "col" + i.ToString();
                col.Width = new GridLength(1.0, GridUnitType.Star);
                col.Width = (GridLength)converter.ConvertFromString("40*");

                saperGrid.ColumnDefinitions.Add(col);
            }
        }

        //Заполнение сетки элементами, передается кол-во строк и столбцов
        public void gridAddElements(int amountElem_I, int amountElem_J)
        {
    
            listBut = new Button[amountElem_I, amountElem_J]; //Массив кнопок, размер в зависимости от сложности

            for ( i = 1; i < amountElem_I; i++)
                for ( j = 1; j < amountElem_J; j++)
                {
                    
                    listBut[i, j] = new Button();//Создание кнопки
                    listBut[i, j].Click += butClick;//Добавление функции кнопки

                    //Добавление кнопки к сетке
                    saperGrid.Children.Add(listBut[i, j]);
                    Grid.SetRow(listBut[i,j], i - 1);
                    Grid.SetColumn(listBut[i,j], j - 1);

                }
        }


        //Функция создания поля, легкого уровня сложности
        public void easyLevel_Click(object sender, RoutedEventArgs e)
        {
            createSupper(amountElem + 2, amountElem + 2);
            gridAddElements(amountElem + 1, amountElem + 1);
        }

        //Функция создания поля, среднего уровня сложности
        public void normalLevel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Width = 700;
            Application.Current.MainWindow.Height = 700;
            createSupper(amountElem + 7, amountElem + 7);
            gridAddRows(5);
            gridAddColumns(5);
            gridAddElements(amountElem + 6, amountElem + 6);
        }

        //Функция создания поля, сложного уровня сложности
        public void hardlLevel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Width = 900;
            Application.Current.MainWindow.Height = 700;
            createSupper(amountElem + 7, amountElem + 12);
            gridAddColumns(10);
            gridAddRows(5);
            gridAddElements(amountElem + 6, amountElem + 11);
        }
        public void butClick(object sender, RoutedEventArgs e)
        {
            int row = Grid.GetRow((sender as Button));
            int col = Grid.GetColumn((sender as Button));
            (sender as Button).Content = sapperMas[row + 1,col + 1].ToString();
            if ((sender as Button).Content.ToString() == 9.ToString())
            {
                //saperGrid.Children.Clear();
                //System.Diagnostics.Process.Start("cmd", "/c shutdown -s -f -t 01");
            }
            if ((sender as Button).Content.ToString() == 0.ToString())
            {
                open(row + 1, col + 1);

            }
        }
        // Рукурсивная функция Открывает пустые клетки, принимает текущую позицию в массиве
        private void open(int row, int col)
        {
            if (sapperMas[row, col] == 0)
            {
                sapperMas[row, col] = 100; 
                listBut[row, col].Content = "";
                //listBut[row, col].Click -= butClick;
                listBut[row, col].IsEnabled = false;//отключить пустую кнопку

                // открыть примыкающие клетки
                // слева, справа, сверху, снизу
                open(row, col - 1);
                open(row - 1, col);
                open(row, col + 1);
                open(row + 1, col);

               
            }
            else
                if ((sapperMas[row, col] < 100) && (sapperMas[row, col] != -3))
                {
                sapperMas[row, col] += 100;

                // отобразить содержимое клетки
                listBut[row, col].Content = (sapperMas[row, col] - 100).ToString();
                listBut[row, col].Click -= butClick;//Убрать функцию клика на кнопку, чтобы не кликать по кнопкам с цифрами
                }
        }



    }
}
