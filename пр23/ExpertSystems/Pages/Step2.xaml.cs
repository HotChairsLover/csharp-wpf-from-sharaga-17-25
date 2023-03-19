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
    /// Логика взаимодействия для Step2.xaml
    /// </summary>
    public partial class Step2 : Page
    {
        MainWindow mainWindow;
        public Step2(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            question.Content = String.Format("Отлично, {0}. Как часто вы готовы заниматься спортом?", mainWindow.stepData[0].Split('*')[0]);
        }

        private void OneDay(object sender, RoutedEventArgs e)
        {
            mainWindow.stepData.Add("Everyday");
            mainWindow.OpenPage(MainWindow.Pages.step3);

        }

        private void ThreeWeeks(object sender, RoutedEventArgs e)
        {
            mainWindow.stepData.Add("ThreeInWeek");
            mainWindow.OpenPage(MainWindow.Pages.step3);
        }

        private void TwoWeeks(object sender, RoutedEventArgs e)
        {
            mainWindow.stepData.Add("TwoInWeek");
            mainWindow.OpenPage(MainWindow.Pages.step3);
        }

        private void OneWeeks(object sender, RoutedEventArgs e)
        {
            mainWindow.stepData.Add("OneInWeek");
            mainWindow.OpenPage(MainWindow.Pages.step3);
        }
    }
}
