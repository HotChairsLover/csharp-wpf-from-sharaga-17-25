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

namespace TextEditor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum pages
        {
            main,
            edit
        }
        public class FileInfos
        {
            public string name { get; set; }
            public string path { get; set; }

            public FileInfos(string _name, string _path)
            {
                name = _name;
                path = _path;
            }
        }
        public List<FileInfos> fileInfos = new List<FileInfos>();
        public MainWindow()
        {
            InitializeComponent();
            fileInfos.Add(new FileInfos("Текстовое имя", "Текстовое расположение"));
            fileInfos.Add(new FileInfos("Текстовое имя 1", "Текстовое расположение 1"));
            OpenPages(pages.main);
        }

        public void OpenPages(pages _page)
        {
            if(_page == pages.main)
            {
                frame.Navigate(new Main(this));
            }
            else if (_page == pages.edit)
            {
                frame.Navigate(new Edit(this));
            }
        }
    }
}
