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

namespace ExpertSystems.Pages
{
    /// <summary>
    /// Логика взаимодействия для Step3.xaml
    /// </summary>
    public partial class Step3 : Page
    {
        MainWindow mainWindow;
        public Step3(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void Spine(object sender, RoutedEventArgs e)
        {
            mainWindow.stepData.Add("spine");
            mainWindow.OpenPage(MainWindow.Pages.step4);
        }

        private void Biceps(object sender, RoutedEventArgs e)
        {
            mainWindow.stepData.Add("biceps");
            mainWindow.OpenPage(MainWindow.Pages.step4);
        }

        private void Triceps(object sender, RoutedEventArgs e)
        {
            mainWindow.stepData.Add("triceps");
            mainWindow.OpenPage(MainWindow.Pages.step4);
        }

        private void CalfMuscle(object sender, RoutedEventArgs e)
        {
            mainWindow.stepData.Add("calfmuscle");
            mainWindow.OpenPage(MainWindow.Pages.step4);
        }
    }
}
