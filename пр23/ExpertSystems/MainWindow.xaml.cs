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

namespace ExpertSystems
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> stepData = new List<string>();
        public enum Pages
        {
            step1, step2, step3, step4, step5, main
        }
        public MainWindow()
        {
            InitializeComponent();
            if(!File.Exists(Directory.GetCurrentDirectory() + @"\" + "123.txt"))
            {
                OpenPage(Pages.step1);
            }
            else
            {
                string[] textLines = File.ReadAllText(Directory.GetCurrentDirectory() + @"\" + "123.txt").Split('\n');
                if(textLines[2] == "spine" || textLines[2] == "biceps" || textLines[2] == "triceps" || textLines[2] == "calfmuscle")
                {
                    frame.Navigate(new ExpertSystems.Pages.Training(this, textLines[2]));
                }
                else
                {
                    File.Delete(Directory.GetCurrentDirectory() + @"\" + "123.txt");
                    OpenPage(Pages.step1);
                }

            }
        }

        public void OpenPage(Pages page)
        {
            if (page == Pages.step1) frame.Navigate(new ExpertSystems.Pages.Step1(this));
            if (page == Pages.step2) frame.Navigate(new ExpertSystems.Pages.Step2(this));
            if (page == Pages.step3) frame.Navigate(new ExpertSystems.Pages.Step3(this));
            if (page == Pages.step4) frame.Navigate(new ExpertSystems.Pages.Step4(this));
            if (page == Pages.step5) frame.Navigate(new ExpertSystems.Pages.Step5(this));
            if (page == Pages.main) frame.Navigate(new ExpertSystems.Pages.Main(this));
        }
    }
}
