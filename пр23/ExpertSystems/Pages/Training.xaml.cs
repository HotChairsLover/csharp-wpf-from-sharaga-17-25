using System;
using System.Collections.Generic;
using System.IO;
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

namespace ExpertSystems.Pages
{
    /// <summary>
    /// Логика взаимодействия для Training.xaml
    /// </summary>
    public partial class Training : Page
    {
        MainWindow mainWindow;
        public class URLs
        {
            public List<string> urlResourse = new List<string>();
        }
        public List<URLs> urls = new List<URLs>(1);

        public Training(MainWindow mainWindow, string id = "")
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            URLs newElement = new URLs();
            if(id == "spine")
            {
                newElement.urlResourse.Add("https://www.youtube.com/watch?v=kmjweaibS0");
                newElement.urlResourse.Add("https://www.youtube.com/watch?v=7AgKyL47XnU");
                firstImg.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\img\spine.jpg"));
                secondImg.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\img\spine.jpg"));
            }
            else if (id == "triceps")
            {
                Title = "Тренировка для трицепса";
                newElement.urlResourse.Add("https://www.youtube.com/watch?v=kmjweaibS0");
                newElement.urlResourse.Add("https://www.youtube.com/watch?v=7AgKyL47XnU");
                firstImg.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\img\triceps.jpg"));
                secondImg.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\img\triceps.jpg"));
            }
            else if (id == "biceps")
            {
                Title = "Тренировка для бицепса";
                newElement.urlResourse.Add("https://www.youtube.com/watch?v=kmjweaibS0");
                newElement.urlResourse.Add("https://www.youtube.com/watch?v=7AgKyL47XnU");
                firstImg.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\img\biceps.jpeg"));
                secondImg.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\img\biceps.jpeg"));
            }
            else if (id == "calfmuscle")
            {
                Title = "Тренировка для икроножной мышцы";
                newElement.urlResourse.Add("https://www.youtube.com/watch?v=kmjweaibS0");
                newElement.urlResourse.Add("https://www.youtube.com/watch?v=7AgKyL47XnU");
                firstImg.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\img\calfmuscle.jpg"));
                secondImg.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\img\calfmuscle.jpg"));
            }
            urls.Add(newElement);
        }

        private void OpenBrowser(object sender, MouseButtonEventArgs e)
        {
            int id = -1;

            for(int i = 0; i < parrent.Children.Count; i++)
            {
                if(parrent.Children[i] == sender)
                {
                    id = i;
                    break;
                }
            }
            System.Diagnostics.Process.Start(urls[0].urlResourse[id]);
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.Pages.main);
        }
    }
}
