using Microsoft.Win32;
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

namespace TextEditor
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
            LoadData();
        }

        public void LoadData()
        {
            for(int i = 0; i < mainWindow.fileInfos.Count; i++)
            {
                Grid global = new Grid();
                global.Height = 50;
                global.MouseDown += delegate
                {

                };

                Image icon = new Image();
                icon.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"/" + "textIcon.png"));
                icon.Width = 30;
                icon.Height = 30;
                icon.Margin = new Thickness(10, 10, 10, 10);
                icon.HorizontalAlignment = HorizontalAlignment.Left;

                Label name = new Label();
                name.Content = mainWindow.fileInfos[i].name;
                name.HorizontalAlignment = HorizontalAlignment.Left;
                name.Margin = new Thickness(45, 10, 0, 0);
                name.HorizontalAlignment = HorizontalAlignment.Left;
                name.VerticalAlignment = VerticalAlignment.Top;
                name.FontWeight = FontWeights.Bold;

                Label path = new Label();
                path.Content = mainWindow.fileInfos[i].path;
                path.HorizontalAlignment = HorizontalAlignment.Left;
                path.Margin = new Thickness(45, 25, 0, 0);
                path.HorizontalAlignment = HorizontalAlignment.Left;
                path.VerticalAlignment = VerticalAlignment.Top;

                global.Children.Add(icon);
                global.Children.Add(name);
                global.Children.Add(path);

                parrent.Children.Add(global);
            }
        }

        private void NewFile(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPages(MainWindow.pages.edit);
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();

            if(fileDialog.FileName != "")
            {
                string text = File.ReadAllText(fileDialog.FileName);
                mainWindow.frame.Navigate(new Edit(mainWindow, text, fileDialog.FileName));
            }
            else
            {
                MessageBox.Show("Выберите файл");
            }
        }
    }
}
