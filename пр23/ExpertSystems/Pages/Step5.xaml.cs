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
    /// Логика взаимодействия для Step5.xaml
    /// </summary>
    public partial class Step5 : Page
    {
        MainWindow mainWindow;
        public Step5(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void NextStep(object sender, RoutedEventArgs e)
        {
            if(man.IsChecked == true)
            {
                mainWindow.stepData.Add("Man");
                mainWindow.OpenPage(MainWindow.Pages.main);
            }
            else if(woman.IsChecked == true)
            {
                mainWindow.stepData.Add("Woman");
                mainWindow.OpenPage(MainWindow.Pages.main);
            }
            
        }
    }
}
