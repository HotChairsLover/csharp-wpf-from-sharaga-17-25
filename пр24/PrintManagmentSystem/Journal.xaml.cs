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

namespace PrintManagmentSystem
{
    /// <summary>
    /// Логика взаимодействия для Journal.xaml
    /// </summary>
    public partial class Journal : Page
    {
        public class DataForListView
        {
            public int id { get; set; }
            public string fio { get; set; }
            public string typeOperationText { get; set; }
            public string formatText { get; set; }
            public int side { get; set; }
            public string colorText { get; set; }
            public int count { get; set; }
            public int price { get; set; }
            public BitmapImage signature { get; set; }
            public string date { get; set; }
        }
        public List<DataForListView> dataForListView = new List<DataForListView>();
        public Journal()
        {
            InitializeComponent();
            LoadData();
        }
        void LoadData()
        {
            Operations.Items.Clear();
            dataForListView.Clear();
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(Directory.GetCurrentDirectory() + "/img/nosignature.jpg");
            img.EndInit();
            Image myimg = new Image();
            myimg.Source = img;
            string query = "SELECT * FROM `mydb`.`operations`";
            var reader = DBConnect.Connection(query);
            while (reader.Read())
            {
                DataForListView data = new DataForListView();
                data.id = Convert.ToInt32(reader.GetValue(0));
                data.fio = reader.GetValue(1).ToString();
                data.typeOperationText = reader.GetValue(2).ToString();
                data.formatText = reader.GetValue(3).ToString();
                data.side = Convert.ToInt32(reader.GetValue(4));
                data.colorText = reader.GetValue(5).ToString();
                data.count = Convert.ToInt32(reader.GetValue(6));
                data.price = Convert.ToInt32(reader.GetValue(7));
                data.signature = img;
                data.date = reader.GetValue(8).ToString();
                dataForListView.Add(data);
            }
            foreach (DataForListView data in dataForListView)
            {
                Operations.Items.Add(data);
            }
        }

        private void DeleteOperation(object sender, RoutedEventArgs e)
        {
            DataForListView data = Operations.SelectedItem as DataForListView;
            string query = String.Format("DELETE FROM `mydb`.`operations` WHERE id = {0}",data.id);
            DBConnect.Connection(query);
            Operations.Items.Remove(Operations.SelectedItem);
            
        }

        private void EditOperation(object sender, RoutedEventArgs e)
        {
            Edit form = new Edit(Operations.SelectedItem as DataForListView);
            form.ShowDialog();
            LoadData();

        }

        private void AddRecordsPrints(object sender, RoutedEventArgs e)
        {
            MainWindow.frame.Navigate(new Main());
        }

        private void DataFilter(object sender, RoutedEventArgs e)
        {
            DateTime date = DateTime.Now;
            string filter = sender.ToString().Split(' ')[1];
            if (filter == "Сегодня")
            {
                Operations.Items.Clear();
                foreach (DataForListView data in dataForListView)
                {

                    if ((Convert.ToDateTime(data.date) - date).TotalDays < 1)
                    {
                        Operations.Items.Add(data);
                    }
                }
            }
            else if (filter == "Вчера")
            {
                Operations.Items.Clear();
                foreach (DataForListView data in dataForListView)
                {
                    
                    if ((Convert.ToDateTime(data.date) - date).TotalDays == 1)
                    {
                        Operations.Items.Add(data);
                    }
                }
            }
            else if (filter == "Неделя")
            {
                Operations.Items.Clear();
                foreach (DataForListView data in dataForListView)
                {

                    if ((Convert.ToDateTime(data.date) - date).TotalDays <= 7)
                    {
                        Operations.Items.Add(data);
                    }
                }
            }
            else if (filter == "Месяц")
            {
                Operations.Items.Clear();
                foreach (DataForListView data in dataForListView)
                {

                    if ((Convert.ToDateTime(data.date) - date).TotalDays <= 31)
                    {
                        Operations.Items.Add(data);
                    }
                }
            }
            else if (filter == "Квартал")
            {
                Operations.Items.Clear();
                foreach (DataForListView data in dataForListView)
                {

                    if ((Convert.ToDateTime(data.date) - date).TotalDays <= 31 * 4)
                    {
                        Operations.Items.Add(data);
                    }
                }
            }
            else if (filter == "Год")
            {
                Operations.Items.Clear();
                foreach (DataForListView data in dataForListView)
                {

                    if ((Convert.ToDateTime(data.date) - date).TotalDays <= 365)
                    {
                        Operations.Items.Add(data);
                    }
                }
            }
            else
            {
                Operations.Items.Clear();
                foreach (DataForListView data in dataForListView)
                {
                    Operations.Items.Add(data);
                }
            }
        }
    }
}
