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
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        MainWindow mainWindow;
        public Main(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            spineImg.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\img\spine.jpg"));
            tricepsImg.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\img\triceps.jpg"));
            bicepsImg.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\img\biceps.jpeg"));
            calfMuscleImg.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\img\calfmuscle.jpg"));
            if (!File.Exists(Directory.GetCurrentDirectory() + @"\" + "123.txt"))
            {
                if (mainWindow.stepData.Count != 0)
                {
                    string path = Directory.GetCurrentDirectory() + @"\" + "123.txt";
                    foreach (string data in mainWindow.stepData)
                    {
                        File.AppendAllText(path, String.Format("{0}\n", data));
                    }
                }
            }
        }

        private void SpineOpen(object sender, MouseButtonEventArgs e)
        {
            mainWindow.frame.Navigate(new Training(mainWindow, "spine"));
        }

        private void TricepsOpen(object sender, MouseButtonEventArgs e)
        {
            mainWindow.frame.Navigate(new Training(mainWindow, "triceps"));
        }

        private void BicepsOpen(object sender, MouseButtonEventArgs e)
        {
            mainWindow.frame.Navigate(new Training(mainWindow, "biceps"));
        }

        private void CalfMuscleOpen(object sender, MouseButtonEventArgs e)
        {
            mainWindow.frame.Navigate(new Training(mainWindow, "calfmuscle"));
        }
    }
}
