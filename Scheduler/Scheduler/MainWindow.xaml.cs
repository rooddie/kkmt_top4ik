using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;


namespace Scheduler
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string file = "users.xml";//xml файл
        public XmlDocument xDoc = new XmlDocument();//переменная для работы с xml документом
        public XmlElement xRoot;//Получает основной элемент xml,  относительно которого производятся записи

        //Переменные, данные к которым присваиваются при создании задачи, для передачи в xml
        public string numXml = "";
        public string themeXml = "";
        public string textXml = null;
        public string datatimeXml = null;

        //Списки, формирующиеся при считывании xml файла, необходимы для созданя задач на экране
        public static List<string> Numbers = new List<string>();
        public static List<string> Themes = new List<string>();
        public static List<string> Texts = new List<string>();
        public static List<string> DataTimes = new List<string>();

        public int num = 0;//Кол-во "Stiker" в xml 
        public static string timeNOW;//Текущее дата и время компьютера

        public static System.Windows.Forms.NotifyIcon notify = new System.Windows.Forms.NotifyIcon();//Иконка свернутого в трей приложения
        public bool statusApp = true;//Статус приложения(открыто/свернуто)
        public System.Windows.Forms.ContextMenu contextMenu1 = new System.Windows.Forms.ContextMenu();//Контекстное меню при нажатии пкм по иконке в трее


        public MainWindow()
        {
            InitializeComponent();
            createStyles();
            XmlRead();
            createPanel();
            threadForDate();
            Notifycation();

            App.LanguageChanged += LanguageChanged;//кол-во языков в программе

            CultureInfo currLang = App.Language;//Перевод приложения

          
            menuLanguage.Items.Clear();  //Отчистка меню смены языка:

            //Заполнение lixtbox с языками
            foreach (var lang in App.Languages)
            {
                MenuItem menuLang = new MenuItem();
              
                menuLang.Margin = new Thickness(0, 0, -120, 0);
               
                menuLang.Header = lang.DisplayName;
                menuLang.Tag = lang; //Хранение выбранного языка
                menuLang.IsChecked = lang.Equals(currLang); //Проверка на тот же язык
                menuLang.Click += ChangeLanguageClick;
                menuLanguage.Items.Add(menuLang);//Добавление языка в листбокс
            }

        }

        /// <summary>
        /// Функция обработки события LanguageChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LanguageChanged(Object sender, EventArgs e)
        {
            CultureInfo currLang = App.Language;

            //Отмечаем нужный пункт смены языка как выбранный язык
            foreach (MenuItem i in menuLanguage.Items)
            {
                CultureInfo ci = i.Tag as CultureInfo;
                i.IsChecked = ci != null && ci.Equals(currLang);
            }
        }

        /// <summary>
        /// Функция смены языка
        /// </summary>
        /// <param name="sender"> объект, на который кликнули</param>
        /// <param name="e">событие</param>
        private void ChangeLanguageClick(Object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi != null)
            {
                CultureInfo lang = mi.Tag as CultureInfo;
                if (lang != null)
                {
                    App.Language = lang;
                }
            }
            LangGrid.Visibility = Visibility.Hidden; //После выбора языка, меня скрывается

        }

        /// <summary>
        /// Функция чтения xml файла
        /// </summary>
        private void XmlRead()
        {
            XmlTextReader reader = new XmlTextReader(file); //Считываемый файл
            
            //Поиск узлов по названиям и запись их в массивы
            while (reader.Read())
            {
                if (reader.Name == "Stiker")
                    num++;
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
            numXml = (num / 2).ToString();//т.к считывается еще и закрывающий объект
        }

        /// <createPanel>
        /// Функция создания заметок на экране
        /// </createPanel>
        private void createPanel()
        {
            for (int i = 0; i < Themes.Count; i++)
            {
                Expander expander = new Expander(); //экспандер, на котором все элементы и будут расположены
                StackPanel stackPanel1 = new StackPanel();//Выезжающее меню
                TextBlock dataTime_tb = new TextBlock();//Дата и время на панели
                TextBlock text_tb = new TextBlock();//Содержание на панели
                Grid inExpander = new Grid();//грид, для размещения кнопки и темы
                TextBlock toHeadEx = new TextBlock();//Тема на панели
                Button but = new Button();//Кнопка удаления записи

                //Настройка вида даты и время на панели

                dataTime_tb.Style = Application.Current.FindResource("MaterialDesignBody") as Style; //Стиль даты/время
                dataTime_tb.Text = DataTimes[i].ToString();
                dataTime_tb.Background = new SolidColorBrush(Colors.Black) { Opacity = 0.5 };
                dataTime_tb.Foreground = new SolidColorBrush(Colors.White);
                dataTime_tb.Margin = new Thickness(0);
                dataTime_tb.Padding = new Thickness(20, 10, 20, 10);

                text_tb.Style = Application.Current.FindResource("MaterialDesignBody") as Style;
                text_tb.Opacity = 68;
                text_tb.Text = Texts[i].ToString();
                text_tb.TextWrapping = TextWrapping.Wrap;
                text_tb.Background = new SolidColorBrush(Colors.Black) { Opacity = 0.5 };
                text_tb.Foreground = new SolidColorBrush(Colors.White);
                text_tb.Margin = new Thickness(0);
                text_tb.Padding = new Thickness(20, 2, 20, 0);

                toHeadEx.Text = Themes[i].ToString();
                toHeadEx.Margin = new Thickness(50, 0, 0, 0);

                but.Margin = new Thickness(-15,0,0,0);
                but.HorizontalAlignment = HorizontalAlignment.Left;
                but.Click += delegate
                {
                    xDoc.Load(file);


                    xDoc.Save(file);
                };

                inExpander.Children.Add(toHeadEx);
                inExpander.Children.Add(but);

                stackPanel1.Orientation = Orientation.Vertical;
                stackPanel1.Margin = new Thickness(0);

                expander.HorizontalAlignment = HorizontalAlignment.Stretch;
                expander.Header = inExpander;
                expander.Margin = new Thickness(5, 0, 5, 0);
                expander.Background = new SolidColorBrush(Colors.Black) { Opacity = 0.5 };
                expander.Foreground = new SolidColorBrush(Colors.White);

                stackPanel1.Children.Add(dataTime_tb);
                stackPanel1.Children.Add(text_tb);
                
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
            LangGrid.Visibility = Visibility.Hidden;

        }

        private void backClick(object sender, RoutedEventArgs e)
        {
            backBut.Visibility = Visibility.Hidden;
            AddSetBut.Visibility = Visibility.Visible;
            AddSetGrid.Visibility = Visibility.Hidden;
            themesGrid.Visibility = Visibility.Hidden;
            listPanel.Visibility = Visibility.Visible;
            LangGrid.Visibility = Visibility.Hidden;
        }

        private void saveClick(object sender, RoutedEventArgs e)
        {
            Themes.Clear();
            Texts.Clear();
            DataTimes.Clear();

            themeXml = temaBox.Text;
            textXml = new TextRange(textToEl.Document.ContentStart, textToEl.Document.ContentEnd).Text;
            datatimeXml = dataEl.Text + " " + timeEl.Text + ":01";
            
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
                temaBox.Text = "";
                textToEl.Text = null;
                dataEl.Text = null;
                timeEl.Text = null;
            }



        }


        private void themes_Click(object sender, RoutedEventArgs e)
        {
            themesGrid.Visibility = Visibility.Visible;
            AddSetBut.Visibility = Visibility.Hidden;
            backBut.Visibility = Visibility.Visible;
            AddSetGrid.Visibility = Visibility.Hidden;
            listPanel.Visibility = Visibility.Hidden;
            LangGrid.Visibility = Visibility.Hidden;

        }
        private void languages_click(object sender, RoutedEventArgs e)
        {
            AddSetGrid.Visibility = Visibility.Hidden;
            themesGrid.Visibility = Visibility.Hidden;
            LangGrid.Visibility = Visibility.Visible;
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
            XmlText numStick = xDoc.CreateTextNode(numXml);
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
            num = 0;
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
                      timeNOW = System.DateTime.Now.ToShortDateString() +" "+ System.DateTime.Now.ToString("HH:mm:ss");
                    //MessageBox.Show(DataTimes[2].ToString());
                    if (DataTimes.Contains(timeNOW))
                    {
                        notify.Visible = true;
                        int index = DataTimes.IndexOf(timeNOW);
                        notify.BalloonTipTitle = "Планировщик";
                        notify.BalloonTipText = Themes[index] + "\n" + Texts[index];
                        
                        notify.ShowBalloonTip(10);
                    }
                    System.Threading.Thread.Sleep(1000);
              }
            });
        }

        public void Notifycation()
        {
            notify.Icon = System.Drawing.SystemIcons.Error;
            notify.Visible = false;
            createMenu();
            notify.DoubleClick += delegate(object sender, EventArgs args)
            {
                if (statusApp == false)
                {
                    notify.Visible = false;
                    this.Show();
                }

            };
           // notify.Click += close;
            
        }
        private void AppInTray(object sender, System.ComponentModel.CancelEventArgs e)
        {
            notify.Visible = true;
            e.Cancel = true;
            statusApp = false;
            notify.BalloonTipText = "Окно было свернуто";
            notify.ShowBalloonTip(5);
            this.Hide();
        }

        public void createMenu()
        {
            // Add menu items to shortcut menu.  
            System.Windows.Forms.MenuItem open = new System.Windows.Forms.MenuItem();
            open.Text = "Открыть";
            open.Click += delegate
            {
                notify.Visible = false;
                this.Show();
            };
            contextMenu1.MenuItems.Add(open);
            System.Windows.Forms.MenuItem close = new System.Windows.Forms.MenuItem();
            close.Text = "Закрыть";
            close.Click += delegate
            {
                Environment.Exit(0);
            };
            contextMenu1.MenuItems.Add(close);
            notify.ContextMenu = contextMenu1;
        }
    }
}
