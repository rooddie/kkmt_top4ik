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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace kalk1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            answLabel.Content += "1";
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            answLabel.Content += "2";
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            answLabel.Content += "3";
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            answLabel.Content += "4";
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            answLabel.Content += "5";
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            answLabel.Content += "6";
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            answLabel.Content += "7";
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            answLabel.Content += "8";
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            answLabel.Content += "9";
        }

        private void button0_Click(object sender, RoutedEventArgs e)
        {
            answLabel.Content += "0";
        }

        // Считает корень числа, при условии что в строке только число, без действий
        private void korenButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (Convert.ToString(answLabel.Content) != "")
            {
                if (Convert.ToString(answLabel.Content).Contains("*") == false && Convert.ToString(answLabel.Content).Contains("/") == false && Convert.ToString(answLabel.Content).Contains("+") == false && Convert.ToString(answLabel.Content).Contains("-") == false )
                    answLabel.Content = Math.Sqrt(Convert.ToDouble(answLabel.Content));
            }

        }

        private void plusButton_Click(object sender, RoutedEventArgs e)
        {
            answLabel.Content += "+";
        }

        private void minusButton_Click(object sender, RoutedEventArgs e)
        {
            answLabel.Content += "-";
        }

        private void proizButton_Click(object sender, RoutedEventArgs e)
        {
            answLabel.Content += "*";
        }

        private void delButton_Click(object sender, RoutedEventArgs e)
        {
            answLabel.Content += "/";
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            answLabel.Content = "";
        }

        private void answButton_Click(object sender, RoutedEventArgs e)
        {
            string st = Convert.ToString(answLabel.Content);
            //Строка разделяется на 2 числа и действие между ними
            string ch1 = ""; 
            string ch2 = "";
            string deistv = "";
            int stLen = st.Length;
            //Если знак действия последний или его впринципе нет, то кнопка равно не сработает
            if (st[st.Length - 1] != '*' || st[st.Length - 1] != '/' || st[st.Length - 1] != '+' || st[st.Length - 1] != '-' 
                && st.Contains('*') == true || st.Contains('/') == true || st.Contains('+') == true || st.Contains('+') == true)
                {
                    for (int i = 0; i < st.Length; i++)
                    {
                        ch1 += st[i];
                        if (st[i] == '*' || st[i] == '/' || st[i] == '+' || st[i] == '-')
                        {
                            deistv += st[i];
    
                            ch2 += ch1;
                           
                            ch1 = "";
                        }

                    }
                    string chh2 = "";
                    //Удаление действия из строки 2
                    for(int i = 0; i < ch2.Length-1; i++)
                    {
                        chh2 += ch2[i];
                    }
                    
                    double result1 = 0.0;
                    double result2 = 0.0;
                    if (deistv == "*")
                    {
                        result1 = Convert.ToDouble(ch1);
                        result2 = Convert.ToDouble(chh2);
                        answLabel.Content = Convert.ToString(result2 * result1);
                    }
                    else if (deistv == "/")
                    {
                        result1 = Convert.ToDouble(ch1);
                        result2 = Convert.ToDouble(chh2);
                        answLabel.Content = Convert.ToString(result2 / result1);
                    }
                    else if (deistv == "+")
                    {
                        result1 = Convert.ToDouble(ch1);
                        result2 = Convert.ToDouble(chh2);
                        answLabel.Content = Convert.ToString(result2 + result1);
                    }
                    else if (deistv == "-")
                    {
                        result1 = Convert.ToDouble(ch1);
                        result2 = Convert.ToDouble(chh2);
                        answLabel.Content = Convert.ToString(result2 - result1);
                    }



            }


        }
    }
}
