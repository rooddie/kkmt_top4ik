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
        public MainWindow()
        {
            InitializeComponent();
            createPos();

        }

        //Функция создания поля
        public void createPos()
        {
            Random rnd = new Random();//Рандом

            int posSwap =rnd.Next(1, 3); // Случайное число для свапа элементов swapMas
           
            int n = 3;//Длина маленьких квадратов

            //Массив заполнения судоку, не перемешанными элементами
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    array[i, j] = (i * n + i / n + j) % (n * n) + 1;
                }
            }

            //Перемешивание swapMas, для перемешивания столбиков и столбцов
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
                 swap_col();
                 swap_row();
             }
            transp();

            //Процесс создания поля судоку
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (j == 0||j == 3||j== 6)
                    {
                        int elCount = 2;// rnd.Next(1,3); //Сложность судоку. Кол-во цифр в одной мини строке
                        if (elCount == 1)
                        {
                            int hiddEl = rnd.Next(0, 3);//Позиция пробела в строке
                            if (hiddEl == 0)
                            {
                                TextBox textBox = new TextBox();
                                
                                gridAdd1(i, j, 0, false, textBox);
                                TextBox textBox1 = new TextBox();
                                gridAdd1(i, j, 1, true, textBox1);
                                TextBox textBox2 = new TextBox();
                                gridAdd1(i, j, 2, true, textBox2);

                            }
                            
                            else if (hiddEl == 1)
                            {
                                TextBox textBox = new TextBox();
                                gridAdd1(i, j, 0, true, textBox);
                                TextBox textBox1 = new TextBox();
                                gridAdd1(i, j, 1, false, textBox1);
                                TextBox textBox2 = new TextBox();
                                gridAdd1(i, j, 2, true, textBox2);
 
                            }

                            else if (hiddEl == 2)
                            {
                                TextBox textBox = new TextBox();
                                gridAdd1(i, j, 0, true, textBox);
                                TextBox textBox1 = new TextBox();
                                gridAdd1(i, j, 1, true, textBox1);
                                TextBox textBox2 = new TextBox();
                                gridAdd1(i, j, 2, false, textBox2);
                            }
                        }
                        else if(elCount == 2)
                        {
                            int hiddEl2 = rnd.Next(0, 3);
                            if (hiddEl2 == 0)
                            {
                                TextBox textBox = new TextBox();
                                gridAdd1(i, j, 0, false, textBox);
                                TextBox textBox1 = new TextBox();
                                gridAdd1(i, j, 1, false, textBox1);
                                TextBox textBox2 = new TextBox();
                                gridAdd1(i, j, 2, true, textBox2);

                            }
                            else if(hiddEl2 == 1)
                            {
                                TextBox textBox = new TextBox();
                                gridAdd1(i, j, 0, false, textBox);
                                TextBox textBox1 = new TextBox();
                                gridAdd1(i, j, 1, true, textBox1);
                                TextBox textBox2 = new TextBox();
                                gridAdd1(i, j, 2, false, textBox2);
                            }

                            else if(hiddEl2 == 2)
                            {
                                TextBox textBox = new TextBox();
                                gridAdd1(i, j, 0, false, textBox);
                                TextBox textBox1 = new TextBox();
                                gridAdd1(i, j, 1, false, textBox1);
                                TextBox textBox2 = new TextBox();
                                gridAdd1(i, j, 2, true, textBox2);
                            }
                            
                        }
                        
                    }
                    //TextBox textBox = new TextBox();
                    //textBox.Name = "btn" + Convert.ToString(i + j);
                    //textBox.Text = Convert.ToString(array[i,j]);



                    
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
        public void swap_row()
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
        public void swap_col()
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

        public void gridAdd1(int i, int j, int nextEl, bool chek, TextBox tb)
        {
            tb.Name = "btn" + i.ToString() + (j).ToString();
            if (chek == true)
            {
                tb.Text = tb.Text = array[i, j + nextEl].ToString(); ;
            }
            else tb.Text = "";
            tb.Background = new SolidColorBrush(Colors.Black) { Opacity = 0.2 };
            

            //Добавление кнопки к сетке
            gridBut.Children.Add(tb);
            Grid.SetRow(tb, i);
            Grid.SetColumn(tb, j+nextEl);
        }

    }
}

/*
if (elCount == 1)
                    {
                        int hiddEl1 = rnd.Next(0, 3);
                        if (hiddEl1 == 0)
                        {
                            textBox.Name = "btn" + i.ToString() + (j + hiddEl1).ToString();
                            textBox.Text = "";
                        }
                        else if (hiddEl1 == 1)
                        {
                            textBox.Name = "btn" + i.ToString() + (j + hiddEl1).ToString();
                            textBox.Text = "";
                        }
                        else if (hiddEl1 == 2)
                        {
                            textBox.Name = "btn" + i.ToString() + (j + hiddEl1).ToString();
                            textBox.Text = "";
                        }
                    }

                    else if (elCount == 2)
                    {
                        int hiddEl2 = rnd.Next(0, 3);
                        if (hiddEl2 == 0)
                        {
                            textBox.Name = "btn" + i.ToString() + (j + 0).ToString();
                            textBox.Text = "";
                            textBox.Name = "btn" + i.ToString() + (j + 1).ToString();
                            textBox.Text = "";
                        }
                        else if (hiddEl2 == 1)
                        {
                            textBox.Name = "btn" + i.ToString() + (j + 1).ToString();
                            textBox.Text = "";
                            textBox.Name = "btn" + i.ToString() + (j + 2).ToString();
                            textBox.Text = "";
                        }
                        else if (hiddEl2 == 2)
                        {
                            textBox.Name = "btn" + i.ToString() + (j + 0).ToString();
                            textBox.Text = "";
                            textBox.Name = "btn" + i.ToString() + (j + 2).ToString();
                            textBox.Text = "";
                        }
                    } 
 */
