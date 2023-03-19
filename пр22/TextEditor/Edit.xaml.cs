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
    /// Логика взаимодействия для Edit.xaml
    /// </summary>
    public partial class Edit : Page
    {
        MainWindow mainWindow;
        string path;
        string fileText;
        public Edit(MainWindow mainWindow, string _text = null, string _path = null)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            if(_text != null)
            {
                text.Text = _text;
                fileText = _text;
                SymbolsAndWordsCount(_text);
            }
            if(_path != null)
            {
                path = _path;
            }
        }

        private void New(object sender, RoutedEventArgs e)
        {
            path = null;
            fileText = null;
            text.Text = "";
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();

            if (fileDialog.FileName != "")
            {
                string fileText = File.ReadAllText(fileDialog.FileName);
                text.Text = fileText;
                path = fileDialog.FileName;
            }
            else
            {
                MessageBox.Show("Выберите файл");
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if(path != null)
            {
                File.WriteAllText(path, text.Text);
                mainWindow.frame.Navigate(new Main(mainWindow));
            }
            else
            {
                SaveAs(text.Text);
            }
        }

        private void SaveNew(object sender, RoutedEventArgs e)
        {
            SaveAs(text.Text);
        }

        private void SaveAs(string text)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFile.ShowDialog();
            if(saveFile.FileName != "")
            {
                File.WriteAllText(saveFile.FileName, text);
            }
            else
            {
                MessageBox.Show("Выберите путь сохранения");
            }
        }

        private void SymbolsAndWordsCount(string text)
        {
            symbols.Content = text.Length;
            var wordsCount = text.Trim().Split(' ').Length;
            wordsCount += text.Split('\n').Length - 1;
            words.Content = wordsCount;
        }

        private void TextChange(object sender, TextChangedEventArgs e)
        {
            SymbolsAndWordsCount(text.Text);
        }
    }
}
