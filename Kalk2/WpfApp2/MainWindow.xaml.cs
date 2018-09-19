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
        }


        private void button_Click(object sender, RoutedEventArgs e)
        { 
            

            string stroka = textBox.Text+' ';
            int j = 0;
            List<string> masCh = new List<string>(); //массив чисел в строке
            List<char> masZn = new List<char>();//массив действий в строке
            List<string> vr = new List<string>();

            for (int i = 0; i < stroka.Length; i++)
            {
                if (stroka[i] == '*' || stroka[i] == '/' || stroka[i] == '+' || stroka[i] == '-' || stroka[i] == ' ')
                {
                    masZn.Add(stroka[i]);//Добавляет элемент в массив действий
                    masCh.Add(stroka.Substring(j, i - j));//Добавляет элемент в массив чисел
                    j = i + 1;
                }
            }

           

            masZn.RemoveAt(masZn.Count - 1);
            //int k = 0;
            double sum = 0;
            double up = 0;
           
            ///label.Content = Convert.ToString(sum);
            
           for(int k = 0; k<=masCh.Count; k++)
            {
                if (masZn.Contains('*') == true || masZn.Contains('/') == true)
                {
                    if(masZn[k] == '*')
                    {
                        up = Convert.ToDouble(masCh[k]) * Convert.ToDouble(masCh[k + 1]);

                        masCh[k] = Convert.ToString(up);
                        masCh.RemoveAt(k+1);
                        masZn.RemoveAt(k);
                        sum += up;
                        k = 0;
                        
                    }
                
                }
                else if (masZn.Contains('*') != true || masZn.Contains('/') != true)
                {
                    if (masZn[k] == '+')
                    {
                        up = Convert.ToDouble(masCh[k]) + Convert.ToDouble(masCh[k + 1]);

                        masCh[k] = Convert.ToString(up);
                        masCh.RemoveAt(k+1);
                        masZn.RemoveAt(k);
                        sum += up;
                        k = 0;
                    }
                }
            }
            label.Content = Convert.ToString(sum);
            /*
            while(masZn.Count >= 0)
             {

                 if (masZn.Contains('*') == true || masZn.Contains('/') == true)
                 {
                     if(masZn[k] == '*')
                     {
                         up = Convert.ToDouble(masCh[k]) * Convert.ToDouble(masCh[k + 1]);

                         masCh[k+1] = Convert.ToString(up);
                         masCh.RemoveAt(k);
                         masZn.RemoveAt(k);
                         ///label.Content = masCh;
                        ///label.Content += Convert.ToString(masZn[k]);
                         sum += up;
                         k++;
                        
                     }
                     */
                     
            }      
        }
}

