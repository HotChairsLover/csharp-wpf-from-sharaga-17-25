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
using System.IO;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Globalization;

namespace Generator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class FilesInfo
        {
            public BitmapImage image { get; set; }
            public string name { get; set; }
            public string length { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        public void CopyStream(string _inputStream, string _outputStream)
        {
            FileStream inputStream = new FileStream(_inputStream, FileMode.Open);
            using (FileStream outputStream = File.Create(_outputStream))
            {
                byte[] buffer = new byte[1024];
                int bytesRead;
                while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    outputStream.Write(buffer, 0, bytesRead);
                }
                outputStream.Flush();
                inputStream.Close();
                outputStream.Close();
            }
        }
        public void LoadData(string name, bool update = false)
        {
            var imgList = Directory.GetFiles(Directory.GetCurrentDirectory(), name);
            FilesInfo filesInfo = new FilesInfo();
            /*if (update)
            {
                string path = Directory.GetCurrentDirectory() + "\\" + name;
                var bitImg = new BitmapImage(new Uri(path));
                filesInfo.image = bitImg;
                int index = path.Split('\\').Length;
                string imgName = path.Split('\\')[index-1];
                filesInfo.name = imgName;
                filesInfo.length = new FileInfo(name).Length.ToString();
                listView.Items.Add(filesInfo);
            }*/
            //else
            //{
            foreach (string file in imgList)
            {

                var bitImg = new BitmapImage();
                bitImg.BeginInit();
                bitImg.CacheOption = BitmapCacheOption.OnLoad;
                bitImg.UriSource = new Uri(file);

                bitImg.EndInit();
                //bitImg.Freeze();

                //var writeBitImg = new WriteableBitmap(bitImg);
                filesInfo.image = bitImg;
                int index = file.Split('\\').Length;
                string imgName = file.Split('\\')[index - 1];
                filesInfo.name = imgName;
                filesInfo.length = new FileInfo(file).Length.ToString();
                listView.Items.Add(filesInfo);
            }
            //}
        }
        private void LoadImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog(); // создание переменной для выбора файлов
            fileDialog.InitialDirectory = "C:\\"; // указание изначальной директории открываемой в окне выбора
            fileDialog.Multiselect = true; // разрешение выбора множества файлов
            fileDialog.ShowDialog(); // открытие окна для выбора файлов

            if (fileDialog.FileName != "" || fileDialog.FileNames.Length > 0) // если выбран какой-то файл
            {
                List<string> names = new List<string>(); // создается список для имен находящихся в папке сохранения
                for (int i = 0; i < fileDialog.FileNames.Length; i++) // цикл перебора всех выбранных файлов
                {
                    string newNameNews = NameFiles(); // генерация случайного имени

                    while (true) // бесконечный цикл
                    {
                        names.Clear(); // отчистка ранее созданного списка
                        var files = Directory.GetFiles(Directory.GetCurrentDirectory()).ToList(); // получение всех файлов в директории с экзешником
                        foreach (var file in files) // перебор всех полученых файлов
                        {
                            names.Add(System.IO.Path.GetFileName(file)); // получение и добавление имен этих файлов в список
                        }
                        if (names.Contains(newNameNews)) // если список содержит сгенерированное имя
                        {
                            newNameNews = NameFiles(); // генерируем новое
                            continue; // возвращаемся к началу цикла while(true)
                        }
                        else // если нет сгенерированного имени в списке
                        {
                            CopyStream(fileDialog.FileNames[i], newNameNews); // копируем картинку в папку с exe
                            LoadData(newNameNews); // выводим ее в лист вью
                            break; // прерываем цикл
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите файлы");
            }
        }

        public string NameFiles()
        {
            string nameFile = "";
            Random random = new Random();

            for (int i = 0; i < 2; i++)
            {
                nameFile += (char)random.Next(0x0410, 0x44F);
            }
            nameFile += ".png";

            return nameFile;
        }

        private void DeleteImage(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem != null)
            {
                var itm = listView.SelectedItem as FilesInfo;
                listView.Items.Remove(listView.SelectedItem);
                File.Delete(Directory.GetCurrentDirectory() + "/" + itm.name);
            }
        }

        private void UpdateImage(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem != null)
            {              
                var itm = listView.SelectedItem as FilesInfo;
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.InitialDirectory = "C:\\";
                fileDialog.Multiselect = false;
                fileDialog.ShowDialog();

                if (fileDialog.FileName != "")
                {
                    listView.Items.Remove(listView.SelectedItem);
                    File.Delete(Directory.GetCurrentDirectory() + "/" + itm.name);
                    itm.name = String.Format("{0}(1).{1}", itm.name.Split('.')[0], itm.name.Split('.')[1]);
                    CopyStream(fileDialog.FileName, itm.name);                   
                    LoadData(itm.name);
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите файл");
                }
            }
        }
    }
}
