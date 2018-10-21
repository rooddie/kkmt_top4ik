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
using System.Xml;
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

        public string numXml;
        public string themeXml;
        public string textXml;
        public string datatimeXml;

        public MainWindow()
        {
            InitializeComponent();

            createStyles();

        }

        private void addSet_Click(object sender, RoutedEventArgs e)
        {
            AddSetGrid.Visibility = Visibility.Visible;
            AddSetBut.Visibility = Visibility.Hidden;
            backBut.Visibility = Visibility.Visible;

        }

        private void backClick(object sender, RoutedEventArgs e)
        {
            backBut.Visibility = Visibility.Hidden;
            AddSetBut.Visibility = Visibility.Visible;
            AddSetGrid.Visibility = Visibility.Hidden;
            themesGrid.Visibility = Visibility.Hidden;

        }

        private void saveClick(object sender, RoutedEventArgs e)
        {
            Volume.IsChecked = false;
            closeSettingBut();
            backBut.Visibility = Visibility.Hidden;
            AddSetBut.Visibility = Visibility.Visible;
            AddSetGrid.Visibility = Visibility.Hidden;

            themeXml = temaBox.Text;
            textXml = new TextRange(textToEl.Document.ContentStart, textToEl.Document.ContentEnd).Text;
            datatimeXml = dataEl.Text + " " + timeEl.Text;
            xmlAdd();
        }

       
        private void themes_Click(object sender, RoutedEventArgs e)
        {
            themesGrid.Visibility = Visibility.Visible;
            AddSetBut.Visibility = Visibility.Hidden;
            backBut.Visibility = Visibility.Visible;
            AddSetGrid.Visibility = Visibility.Hidden;

        }

        private void closeSettingBut()
        {
            
            languagesBut.Visibility = Visibility.Hidden;
            temesBut.Visibility = Visibility.Hidden;
        }

        private void openSettingBut()
        {
            languagesBut.Visibility = Visibility.Visible;
            temesBut.Visibility = Visibility.Visible;
        }

        void createStyles()
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

        void xmlAdd()
        {
            xDoc.Load(file);
            xRoot = xDoc.DocumentElement;
            // создаем новый элемент user
            XmlElement Element = xDoc.CreateElement("Stiker");
            XmlAttribute numEl = xDoc.CreateAttribute("Num");

            XmlElement theme = xDoc.CreateElement("Theme");
            XmlElement text = xDoc.CreateElement("Content");
            XmlElement dataTime = xDoc.CreateElement("DataTime");
            // создаем текстовые значения для элементов и атрибута

            XmlText numStick = xDoc.CreateTextNode("1");
            XmlText themeElem = xDoc.CreateTextNode(themeXml);
            XmlText content = xDoc.CreateTextNode(textXml);
            XmlText dataTimeElem = xDoc.CreateTextNode(datatimeXml);

            //добавляем узлы

            numEl.AppendChild(numStick);
            theme.AppendChild(themeElem);
            text.AppendChild(content);
            dataTime.AppendChild(dataTimeElem);

            Element.Attributes.Append(numEl);
            Element.AppendChild(theme);
            Element.AppendChild(text);
            Element.AppendChild(dataTime);

            xRoot.AppendChild(Element);
            xDoc.Save("users.xml");
        }

    }
}
