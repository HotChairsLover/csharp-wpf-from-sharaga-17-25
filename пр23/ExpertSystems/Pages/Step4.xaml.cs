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
    /// Логика взаимодействия для Step4.xaml
    /// </summary>
    public partial class Step4 : Page
    {
        MainWindow mainWindow;
        public Step4(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void NextStep(object sender, RoutedEventArgs e)
        {
            mainWindow.stepData.Add(String.Format("{0}*{1}", weight.Text, growth.Text));
            mainWindow.OpenPage(MainWindow.Pages.step5);
        }
    }
}
