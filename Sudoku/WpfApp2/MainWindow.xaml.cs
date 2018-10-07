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
        public int[,] timeMas = new int[9, 9]; //Временный массив для хранения промежуточных перестановок
        public int[] swapMas = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };//Массив, элементы которого отвечают за перестановки
        public int[,] array = new int[9, 9];//Основной массив
        public string[,] endGameMas = new string[9, 9];//


        public MainWindow()
        {
            InitializeComponent();
            listDigit.Visibility = Visibility.Hidden;
            Application.Current.MainWindow.Height = 421;
            createDigits();//Заполнение comboBox
            winGame.Visibility = Visibility.Hidden;
        }

        //Функция создания поля
        public void createPos()
        {
            listDigit.Visibility = Visibility.Visible;
            Random rnd = new Random();//Рандом
            //Next - задает диапазон от 1 до 3, 3 не вкючительно
            int posSwap = rnd.Next(1, 3); // Случайное число для свапа элементов swapMas

            int n = 3;//Длина маленьких квадратов судоку

            //Массив заполнения судоку, не перемешанными элементами
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    array[i, j] = (i * n + i / n + j) % (n * n) + 1;
                }
            }

            //Перемешивание swapMas.
            //На основе этого массива, будут перемешаны столбики и столбцы матрицы
            for (int i = 0; i < 9; i += 3)
            {
                if (posSwap == 1)
                {
                    int temp = swapMas[i + 1];
                    swapMas[i + 1] = swapMas[i];
                    swapMas[i] = temp;
                }
                else if (posSwap == 2)
                {
                    int temp = swapMas[i + 2];
                    swapMas[i + 2] = swapMas[i];
                    swapMas[i] = temp;
                }
            }


            int swapCount = rnd.Next(10, 25);//Количество перемешиваний

            //Перемешивание массива
            transp();
            for (int i = 0; i < swapCount; i++)
            {
                swapCol();
                swapRow();
            }
            transp();
            for (int i = 0; i < 9; i++)
            {
                tLabel.Content += "\n";
                for (int j = 0; j < 9; j++)
                {
                    tLabel.Content += array[i, j].ToString();
                }
            }
            //Процесс создания поля судоку
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (j == 0 || j == 3 || j == 6)
                    {
                        int elCount = 1;// rnd.Next(1,3); //Сложность судоку. Кол-во пустых ячеек в строке квадрата 3х3
                        if (elCount == 1)
                        {
                            int hiddEl = rnd.Next(0, 3);//Позиция пробела в строке
                            if (hiddEl == 0)
                            {
                                Button sudokuElem = new Button();
                                gridAdd(i, j, 0, false, sudokuElem);
                                Button sudokuElem1 = new Button();
                                gridAdd(i, j, 1, true, sudokuElem1);
                                Button sudokuElem2 = new Button();
                                gridAdd(i, j, 2, true, sudokuElem2);
                            }

                            else if (hiddEl == 1)
                            {
                                Button sudokuElem = new Button();
                                gridAdd(i, j, 0, true, sudokuElem);
                                Button sudokuElem1 = new Button();
                                gridAdd(i, j, 1, false, sudokuElem1);
                                Button sudokuElem2 = new Button();
                                gridAdd(i, j, 2, true, sudokuElem2);
                            }

                            else if (hiddEl == 2)
                            {
                                Button sudokuElem = new Button();
                                gridAdd(i, j, 0, true, sudokuElem);
                                Button sudokuElem1 = new Button();
                                gridAdd(i, j, 1, true, sudokuElem1);
                                Button sudokuElem2 = new Button();
                                gridAdd(i, j, 2, false, sudokuElem2);
                            }
                        }
                        else if (elCount == 2)
                        {
                            int hiddEl2 = rnd.Next(0, 3);
                            if (hiddEl2 == 0)
                            {
                                Button sudokuElem = new Button();
                                gridAdd(i, j, 0, true, sudokuElem);
                                Button sudokuElem1 = new Button();
                                gridAdd(i, j, 1, false, sudokuElem1);
                                Button sudokuElem2 = new Button();
                                gridAdd(i, j, 2, false, sudokuElem2);
                            }
                            else if (hiddEl2 == 1)
                            {
                                Button sudokuElem = new Button();
                                gridAdd(i, j, 0, false, sudokuElem);
                                Button sudokuElem1 = new Button();
                                gridAdd(i, j, 1, true, sudokuElem1);
                                Button sudokuElem2 = new Button();
                                gridAdd(i, j, 2, false, sudokuElem2);
                            }

                            else if (hiddEl2 == 2)
                            {
                                Button sudokuElem = new Button();
                                gridAdd(i, j, 0, false, sudokuElem);
                                Button sudokuElem1 = new Button();
                                gridAdd(i, j, 1, false, sudokuElem1);
                                Button sudokuElem2 = new Button();
                                gridAdd(i, j, 2, true, sudokuElem2);
                            }

                        }

                    }
                    //Button sudokuElem = new Button();
                    //sudokuElem.Name = "btn" + Convert.ToString(i + j);
                    //sudokuElem.Text = Convert.ToString(array[i,j]);
                }

            }

        }

        //Функция транспонирования матрицы
        public void transp()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int a = array[j, i];
                    timeMas[i, j] = a;
                }
            }
            //Присваивание новых данных основному массиву для вывода
            for (int i = 0; i < 9; i++)
            {

                for (int j = 0; j < 9; j++)
                {
                    array[i, j] = timeMas[i, j];

                }
            }
        }

        //Функция свапа строк матрицы
        public void swapRow()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    array[i, j] = timeMas[i, j];

                }
            }
            //Присваивание новых данных основному массиву для вывода
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int a = array[swapMas[i], j];
                    timeMas[i, j] = a;
                }
            }
        }

        //Функция свапа столбцов матрицы
        public void swapCol()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int a = array[swapMas[j], i];
                    timeMas[i, j] = a;
                }
            }
            //Присваивание новых данных основному массиву для вывода
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    array[i, j] = timeMas[i, j];

                }
            }
        }

        //Функция добавления в сетку готового массива
        public void gridAdd(int i, int j, int nextEl, bool chek, Button but)
        {
            //Создание кнопки
            but.Name = "btn" + i.ToString() + (j).ToString();

            if (chek == true)
            {
                but.Content = array[i, j + nextEl].ToString();
                endGameMas[i, j + nextEl] = array[i, j + nextEl].ToString();
            }
            else
            {
                but.Content = "";
                endGameMas[i, j + nextEl] = " ";

                but.Foreground = new SolidColorBrush(Colors.Red);
                but.Click += delegate
                {
                    addDigit(but);
                    endGameMas[i, j + nextEl] = listDigit.Text;

                    //Если массивы равны, наступает конец игры
                    if (comparsionMas(endGameMas, array) == true)
                    {
                        listDigit.Visibility = Visibility.Hidden;
                        Application.Current.MainWindow.Height = 421;
                        winGame.Visibility = Visibility.Visible;
                    }
                };


            }
            but.Background = new SolidColorBrush() { Opacity = 0.0000001 };
            //Добавление кнопки к сетке
            gridBut.Children.Add(but);
            Grid.SetRow(but, i);
            Grid.SetColumn(but, j + nextEl);


        }

        //Функция клика на кнопку Старт
        //Название игры и кнопка скрываются
        //Создается поле судоку
        private void startGame(object sender, RoutedEventArgs e)
        {
            nameGame.Visibility = Visibility.Hidden;
            startButton.Visibility = Visibility.Hidden;
            Application.Current.MainWindow.Height = 458;
            createPos();
        }

        //Функция добавления в пустую ячейку выбранного числа в comboBox
        private void addDigit(Button but)
        {
            but.Content = listDigit.Text;
        }

        //Функция клика по кнопке рестарт
        private void restartCkick(object sender, RoutedEventArgs e)
        {
            //Поиск всех кнопок в Grid
            foreach (DependencyObject button in gridBut.Children)
            {
                if (button.GetType().ToString() == "System.Windows.Controls.Button")
                {
                    ((Button)button).Content = "";

                }
                //Контент рестарта очищается, так как это тоже кнопка
                restartButton.Content = "Restart";
            }
            createPos();
        }

        //Заполнение comboBox 
        private void createDigits()
        {
            listDigit.Visibility = Visibility.Visible;
            for (int i = 1; i <= 9; i++)
            {
                listDigit.Items.Add(i.ToString());
            }
        }

        //Функция сравнения массивов
        private bool comparsionMas(string[,] arr1, int[,] arr2)
        {
            if (arr1.Length == arr2.Length)
            {
                for (int i = 0; i < 9; i++)
                { 
                    for (int j = 0; j < 9;j++)
                    if (arr1[i,j].ToString() != arr2[i,j].ToString())
                    {
                        return false;
                    }
                }
                return true;
            }
            else return false;
        }


    }
}

/*
if (elCount == 1)
                    {
                        int hiddEl1 = rnd.Next(0, 3);
                        if (hiddEl1 == 0)
                        {
                            sudokuElem.Name = "btn" + i.ToString() + (j + hiddEl1).ToString();
                            sudokuElem.Text = "";
                        }
                        else if (hiddEl1 == 1)
                        {
                            sudokuElem.Name = "btn" + i.ToString() + (j + hiddEl1).ToString();
                            sudokuElem.Text = "";
                        }
                        else if (hiddEl1 == 2)
                        {
                            sudokuElem.Name = "btn" + i.ToString() + (j + hiddEl1).ToString();
                            sudokuElem.Text = "";
                        }
                    }

                    else if (elCount == 2)
                    {
                        int hiddEl2 = rnd.Next(0, 3);
                        if (hiddEl2 == 0)
                        {
                            sudokuElem.Name = "btn" + i.ToString() + (j + 0).ToString();
                            sudokuElem.Text = "";
                            sudokuElem.Name = "btn" + i.ToString() + (j + 1).ToString();
                            sudokuElem.Text = "";
                        }
                        else if (hiddEl2 == 1)
                        {
                            sudokuElem.Name = "btn" + i.ToString() + (j + 1).ToString();
                            sudokuElem.Text = "";
                            sudokuElem.Name = "btn" + i.ToString() + (j + 2).ToString();
                            sudokuElem.Text = "";
                        }
                        else if (hiddEl2 == 2)
                        {
                            sudokuElem.Name = "btn" + i.ToString() + (j + 0).ToString();
                            sudokuElem.Text = "";
                            sudokuElem.Name = "btn" + i.ToString() + (j + 2).ToString();
                            sudokuElem.Text = "";
                        }
                    } 
 */
