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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace Pr17.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddAndChange.xaml
    /// </summary>
    public partial class AddAndChange : Window
    {
        private int id;
        public AddAndChange(int id = 0, string name = "", string color = "", int maxMemory = 0,
            string diagonal = "", int resolution = 0)
        {
            InitializeComponent();
            string query = "select color from mydb.color";
            MySqlDataReader colors = DbConnect.Connection(query);
            while (colors.Read())
            {
                this.color.Items.Add(colors.GetValue(0).ToString());
            }
            if (id != 0)
            {
                Title = "Изменить";
                this.name.Text = name;
                this.maxMemory.Text = maxMemory.ToString();
                this.diagonal.Text = diagonal;
                this.resolution.Text = resolution.ToString();               
                this.color.SelectedItem = color;
            }
            this.id = id;
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            if(id != 0)
            {
                string query = String.Format("update mydb.electroshop set name = '{0}', color = '{1}', max_memory = {2}, diagonal = '{3}', resolution = {4} where id = {5}",
                    name.Text, color.SelectedItem, maxMemory.Text, diagonal.Text, resolution.Text, id);
                DbConnect.Connection(query);
                Close();
            }
            else
            {
                string query = String.Format("INSERT INTO `mydb`.`electroshop` (`name`, `color`, `max_memory`, `diagonal`, `resolution`) " +
                    "VALUES ('{0}', '{1}', {2}, '{3}', {4})",
                    name.Text, color.SelectedItem, maxMemory.Text, diagonal.Text, resolution.Text);
                DbConnect.Connection(query);
                Close();
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
