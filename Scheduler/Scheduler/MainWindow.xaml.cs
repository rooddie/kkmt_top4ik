using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Timers;
namespace Scheduler
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string file = "users.xml";
        public XmlDocument xDoc = new XmlDocument();
        public XmlElement xRoot;
        public string numXml = "";
        public string themeXml = "";
        public string textXml = "";
        public string datatimeXml = "";

        List<string> Numbers = new List<string>();
        List<string> Themes = new List<string>();
        List<string> Texts = new List<string>();
        List<string> DataTimes = new List<string>();

        public int n = 0;


        public MainWindow()
        {
            InitializeComponent();
            createStyles();
            XmlRead();
            createPanel();
            threadForDate();

        }

        private void XmlRead()
        {
            int i = 0;
            XmlTextReader reader = new XmlTextReader(file);
            while (reader.Read())
            {
                if (reader.Name == "Stiker")
                    n++;
                if (reader.Name == "Theme")
                {
                    Themes.Add(reader.ReadInnerXml().ToString());

                }
                if (reader.Name == "Content")
                {
                    Texts.Add(reader.ReadInnerXml().ToString());

                }
                if (reader.Name == "DataTime")
                {
                    DataTimes.Add(reader.ReadInnerXml().ToString());

                }
            }
        }

        /// <createPanel>
        /// Функция создания заметок на экране
        /// </createPanel>
        private void createPanel()
        {
            for (int i = 0; i < Themes.Count; i++)
            {
                Expander expander = new Expander();
                StackPanel stackPanel1 = new StackPanel();
                TextBlock textBlock_1 = new TextBlock();
                TextBlock textBlock_2 = new TextBlock();

                textBlock_1.Style = Application.Current.FindResource("MaterialDesignBody") as Style;
                textBlock_1.Text = DataTimes[i].ToString();
                textBlock_1.Background = new SolidColorBrush(Colors.Black) { Opacity = 0.5 };
                textBlock_1.Foreground = new SolidColorBrush(Colors.White);
                textBlock_1.Margin = new Thickness(0);
                textBlock_1.Padding = new Thickness(20, 10, 20, 10);

                textBlock_2.Style = Application.Current.FindResource("MaterialDesignBody") as Style;
                textBlock_2.Opacity = 68;
                textBlock_2.Text = Texts[i].ToString();
                textBlock_2.TextWrapping = TextWrapping.Wrap;
                textBlock_2.Background = new SolidColorBrush(Colors.Black) { Opacity = 0.5 };
                textBlock_2.Foreground = new SolidColorBrush(Colors.White);
                textBlock_2.Margin = new Thickness(0);
                textBlock_2.Padding = new Thickness(20, 2, 20, 0);

                stackPanel1.Orientation = Orientation.Vertical;
                stackPanel1.Margin = new Thickness(0);

                expander.HorizontalAlignment = HorizontalAlignment.Stretch;
                expander.Header = Themes[i].ToString();
                expander.Margin = new Thickness(5, 0, 5, 0);
                expander.Background = new SolidColorBrush(Colors.Black) { Opacity = 0.5 };
                expander.Foreground = new SolidColorBrush(Colors.White);

                stackPanel1.Children.Add(textBlock_1);
                stackPanel1.Children.Add(textBlock_2);
                expander.Content = stackPanel1;
                listPanel.Children.Add(expander);
            }

        }


        private void addSet_Click(object sender, RoutedEventArgs e)
        {
            AddSetGrid.Visibility = Visibility.Visible;
            AddSetBut.Visibility = Visibility.Hidden;
            backBut.Visibility = Visibility.Visible;
            listPanel.Visibility = Visibility.Hidden;

        }

        private void backClick(object sender, RoutedEventArgs e)
        {
            backBut.Visibility = Visibility.Hidden;
            AddSetBut.Visibility = Visibility.Visible;
            AddSetGrid.Visibility = Visibility.Hidden;
            themesGrid.Visibility = Visibility.Hidden;
            listPanel.Visibility = Visibility.Visible;
            temaBox.Text = "";
            textToEl.Text = null;
            dataEl.Text = null;
            timeEl.Text = null;

        }

        private void saveClick(object sender, RoutedEventArgs e)
        {


            themeXml = temaBox.Text;
            textXml = new TextRange(textToEl.Document.ContentStart, textToEl.Document.ContentEnd).Text;
            datatimeXml = dataEl.Text + " " + timeEl.Text;

            if (themeXml == "" || textXml == "" || datatimeXml == "" || dataEl.Text == null || timeEl.Text == null)
            {
                MessageBox.Show("Присутствуют незаполненные поля");
            }
            else
            {
                Volume.IsChecked = false;
                backBut.Visibility = Visibility.Hidden;
                AddSetBut.Visibility = Visibility.Visible;
                AddSetGrid.Visibility = Visibility.Hidden;
                listPanel.Visibility = Visibility.Visible;
                xmlAdd();



                listPanel.Children.Clear();
                XmlRead();
                createPanel();
                Themes.Clear();
                Texts.Clear();
                DataTimes.Clear();
            }



        }


        private void themes_Click(object sender, RoutedEventArgs e)
        {
            themesGrid.Visibility = Visibility.Visible;
            AddSetBut.Visibility = Visibility.Hidden;
            backBut.Visibility = Visibility.Visible;
            AddSetGrid.Visibility = Visibility.Hidden;
            listPanel.Visibility = Visibility.Hidden;

        }


        private void createStyles()
        {
            Button[,] listBut = new Button[3, 3];
            string[,] themesName = { { "Gradient", "Rain", "Relax" }, { "Sea", "Cat", "Nature" }, { "Girl", "Dark", "Fun" } };
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    listBut[i, j] = new Button();
                    listBut[i, j].Content = themesName[i, j].ToString();
                    if (themesName[i, j].ToString() == "Cat" || themesName[i, j].ToString() == "Girl")
                    {
                        listBut[i, j].Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    }
                    listBut[i, j].BorderBrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

                    ImageBrush myBrush = new ImageBrush();
                    myBrush.ImageSource = new BitmapImage(new Uri("Resources/" + i.ToString() + j.ToString() + ".jpg", UriKind.Relative));

                    listBut[i, j].Background = myBrush;
                    listBut[i, j].Width = 140;
                    listBut[i, j].Height = 100;
                    listBut[i, j].Margin = new Thickness(5, 5, 5, 5);
                    listBut[i, j].VerticalAlignment = VerticalAlignment.Center;
                    listBut[i, j].HorizontalAlignment = HorizontalAlignment.Left;
                    listBut[i, j].FontSize = 24;
                    //listBut[i, j].HorizontalAlignment = HorizontalAlignment.Center;
                    listBut[i, j].Click += delegate
                    {
                        UpLine.Background = new SolidColorBrush(Color.FromArgb(80, 115, 120, 236));

                        mainGrid.Background = myBrush;
                    };
                    PicturesGrid.Children.Add(listBut[i, j]);
                    Grid.SetRow(listBut[i, j], i);
                    Grid.SetColumn(listBut[i, j], j);
                }
        }


        // Функция добавления записей в XML 
        private void xmlAdd()
        {
            xDoc.Load(file);
            xRoot = xDoc.DocumentElement;
            //Создание нового элемента Striker с атрибутом Num
            XmlElement Element = xDoc.CreateElement("Stiker");
            XmlAttribute numEl = xDoc.CreateAttribute("Num");

            //Создание остальных атрибутов
            XmlElement theme = xDoc.CreateElement("Theme");
            XmlElement text = xDoc.CreateElement("Content");
            XmlElement dataTime = xDoc.CreateElement("DataTime");

            // создаем текстовые значения для элементов и атрибута
            XmlText numStick = xDoc.CreateTextNode("1");
            XmlText themeElem = xDoc.CreateTextNode(themeXml);
            XmlText content = xDoc.CreateTextNode(textXml);
            XmlText dataTimeElem = xDoc.CreateTextNode(datatimeXml);

            //добавляем узлы
            //К атрибутам привязывыем строки, соответствующие созданной записи
            numEl.AppendChild(numStick);
            theme.AppendChild(themeElem);
            text.AppendChild(content);
            dataTime.AppendChild(dataTimeElem);

            //К связь основного элемента с зависимыми
            Element.Attributes.Append(numEl);
            Element.AppendChild(theme);
            Element.AppendChild(text);
            Element.AppendChild(dataTime);

            xRoot.AppendChild(Element);
            xDoc.Save("users.xml");
        }

        //Функция поиска совпадения текущей даты и времени, с данными списка DataTimes
        public void threadForDate()
        {
            System.Threading.Tasks.Task.Run(() =>
            {
              while (true)
              {
                  dateTimeNow.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
                  {
                      dateTimeNow.Text = System.DateTime.Now.ToShortDateString() +" "+ System.DateTime.Now.ToString("HH:mm");
                      
                  }
                  ));

                  /*if (Themes.Contains(dateTimeNow.Text))
                        MessageBox.Show("ТАМ ЧОТА ЕСТЬ");
                  System.Threading.Thread.Sleep(1000);*/
              }
            });

        }
    }
}
