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
using MySql.Data.MySqlClient;

namespace Pr17
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            OpenPage(pages.products);
        }

        public class Items
        {
            public int id { get; set; }
            public string name { get; set; }
            public string color { get; set; }
            public int max_memory { get; set; }
            public string diagonal { get; set; }
            public string resolution { get; set; }
        }
        public List<Items> items = new List<Items>();

        public void LoadData()
        {
            items.Clear();

            MySqlDataReader itemsQuery = DbConnect.Connection("SELECT * FROM `ElectroShop`");

            while(itemsQuery.Read())
            {
                Items newItem = new Items();
                newItem.id = Convert.ToInt32(itemsQuery.GetValue(0));
                newItem.name = Convert.ToString(itemsQuery.GetValue(1));
                newItem.color = Convert.ToString(itemsQuery.GetValue(2));
                newItem.max_memory = Convert.ToInt32(itemsQuery.GetValue(3));
                newItem.diagonal = Convert.ToString(itemsQuery.GetValue(4));
                newItem.resolution = Convert.ToString(itemsQuery.GetValue(5));

                items.Add(newItem);
            }

        }

        public enum pages
        {
            products
        }

        public void OpenPage(pages _pages)
        {
            if(_pages == pages.products)
            {
                frame.Navigate(new Pages.Products(this));
            }
        }
    }
}
